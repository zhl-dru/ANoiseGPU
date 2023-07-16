namespace ANoiseGPU
{
    public class MFloor : MBase
    {
        private MBase m_source;

        public MFloor SetSource(MBase source) { m_source = source; return this; }
        public MFloor SetSource(float source) { m_source = new MConstant(source); return this; }
        public MFloor Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KFloorMain");

        protected override int K3DId => Shader.FindKernel("KFloorMain");

        protected override int K4DId => Shader.FindKernel("KFloorMain");

        protected override string ResultBufferName => "KFloorOutputBuffer";
    }
}