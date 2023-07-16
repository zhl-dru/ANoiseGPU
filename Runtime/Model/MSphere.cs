namespace ANoiseGPU
{
    public class MSphere : MBase
    {
        private MBase m_cx, m_cy, m_cz, m_cw;
        private MBase m_radius;


        public MSphere SetCenterX(MBase cx) { m_cx = cx; return this; }
        public MSphere SetCenterY(MBase cy) { m_cy = cy; return this; }
        public MSphere SetCenterZ(MBase cz) { m_cz = cz; return this; }
        public MSphere SetCenterW(MBase cw) { m_cw = cw; return this; }
        public MSphere SetCenterX(float cx) { m_cx = new MConstant(cx); return this; }
        public MSphere SetCenterY(float cy) { m_cy = new MConstant(cy); return this; }
        public MSphere SetCenterZ(float cz) { m_cz = new MConstant(cz); return this; }
        public MSphere SetCenterW(float cw) { m_cw = new MConstant(cw); return this; }
        public MSphere setCenterXYZW(MBase cx, MBase cy, MBase cz, MBase cw)
        {
            m_cx = cx;
            m_cy = cy;
            m_cz = cz;
            m_cw = cw;
            return this;
        }
        public MSphere setCenterXYZW(float cx, float cy, float cz, float cw)
        {
            m_cx = new MConstant(cx);
            m_cy = new MConstant(cy);
            m_cz = new MConstant(cz);
            m_cw = new MConstant(cw);
            return this;
        }
        public MSphere SetRadius(MBase r) { m_radius = r; return this; }
        public MSphere SetRadius(float r) { m_radius = new MConstant(r); return this; }
        public MSphere Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_cx));
            bufferDatas.Add(new ValueBufferData(1, m_cy));
            bufferDatas.Add(new ValueBufferData(2, m_cz));
            bufferDatas.Add(new ValueBufferData(3, m_cw));
            bufferDatas.Add(new ValueBufferData(4, m_radius));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KSphereMain2D");

        protected override int K3DId => Shader.FindKernel("KSphereMain3D");

        protected override int K4DId => Shader.FindKernel("KSphereMain4D");

        protected override string ResultBufferName => "KSphereOutputBuffer";
    }
}