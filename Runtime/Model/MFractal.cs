using Random = Unity.Mathematics.Random;

namespace ANoiseGPU
{
    public class MFractal : MBase
    {
        private NoiseType m_ntype;
        private FractalType m_ftype;
        private uint m_seed;
        private int m_octave;
        private float m_frequency;
        private float m_lacunarity;
        private float m_gain;

        private float m_ranseed;

        private const string c_ntype = "fractal_ntype";
        private const string c_ftype = "fractal_ftype";
        private const string c_seed = "fractal_seed";
        private const string c_octave = "fractal_octave";
        private const string c_frequency = "fractal_frequency";
        private const string c_lacunarity = "fractal_lacunarity";
        private const string c_gain = "fractal_gain";

        protected override int K2DId => Shader.FindKernel("KFractalMain2D");

        protected override int K3DId => Shader.FindKernel("KFractalMain3D");

        protected override int K4DId => Shader.FindKernel("KFractalMain4D");

        protected override string ResultBufferName => "KFractalOutputBuffer";

        public MFractal SetSeed(int seed) { m_seed = (uint)seed; return this; }
        public MFractal SetNoiseType(NoiseType ntype) { m_ntype = ntype; return this; }
        public MFractal SetFractalType(FractalType ftype) { m_ftype = ftype; return this; }
        public MFractal SetOctave(int octave) { m_octave = octave; return this; }
        public MFractal SetFrequency(float frequency) { m_frequency = frequency; return this; }
        public MFractal SetLacunarity(float lacunarity) { m_lacunarity = lacunarity; return this; }
        public MFractal SetGain(float gain) { m_gain = gain; return this; }
        public MFractal Build()
        {
            Random random = new Random(m_seed);
            m_ranseed = random.NextFloat();
            return this;
        }

        protected override void SetVariableData()
        {
            Shader.SetInt(c_ntype, (int)m_ntype);
            Shader.SetInt(c_ftype, (int)m_ftype);
            Shader.SetInt(c_seed, (int)m_seed);
            Shader.SetInt(c_octave, m_octave);
            Shader.SetFloat(c_frequency, m_frequency);
            Shader.SetFloat(c_lacunarity, m_lacunarity);
            Shader.SetFloat(c_gain, m_gain);
        }

    }

    public enum NoiseType
    {
        PERLIN = 0,
        SIMPLEX = 1
    }

    public enum FractalType
    {
        FBM = 0,
        BILLOWY = 1,
        RIDGED = 2
    }
}
