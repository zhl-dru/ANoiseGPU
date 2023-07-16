using UnityEngine;

namespace ANoiseGPU
{
    public class MRotateDomain : MBase
    {
        private MBase m_source;
        private MBase m_ax, m_ay, m_az, m_angledeg;

        private const string c_domain2DName = "KRotateDomainCoordMain2D";
        private const string c_domain3DName = "KRotateDomainCoordMain3D";
        private const string c_domain4DName = "KRotateDomainCoordMain4D";
        public MRotateDomain SetSource(MBase source) { m_source = source; return this; }
        public MRotateDomain SetSource(float source) { m_source = new MConstant(source); return this; }
        public MRotateDomain SetAxis(MBase ax, MBase ay, MBase az) { m_ax = ax; m_ay = ay; m_az = az; return this; }
        public MRotateDomain SetAxis(float ax, float ay, float az)
        {
            m_ax = new MConstant(ax);
            m_ay = new MConstant(ay);
            m_az = new MConstant(az);
            return this;
        }
        public MRotateDomain SetAxisX(MBase ax) { m_ax = ax; return this; }
        public MRotateDomain SetAxisY(MBase ay) { m_ay = ay; return this; }
        public MRotateDomain SetAxisZ(MBase az) { m_az = az; return this; }
        public MRotateDomain SetAxisX(float ax) { m_ax = new MConstant(ax); return this; }
        public MRotateDomain SetAxisY(float ay) { m_ay = new MConstant(ay); return this; }
        public MRotateDomain SetAxisZ(float az) { m_az = new MConstant(az); return this; }
        public MRotateDomain SetAxisXYZ(MBase ax, MBase ay, MBase az)
        {
            m_ax = ax;
            m_ay = ay;
            m_az = az;
            return this;
        }
        public MRotateDomain SetAxisXYZ(float ax, float ay, float az)
        {
            m_ax = new MConstant(ax);
            m_ay = new MConstant(ay);
            m_az = new MConstant(az);
            return this;
        }
        public MRotateDomain SetAngle(MBase angle) { m_angledeg = angle; return this; }
        public MRotateDomain SetAngle(float angle) { m_angledeg = new MConstant(angle); return this; }
        public MRotateDomain Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_angledeg));
            bufferDatas.Add(new ValueBufferData(1, m_ax));
            bufferDatas.Add(new ValueBufferData(2, m_ay));
            bufferDatas.Add(new ValueBufferData(3, m_az));
            bufferDatas.Add(new ValueBufferData(4, m_source, c_domain2DName, c_domain3DName, c_domain4DName));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KRotateDomainMain");

        protected override int K3DId => Shader.FindKernel("KRotateDomainMain");

        protected override int K4DId => Shader.FindKernel("KRotateDomainMain");

        protected override string ResultBufferName => "KRotateDomainBuffer";

        protected override void SetBuffer(int resolution, int numthreads, int kId, DimensionType dtype, ComputeBuffer coordBuffer, ComputeBuffer valueBuffer)
        {
            ComputeBuffer angledegBuffer = CreateValueBuffer(resolution);
            ComputeBuffer axBuffer = CreateValueBuffer(resolution);
            ComputeBuffer ayBuffer = CreateValueBuffer(resolution);
            ComputeBuffer azBuffer = CreateValueBuffer(resolution);

            m_angledeg.Get(resolution, numthreads, dtype, coordBuffer, angledegBuffer);
            m_ax.Get(resolution, numthreads, dtype, coordBuffer, axBuffer);
            m_ay.Get(resolution, numthreads, dtype, coordBuffer, ayBuffer);
            m_az.Get(resolution, numthreads, dtype, coordBuffer, azBuffer);

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
            Shader.SetBuffer(domainId, Config.GetCoordBufferName(dtype), coordBuffer);
            Shader.SetBuffer(domainId, Config.GetDomainCoordBufferName(dtype), domainBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(0), angledegBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(1), axBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(2), ayBuffer);
            Shader.SetBuffer(domainId, Config.GetInputBufferName(3), azBuffer);
            Shader.Dispatch(domainId, numthreads, numthreads, 1);

            m_source.Get(resolution, numthreads, dtype, domainBuffer, valueBuffer);
        }
    }
}