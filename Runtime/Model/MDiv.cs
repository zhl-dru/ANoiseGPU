namespace ANoiseGPU
{
    public class MDiv : MBase
    {
        private MBase m_source1;
        private MBase m_source2;


        public MDiv SetSource1(MBase source1) { m_source1 = source1; return this; }
        public MDiv SetSource2(MBase source2) { m_source2 = source2; return this; }
        public MDiv SetSource1(float source1) { m_source1 = new MConstant(source1); return this; }
        public MDiv SetSource2(float source2) { m_source2 = new MConstant(source2); ; return this; }
        public MDiv Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source1));
            bufferDatas.Add(new ValueBufferData(1, m_source2));
            return this;
        }
        protected override int K2DId => Shader.FindKernel("KDivMain");

        protected override int K3DId => Shader.FindKernel("KDivMain");

        protected override int K4DId => Shader.FindKernel("KDivMain");

        protected override string ResultBufferName => "KDivOutputBuffer";
    }
}