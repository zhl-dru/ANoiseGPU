namespace ANoiseGPU
{
    public class MBrightContrast : MBase
    {
        private MBase m_source;
        private MBase m_bright, m_threshold, m_factor;

        public MBrightContrast SetSource(MBase source) { m_source = source; return this; }
        public MBrightContrast SetBright(MBase bright) { m_bright = bright; return this; }
        public MBrightContrast SetThreshold(MBase threshold) { m_threshold = threshold; return this; }
        public MBrightContrast SetFactor(MBase factor) { m_factor = factor; return this; }
        public MBrightContrast SetSource(float source) { m_source = new MConstant(source); return this; }
        public MBrightContrast SetBright(float bright) { m_bright = new MConstant(bright); return this; }
        public MBrightContrast SetThreshold(float threshold) { m_threshold = new MConstant(threshold); return this; }
        public MBrightContrast SetFactor(float factor) { m_factor = new MConstant(factor); return this; }
        public MBrightContrast Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_bright));
            bufferDatas.Add(new ValueBufferData(2, m_threshold));
            bufferDatas.Add(new ValueBufferData(3, m_factor));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KBrightContrastMain");

        protected override int K3DId => Shader.FindKernel("KBrightContrastMain");

        protected override int K4DId => Shader.FindKernel("KBrightContrastMain");

        protected override string ResultBufferName => "KBCOutputBuffer";
    }
}