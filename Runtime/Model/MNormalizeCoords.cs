using UnityEngine;

namespace ANoiseGPU
{
    public class MNormalizeCoords : MBase
    {
        private MBase m_source;
        private MBase m_length;

        private const string c_domain2DName = "KNormalizeCoordsMain2D";
        private const string c_domain3DName = "KNormalizeCoordsMain3D";
        private const string c_domain4DName = "KNormalizeCoordsMain4D";

        public MNormalizeCoords SetSource(MBase source) { m_source = source; return this; }
        public MNormalizeCoords SetSource(float source) { m_source = new MConstant(source); return this; }
        public MNormalizeCoords SetLength(MBase length) { m_length = length; return this; }
        public MNormalizeCoords SetLength(float length) { m_length = new MConstant(length); return this; }
        public MNormalizeCoords Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_length));
            bufferDatas.Add(new ValueBufferData(1, m_source, c_domain2DName, c_domain3DName, c_domain4DName));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KNormalizeCoordsMain");

        protected override int K3DId => Shader.FindKernel("KNormalizeCoordsMain");

        protected override int K4DId => Shader.FindKernel("KNormalizeCoordsMain");

        protected override string ResultBufferName => "KNormalizeCoordsOutputBuffer";

        protected override void SetBuffer(int resolution, int numthreads, int kId, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            ComputeBuffer lengthBuffer = CreateValueBuffer(resolution);
            m_length.Get(resolution, numthreads, dtype, coordBuffer, lengthBuffer);
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
            Shader.SetBuffer(domainId, Config.GetInputBufferName(0), lengthBuffer);
            Shader.SetBuffer(domainId, Config.GetCoordBufferName(dtype), coordBuffer);
            Shader.SetBuffer(domainId, Config.GetDomainCoordBufferName(dtype), domainBuffer);
            Shader.Dispatch(domainId, numthreads, numthreads, 1);

            m_source.Get(resolution, numthreads, dtype, domainBuffer, valueBuffer);
        }
    }
}