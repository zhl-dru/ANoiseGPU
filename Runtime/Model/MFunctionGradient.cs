namespace ANoiseGPU
{
    public class MFunctionGradient : MBase
    {
        private MBase m_source;
        private float m_spacing = 0.01f;
        private EFunctionGradientAxis m_axis;

        private const string c_spacing = "fg_spacing";
        private const string c_axis = "fg_axis";

        private const string c_domain2DName1 = "KGradientCoordMinusMain2D";
        private const string c_domain3DName1 = "KGradientCoordMinusMain3D";
        private const string c_domain4DName1 = "KGradientCoordMinusMain4D";
        private const string c_domain2DName2 = "KGradientCoordPlusMain2D";
        private const string c_domain3DName2 = "KGradientCoordPlusMain3D";
        private const string c_domain4DName2 = "KGradientCoordPlusMain4D";

        public MFunctionGradient SetSource(MBase source) { m_source = source; return this; }
        public MFunctionGradient SetSource(float source) { m_source = new MConstant(source); return this; }
        public MFunctionGradient SetSpacing(float spacing) { m_spacing = spacing; return this; }
        public MFunctionGradient SetAxis(EFunctionGradientAxis axis) { m_axis = axis; return this; }
        public MFunctionGradient Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_source, c_domain2DName1, c_domain3DName1, c_domain4DName1));
            bufferDatas.Add(new ValueBufferData(1, m_source, c_domain2DName2, c_domain3DName2, c_domain4DName2));
            return this;
        }

        protected override int K2DId => Shader.FindKernel("KFunctionGradientMain");

        protected override int K3DId => Shader.FindKernel("KFunctionGradientMain");

        protected override int K4DId => Shader.FindKernel("KFunctionGradientMain");

        protected override string ResultBufferName => "KFGOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetFloat(c_spacing, m_spacing);
            Shader.SetInt(c_axis, (int)m_axis);
        }
    }

    public enum EFunctionGradientAxis
    {
        X_AXIS,
        Y_AXIS,
        Z_AXIS,
        W_AXIS,
        U_AXIS,
        V_AXIS
    }
}
