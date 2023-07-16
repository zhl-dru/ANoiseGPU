using UnityEngine;

namespace ANoiseGPU
{
    public class MTranslateDomain : MBase
    {
        private MBase m_source, m_ax, m_ay, m_az, m_aw;

        private const string c_domain2DName = "KTranslateDomainCoordMain2D";
        private const string c_domain3DName = "KTranslateDomainCoordMain3D";
        private const string c_domain4DName = "KTranslateDomainCoordMain4D";

        public MTranslateDomain SetAxisX(MBase x) { m_ax = x; return this; }
        public MTranslateDomain SetAxisY(MBase y) { m_ay = y; return this; }
        public MTranslateDomain SetAxisZ(MBase z) { m_az = z; return this; }
        public MTranslateDomain SetAxisW(MBase w) { m_aw = w; return this; }
        public MTranslateDomain SetAxisX(float x) { m_ax = new MConstant(x); return this; }
        public MTranslateDomain SetAxisY(float y) { m_ay = new MConstant(y); return this; }
        public MTranslateDomain SetAxisZ(float z) { m_az = new MConstant(z); return this; }
        public MTranslateDomain SetAxisW(float w) { m_aw = new MConstant(w); return this; }
        public MTranslateDomain SetAxisXYZW(MBase x, MBase y, MBase z, MBase w)
        {
            m_ax = x;
            m_ay = y;
            m_az = z;
            m_aw = w;
            return this;
        }
        public MTranslateDomain SetAxisXYZW(float x, float y, float z, float w)
        {
            m_ax = new MConstant(x);
            m_ay = new MConstant(y);
            m_az = new MConstant(z);
            m_aw = new MConstant(w);
            return this;
        }
        public MTranslateDomain SetSource(MBase source) { m_source = source; return this; }
        public MTranslateDomain SetSource(float source) { m_source = new MConstant(source); return this; }
        public MTranslateDomain Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_ax));
            bufferDatas.Add(new ValueBufferData(1, m_ay));
            bufferDatas.Add(new ValueBufferData(2, m_az));
            bufferDatas.Add(new ValueBufferData(3, m_aw));
            bufferDatas.Add(new ValueBufferData(4, m_source, c_domain2DName, c_domain3DName, c_domain4DName));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KTranslateDomainMain");

        protected override int K3DId => Shader.FindKernel("KTranslateDomainMain");

        protected override int K4DId => Shader.FindKernel("KTranslateDomainMain");

        protected override string ResultBufferName => "KTranslateDomainOutputBuffer";

        protected override void SetBuffer(int resolution, int numthreads, int kId, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            ComputeBuffer xBuffer = CreateValueBuffer(resolution);
            ComputeBuffer yBuffer = CreateValueBuffer(resolution);
            ComputeBuffer zBuffer = CreateValueBuffer(resolution);
            ComputeBuffer wBuffer = CreateValueBuffer(resolution);

            m_ax.Get(resolution, numthreads, dtype, coordBuffer, xBuffer);
            m_ay.Get(resolution, numthreads, dtype, coordBuffer, yBuffer);
            m_az.Get(resolution, numthreads, dtype, coordBuffer, zBuffer);
            m_aw.Get(resolution, numthreads, dtype, coordBuffer, wBuffer);

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