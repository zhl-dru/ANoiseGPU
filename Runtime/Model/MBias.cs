namespace ANoiseGPU
{
    public class MBias : MBase
    {
        private MBase m_source;
        private MBase m_bias;

        public MBias SetSource(MBase source) { m_source = source; return this; }
        public MBias SetBias(MBase bias) { m_bias = bias; return this; }
        public MBias SetSource(float source) { m_source = new MConstant(source); return this; }
        public MBias SetBias(float bias) { m_bias = new MConstant(bias); return this; }
        public MBias Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_bias));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KBiasMain");

        protected override int K3DId => Shader.FindKernel("KBiasMain");

        protected override int K4DId => Shader.FindKernel("KBiasMain");

        protected override string ResultBufferName => "KBiasOutputBuffer";
    }
}