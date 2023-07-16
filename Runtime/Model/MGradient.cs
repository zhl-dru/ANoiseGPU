namespace ANoiseGPU
{
    public class MGradient : MBase
    {
        private float m_gx1, m_gy1, m_gz1, m_gw1;
        private float m_gx2, m_gy2, m_gz2, m_gw2;
        private float m_x, m_y, m_z, m_w;
        private float m_vlen;

        private const string c_gx = "gradient_gx";
        private const string c_gy = "gradient_gy";
        private const string c_gz = "gradient_gz";
        private const string c_gw = "gradient_gw";
        private const string c_x = "gradient_x";
        private const string c_y = "gradient_y";
        private const string c_z = "gradient_z";
        private const string c_w = "gradient_w";
        private const string c_vlen = "gradient_vlen";

        public MGradient SetX(float x1, float x2) { m_gx1 = x1; m_gx2 = x2; return this; }
        public MGradient SetY(float y1, float y2) { m_gy1 = y1; m_gy2 = y2; return this; }
        public MGradient SetZ(float z1, float z2) { m_gz1 = z1; m_gz2 = z2; return this; }
        public MGradient SetW(float w1, float w2) { m_gw1 = w1; m_gw2 = w2; return this; }
        public MGradient SetXYZW(float x1, float x2, float y1, float y2, float z1, float z2, float w1, float w2)
        {
            m_gx1 = x1; m_gx2 = x2;
            m_gy1 = y1; m_gy2 = y2;
            m_gz1 = z1; m_gz2 = z2;
            m_gw1 = w1; m_gw2 = w2;
            return this;
        }
        public MGradient Build()
        {
            m_x = m_gx2 - m_gx1;
            m_y = m_gy2 - m_gy1;
            m_z = m_gz2 - m_gz1;
            m_w = m_gw2 - m_gw1;
            m_vlen = m_x * m_x + m_y * m_y + m_z * m_z + m_w * m_w;
            return this;
        }


        protected override int K2DId => Shader.FindKernel("KGradientMain2D");

        protected override int K3DId => Shader.FindKernel("KGradientMain3D");

        protected override int K4DId => Shader.FindKernel("KGradientMain4D");

        protected override string ResultBufferName => "KGradientOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetFloat(c_gx, m_gx1);
            Shader.SetFloat(c_gy, m_gy1);
            Shader.SetFloat(c_gz, m_gz1);
            Shader.SetFloat(c_gw, m_gw1);
            Shader.SetFloat(c_x, m_x);
            Shader.SetFloat(c_y, m_y);
            Shader.SetFloat(c_z, m_z);
            Shader.SetFloat(c_w, m_w);
            Shader.SetFloat(c_vlen, m_vlen);
        }
    }
}