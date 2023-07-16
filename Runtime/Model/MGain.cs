namespace ANoiseGPU
{
    public class MGain : MBase
    {
        private MBase m_source;
        private MBase m_gain;

        public MGain SetSource(MBase source) { m_source = source; return this; }
        public MGain SetGain(MBase gain) { m_gain = gain; return this; }
        public MGain SetSource(float source) { m_source = new MConstant(source); return this; }
        public MGain SetGain(float gain) { m_gain = new MConstant(gain); return this; }
        public MGain Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_gain));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KGainMain");

        protected override int K3DId => Shader.FindKernel("KGainMain");

        protected override int K4DId => Shader.FindKernel("KGainMain");

        protected override string ResultBufferName => "KGainOutputBuffer";
    }
}