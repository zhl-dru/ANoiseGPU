namespace ANoiseGPU
{
    public class MSawtooth : MBase
    {
        private MBase m_source;
        private MBase m_period;

        public MSawtooth SetSource(MBase source) { m_source = source; return this; }
        public MSawtooth SetPeriod(MBase period) { m_period = period; return this; }
        public MSawtooth SetSource(float source) { m_source = new MConstant(source); return this; }
        public MSawtooth SetPeriod(float period) { m_period = new MConstant(period); return this; }
        public MSawtooth Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_period));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KSawtoothMain");

        protected override int K3DId => Shader.FindKernel("KSawtoothMain");

        protected override int K4DId => Shader.FindKernel("KSawtoothMain");

        protected override string ResultBufferName => "KSawtoothOutputBuffer";
    }
}