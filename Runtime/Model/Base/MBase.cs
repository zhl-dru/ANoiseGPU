using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;

namespace ANoiseGPU
{
    public abstract partial class MBase
    {
        public static ComputeShader Shader = null;
        private static List<ComputeBuffer> bufferCache = new List<ComputeBuffer>();

        protected List<ValueBufferData> bufferDatas = new List<ValueBufferData>();

        protected abstract int K2DId { get; }
        protected abstract int K3DId { get; }
        protected abstract int K4DId { get; }

        private int KId(DimensionType dtype)
        {
            int kId;
            switch (dtype)
            {
                case DimensionType._2D:
                    kId = K2DId;
                    break;
                case DimensionType._3D:
                    kId = K3DId;
                    break;
                case DimensionType._4D:
                    kId = K4DId;
                    break;
                default:
                    kId = K2DId;
                    break;
            }
            return kId;
        }

        private const string c_resolution = "resolution";
        private const string c_start = "start";
        private const string c_step = "stepsize";

        private int C2DId => Shader.FindKernel("KOriginCoordMain2D");
        private int C3DId => Shader.FindKernel("KOriginCoordMain3D");
        private int C4DId => Shader.FindKernel("KOriginCoordMain4D");

        private const int fsize = 4;
        private const int tnum = 16;

        protected abstract string ResultBufferName { get; }
        protected virtual void SetBuffer(int resolution, int numthreads, int kId, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            if (bufferDatas.Count > 0)
            {
                for (int i = 0; i < bufferDatas.Count; i++)
                {
                    ValueBufferData bufferData = bufferDatas[i];
                    MBase module = bufferData.Module;

                    ComputeBuffer vBuffer = CreateValueBuffer(resolution);
                    ComputeBuffer cBuffer = SetCoordBuffer(resolution, numthreads, bufferData, dtype, coordBuffer);

                    module.Get(resolution, numthreads, dtype, cBuffer, vBuffer);
                    Shader.SetBuffer(kId, Config.GetInputBufferName(bufferData.Input), vBuffer);
                }
            }
            Shader.SetBuffer(kId, Config.GetCoordBufferName(dtype), coordBuffer);
            Shader.SetBuffer(kId, ResultBufferName, valueBuffer);
            Shader.Dispatch(kId, numthreads, numthreads, 1);
        }
        protected virtual void SetVariableData() { }
        private ComputeBuffer InitCoordBuffer(int resolution, int numthreads, DimensionType dtype)
        {
            int cId;
            switch (dtype)
            {
                case DimensionType._2D: cId = C2DId; break;
                case DimensionType._3D: cId = C3DId; break;
                case DimensionType._4D: cId = C4DId; break;
                default: cId = C2DId; break;
            }
            ComputeBuffer buffer = CreateCoordBuffer(resolution, dtype);
            Shader.SetBuffer(cId, Config.GetCoordBufferName(dtype), buffer);
            Shader.Dispatch(cId, numthreads, numthreads, 1);
            return buffer;

        }
        private ComputeBuffer SetCoordBuffer(int resolution, int numthreads, ValueBufferData bufferData, DimensionType dtype, ComputeBuffer coordBuffer)
        {
            if (bufferData.UseDomain)
            {
                int domainId;
                switch (dtype)
                {
                    case DimensionType._2D:
                        domainId = Shader.FindKernel(bufferData.D2dName);
                        break;
                    case DimensionType._3D:
                        domainId = Shader.FindKernel(bufferData.D3dName);
                        break;
                    case DimensionType._4D:
                        domainId = Shader.FindKernel(bufferData.D4dName);
                        break;
                    default:
                        domainId = Shader.FindKernel(bufferData.D2dName);
                        break;
                }
                ComputeBuffer domainBuffer = CreateCoordBuffer(resolution, dtype);
                Shader.SetBuffer(domainId, Config.GetCoordBufferName(dtype), coordBuffer);
                Shader.SetBuffer(domainId, Config.GetDomainCoordBufferName(dtype), domainBuffer);
                Shader.Dispatch(domainId, numthreads, numthreads, 1);

                return domainBuffer;
            }
            else
            {
                return coordBuffer;
            }
        }

        internal void Get(int resolution, int numthreads, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            int kId = KId(dtype);
            SetVariableData();
            SetBuffer(resolution, numthreads, kId, dtype, coordBuffer, valueBuffer);
        }
        private ComputeBuffer Get(int resolution, DimensionType dtype)
        {
            GPUResolutionData data = new GPUResolutionData(resolution, new float2(0, 0), 1 / (float)resolution);
            return Get(data, dtype);
        }
        private ComputeBuffer Get(GPUResolutionData data, DimensionType dtype)
        {
            Check();
            int resolution = data.Resolution;
            SetResolutionData(data);
            int tn = CalculateThreadGroups(resolution);
            ComputeBuffer coordBuffer = InitCoordBuffer(resolution, tn, dtype);
            ComputeBuffer valueBuffer = new ComputeBuffer(resolution * resolution, fsize);
            Get(resolution, tn, dtype, coordBuffer, valueBuffer);
            return valueBuffer;
        }

        public ComputeBuffer Get2(int resolution)
        {
            return Get(resolution, DimensionType._2D);
        }
        public ComputeBuffer Get3(int resolution)
        {
            return Get(resolution, DimensionType._3D);
        }
        public ComputeBuffer Get4(int resolution)
        {
            return Get(resolution, DimensionType._4D);
        }

        public ComputeBuffer Get2(GPUResolutionData data)
        {
            return Get(data, DimensionType._2D);
        }
        public ComputeBuffer Get3(GPUResolutionData data)
        {
            return Get(data, DimensionType._3D);
        }
        public ComputeBuffer Get4(GPUResolutionData data)
        {
            return Get(data, DimensionType._4D);
        }
        public static void Dispose()
        {
            for (int i = 0; i < bufferCache.Count; i++)
            {
                bufferCache[i].Dispose();
            }
            bufferCache.Clear();
        }

        protected void Examine(params MBase[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                MBase m = args[i];
                if (m == null)
                {
                    m = new MConstant(0);
                    Debug.LogWarning("");
                }
            }
        }

        private void Check()
        {
            if (bufferCache.Count > 0)
            {
                Debug.LogError("存在未清理的ComputeBuffer缓存");
            }
        }
        protected ComputeBuffer CreateValueBuffer(int resolution)
        {
            ComputeBuffer buffer = new ComputeBuffer(resolution * resolution, fsize);
            bufferCache.Add(buffer);
            return buffer;
        }
        protected ComputeBuffer CreateCoordBuffer(int resolution, DimensionType dtype)
        {
            int size;
            switch (dtype)
            {
                case DimensionType._2D: size = fsize * 2; break;
                case DimensionType._3D: size = fsize * 3; break;
                case DimensionType._4D: size = fsize * 4; break;
                default: size = fsize * 2; break;
            }
            ComputeBuffer buffer = new ComputeBuffer(resolution * resolution, size);
            bufferCache.Add(buffer);
            return buffer;
        }

        private void SetResolutionData(GPUResolutionData data)
        {
            Shader.SetInt(c_resolution, data.Resolution);
            Shader.SetFloats(c_start, data.Start.x, data.Start.y);
            Shader.SetFloat(c_step, data.Step);
        }
        private int CalculateThreadGroups(int resolution)
        {
            int tn = (int)math.ceil(resolution / tnum);
            return tn < 1 ? 1 : tn;
        }
    }

    public struct GPUResolutionData
    {
        public int Resolution;
        public float2 Start;
        public float Step;

        public GPUResolutionData(int resolution, float2 start, float step)
        {
            Resolution = resolution;
            Start = start;
            Step = step;
        }
    }

    public class ValueBufferData
    {
        private int input;
        private MBase module;
        private bool useDomain;

        private string d2dName;
        private string d3dName;
        private string d4dName;

        public int Input { get => input; }
        public MBase Module { get => module; }
        public bool UseDomain { get => useDomain; }
        public string D2dName { get => d2dName; }
        public string D3dName { get => d3dName; }
        public string D4dName { get => d4dName; }

        public ValueBufferData(int i, MBase m)
        {
            input = i;
            if (m == null)
                m = new MConstant(0);
            module = m;
            useDomain = false;
        }
        public ValueBufferData(int i, MBase m, string d2n, string d3n, string d4n)
        {
            input = i;
            if (m == null)
                m = new MConstant(0);
            module = m;
            useDomain = true;
            d2dName = d2n;
            d3dName = d3n;
            d4dName = d4n;
        }
    }

    public enum DimensionType
    {
        _2D, _3D, _4D
    }
}
