namespace ANoiseGPU
{
    public class MMagnitude : MBase
    {
        private MBase m_x, m_y, m_z, m_w;

        public MMagnitude SetX(MBase x) { m_x = x; return this; }
        public MMagnitude SetY(MBase y) { m_y = y; return this; }
        public MMagnitude SetZ(MBase z) { m_z = z; return this; }
        public MMagnitude SetW(MBase w) { m_w = w; return this; }
        public MMagnitude SetX(float x) { m_x = new MConstant(x); return this; }
        public MMagnitude SetY(float y) { m_y = new MConstant(y); return this; }
        public MMagnitude SetZ(float z) { m_z = new MConstant(z); return this; }
        public MMagnitude SetW(float w) { m_w = new MConstant(w); return this; }
        public MMagnitude SetXYZW(MBase x, MBase y, MBase z, MBase w)
        {
            m_x = x;
            m_y = y;
            m_z = z;
            m_w = w;
            return this;
        }
        public MMagnitude SetXYZW(float x, float y, float z, float w)
        {
            m_x = new MConstant(x);
            m_y = new MConstant(y);
            m_z = new MConstant(z);
            m_w = new MConstant(w);
            return this;
        }
        public MMagnitude Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_x));
            bufferDatas.Add(new ValueBufferData(1, m_y));
            bufferDatas.Add(new ValueBufferData(2, m_z));
            bufferDatas.Add(new ValueBufferData(3, m_w));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KMagnitudeMain2D");

        protected override int K3DId => Shader.FindKernel("KMagnitudeMain3D");

        protected override int K4DId => Shader.FindKernel("KMagnitudeMain4D");

        protected override string ResultBufferName => "KMagnitudeOutputBuffer";
    }
}