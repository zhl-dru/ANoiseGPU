namespace ANoiseGPU
{
    public class MTriangle : MBase
    {
        private MBase m_source, m_period, m_offset;


        public MTriangle SetSource(MBase s) { m_source = s; return this; }
        public MTriangle SetPeriod(MBase p) { m_period = p; return this; }
        public MTriangle SetOffset(MBase o) { m_offset = o; return this; }
        public MTriangle SetSource(float s) { m_source = new MConstant(s); return this; }
        public MTriangle SetPeriod(float p) { m_period = new MConstant(p); return this; }
        public MTriangle SetOffset(float o) { m_offset = new MConstant(o); return this; }
        public MTriangle Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_period));
            bufferDatas.Add(new ValueBufferData(2, m_offset));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KTriangleMain");

        protected override int K3DId => Shader.FindKernel("KTriangleMain");

        protected override int K4DId => Shader.FindKernel("KTriangleMain");

        protected override string ResultBufferName => "KTriangleOutputBuffer";
    }
}