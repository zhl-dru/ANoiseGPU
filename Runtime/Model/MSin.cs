namespace ANoiseGPU
{
    public class MSin : MBase
    {
        private MBase m_source;

        public MSin SetSource(MBase source) { m_source = source; return this; }
        public MSin SetSource(float source) { m_source = new MConstant(source); return this; }
        public MSin Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KSinMain");

        protected override int K3DId => Shader.FindKernel("KSinMain");

        protected override int K4DId => Shader.FindKernel("KSinMain");

        protected override string ResultBufferName => "KSinOutputBuffer";
    }
}