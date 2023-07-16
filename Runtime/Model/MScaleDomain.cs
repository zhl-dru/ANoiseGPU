using UnityEngine;

namespace ANoiseGPU
{
    public class MScaleDomain : MBase
    {
        private MBase m_source;
        private MBase m_sx, m_sy, m_sz, m_sw;

        private const string c_domain2DName = "KScaleDomainCoordMain2D";
        private const string c_domain3DName = "KScaleDomainCoordMain3D";
        private const string c_domain4DName = "KScaleDomainCoordMain4D";

        public MScaleDomain SetSource(MBase source) { m_source = source; return this; }
        public MScaleDomain SetSource(float source) { m_source = m_source = new MConstant(source); return this; }
        public MScaleDomain SetScaleX(MBase x) { m_sx = x; return this; }
        public MScaleDomain SetScaleY(MBase y) { m_sy = y; return this; }
        public MScaleDomain SetScaleZ(MBase z) { m_sz = z; return this; }
        public MScaleDomain SetScaleW(MBase w) { m_sw = w; return this; }
        public MScaleDomain SetScaleX(float x) { m_sx = new MConstant(x); return this; }
        public MScaleDomain SetScaleY(float y) { m_sy = new MConstant(y); return this; }
        public MScaleDomain SetScaleZ(float z) { m_sz = new MConstant(z); return this; }
        public MScaleDomain SetScaleW(float w) { m_sw = new MConstant(w); return this; }
        public MScaleDomain SetScaleXYZW(MBase x, MBase y, MBase z, MBase w)
        {
            m_sx = x;
            m_sy = y;
            m_sz = z;
            m_sw = w;
            return this;
        }
        public MScaleDomain SetScaleXYZW(float x, float y, float z, float w)
        {
            m_sx = new MConstant(x);
            m_sy = new MConstant(y);
            m_sz = new MConstant(z);
            m_sw = new MConstant(w);
            return this;
        }
        public MScaleDomain Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_sx));
            bufferDatas.Add(new ValueBufferData(1, m_sy));
            bufferDatas.Add(new ValueBufferData(2, m_sz));
            bufferDatas.Add(new ValueBufferData(3, m_sw));
            bufferDatas.Add(new ValueBufferData(4, m_source, c_domain2DName, c_domain3DName, c_domain4DName));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KScaleDomainMain");

        protected override int K3DId => Shader.FindKernel("KScaleDomainMain");

        protected override int K4DId => Shader.FindKernel("KScaleDomainMain");

        protected override string ResultBufferName => "KScaleDomainOutputBuffer";

        protected override void SetBuffer(int resolution, int numthreads, int kId, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            ComputeBuffer xBuffer = CreateValueBuffer(resolution);
            ComputeBuffer yBuffer = CreateValueBuffer(resolution);
            ComputeBuffer zBuffer = CreateValueBuffer(resolution);
            ComputeBuffer wBuffer = CreateValueBuffer(resolution);

            m_sx.Get(resolution, numthreads, dtype, coordBuffer, xBuffer);
            m_sy.Get(resolution, numthreads, dtype, coordBuffer, yBuffer);
            m_sz.Get(resolution, numthreads, dtype, coordBuffer, zBuffer);
            m_sw.Get(resolution, numthreads, dtype, coordBuffer, wBuffer);

            ComputeBuffer domainBuffer = CreateCoordBuffer(resolution, dtype);
            int domainId;
            switch (dtype)
            {
                case DimensionType._2D:
                    domainId = Shader.FindKernel(c_domain2DName);
                    break;
                case DimensionType._3D:
                    domainId = Shader.FindKernel(c_domain3DName);
                    break;
                case DimensionType._4D:
                    domainId = Shader.FindKernel(c_domain4DName);
                    break;
                default:
                    domainId = Shader.FindKernel(c_domain2DName);
                    break;
            }
            Shader.SetBuffer(domainId, Config.GetInputBufferName(0), xBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(1), yBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(2), zBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(3), wBuffer);
            Shader.SetBuffer(domainId, Config.GetCoordBufferName(dtype), coordBuffer);
            Shader.SetBuffer(domainId, Config.GetDomainCoordBufferName(dtype), domainBuffer);
            Shader.Dispatch(domainId, numthreads, numthreads, 1);

            m_source.Get(resolution, numthreads, dtype, domainBuffer, valueBuffer);
        }
    }
}