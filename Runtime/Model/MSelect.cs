namespace ANoiseGPU
{
    public class MSelect : MBase
    {
        private MBase m_low, m_high, m_control;
        private MBase m_threshold, m_falloff;

        public MSelect SetLowSource(MBase low) { m_low = low; return this; }
        public MSelect SetHighSource(MBase high) { m_high = high; return this; }
        public MSelect SetControlSource(MBase control) { m_control = control; return this; }
        public MSelect SetThreshold(MBase threshold) { m_threshold = threshold; return this; }
        public MSelect SetFalloff(MBase falloff) { m_falloff = falloff; return this; }
        public MSelect SetLowSource(float low) { m_low = new MConstant(low); return this; }
        public MSelect SetHighSource(float high) { m_high = new MConstant(high); return this; }
        public MSelect SetControlSource(float control) { m_control = new MConstant(control); return this; }
        public MSelect SetThreshold(float threshold) { m_threshold = new MConstant(threshold); return this; }
        public MSelect SetFalloff(float falloff) { m_falloff = new MConstant(falloff); return this; }
        public MSelect Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_low));
            bufferDatas.Add(new ValueBufferData(1, m_high));
            bufferDatas.Add(new ValueBufferData(2, m_control));
            bufferDatas.Add(new ValueBufferData(3, m_threshold));
            bufferDatas.Add(new ValueBufferData(4, m_falloff));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KSelectMain");

        protected override int K3DId => Shader.FindKernel("KSelectMain");

        protected override int K4DId => Shader.FindKernel("KSelectMain");

        protected override string ResultBufferName => "KSelectOutputBuffer";
    }
}