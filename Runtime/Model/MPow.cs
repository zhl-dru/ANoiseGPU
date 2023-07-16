namespace ANoiseGPU
{
    public class MPow : MBase
    {
        private MBase m_source;
        private MBase m_power;

        public MPow SetSource(MBase source) { m_source = source; return this; }
        public MPow SetPower(MBase power) { m_power = power; return this; }
        public MPow SetSource(float source) { m_source = new MConstant(source); return this; }
        public MPow SetPower(float power) { m_power = new MConstant(power); return this; }
        public MPow Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_power));
            return this;
        }
        protected override int K2DId => Shader.FindKernel("KPowMain");

        protected override int K3DId => Shader.FindKernel("KPowMain");

        protected override int K4DId => Shader.FindKernel("KPowMain");

        protected override string ResultBufferName => "KPowOutputBuffer";
    }
}