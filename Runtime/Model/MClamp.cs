namespace ANoiseGPU
{
    public class MClamp : MBase
    {
        private MBase m_source;
        private float m_low, m_high;

        private const string c_low = "clamp_low";
        private const string c_high = "clamp_high";

        public MClamp SetSource(MBase source) { m_source = source; return this; }
        public MClamp SetRange(float low, float high) { m_low = low; m_high = high; return this; }
        public MClamp Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KClampMain");

        protected override int K3DId => Shader.FindKernel("KClampMain");

        protected override int K4DId => Shader.FindKernel("KClampMain");

        protected override string ResultBufferName => "KClampOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetFloat(c_low, m_low);
            Shader.SetFloat(c_high, m_high);
        }
    }
}