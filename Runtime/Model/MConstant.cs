namespace ANoiseGPU
{
    public class MConstant : MBase
    {
        private float m_value;

        private const string c_value = "constant_value";

        public MConstant SetValue(float value) { m_value = value; return this; }
        public MConstant Build() { return this; }
        public MConstant(float value) { m_value = value; }
        public MConstant() { }

        protected override int K2DId => Shader.FindKernel("KConstantMain");

        protected override int K3DId => Shader.FindKernel("KConstantMain");

        protected override int K4DId => Shader.FindKernel("KConstantMain");

        protected override string ResultBufferName => "KConstantOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetFloat(c_value, m_value);
        }
    }
}