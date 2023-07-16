namespace ANoiseGPU
{
    public class MScaleOffset : MBase
    {
        private MBase m_source;
        private MBase m_scale, m_offset;


        public MScaleOffset SetSource(MBase source) { m_source = source; return this; }
        public MScaleOffset SetScale(MBase scale) { m_scale = scale; return this; }
        public MScaleOffset SetOffset(MBase offset) { m_offset = offset; return this; }
        public MScaleOffset SetSource(float source) { m_source = new MConstant(source); return this; }
        public MScaleOffset SetScale(float scale) { m_scale = new MConstant(scale); return this; }
        public MScaleOffset SetOffset(float offset) { m_offset = new MConstant(offset); return this; }
        public MScaleOffset Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            bufferDatas.Add(new ValueBufferData(1, m_scale));
            bufferDatas.Add(new ValueBufferData(2, m_offset));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KScaleOffsetMain");

        protected override int K3DId => Shader.FindKernel("KScaleOffsetMain");

        protected override int K4DId => Shader.FindKernel("KScaleOffsetMain");

        protected override string ResultBufferName => "KScaleOffsetOutputBuffer";
    }
}