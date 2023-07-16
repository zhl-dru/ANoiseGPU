namespace ANoiseGPU
{
    public class MSub : MBase
    {
        private MBase m_source1;
        private MBase m_source2;

        public MSub SetSource1(MBase source1) { m_source1 = source1; return this; }
        public MSub SetSource2(MBase source2) { m_source2 = source2; return this; }
        public MSub SetSource1(float source1) { m_source1 = new MConstant(source1); return this; }
        public MSub SetSource2(float source2) { m_source2 = new MConstant(source2); return this; }
        public MSub Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source1));
            bufferDatas.Add(new ValueBufferData(1, m_source2));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KSubMain");

        protected override int K3DId => Shader.FindKernel("KSubMain");

        protected override int K4DId => Shader.FindKernel("KSubMain");

        protected override string ResultBufferName => "KSubOutputBuffer";
    }
}