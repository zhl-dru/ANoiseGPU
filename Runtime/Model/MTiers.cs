namespace ANoiseGPU
{
    public class MTiers : MBase
    {
        private MBase m_source;
        private int m_numtiers;
        private bool m_smooth;

        private const string c_numtiers = "tiers_numtiers";
        private const string c_smooth = "tiers_smooth";

        public MTiers SetSource(MBase source) { m_source = source; return this; }
        public MTiers SetSource(float source) { m_source = new MConstant(source); return this; }
        public MTiers SetNumTiers(int numtiers) { m_numtiers = numtiers; return this; }
        public MTiers SetSmooth(bool smooth) { m_smooth = smooth; return this; }
        public MTiers Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KTiersMain");

        protected override int K3DId => Shader.FindKernel("KTiersMain");

        protected override int K4DId => Shader.FindKernel("KTiersMain");

        protected override string ResultBufferName => "KTiersOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetInt(c_numtiers, m_numtiers);
            Shader.SetBool(c_smooth, m_smooth);
        }
    }
}