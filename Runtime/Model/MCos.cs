namespace ANoiseGPU
{
    public class MCos : MBase
    {
        private MBase m_source;

        public MCos SetSource(MBase source) { m_source = source; return this; }
        public MCos SetSource(float source) { m_source = new MConstant(source); return this; }
        public MCos Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KCosMain");

        protected override int K3DId => Shader.FindKernel("KCosMain");

        protected override int K4DId => Shader.FindKernel("KCosMain");

        protected override string ResultBufferName => "KCosOutputBuffer";
    }
}