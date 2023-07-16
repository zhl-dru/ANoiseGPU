using ANoiseGPU;

public class ExampleBrightContrast : Example
{
    public float Bright = -0.5f;
    public float Threshold = 0.25f;
    public float Factor = 2f;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MFractal fractal = new MFractal()
             .SetSeed(NoiseSet.Seed)
             .SetOctave(NoiseSet.Octave)
             .SetFrequency(NoiseSet.Frequency)
             .SetLacunarity(NoiseSet.Lacunarity)
             .SetGain(NoiseSet.Gain)
             .SetNoiseType(NoiseSet.NType)
             .SetFractalType(NoiseSet.FType)
             .Build();
            MAutoCorrect autoCorrect = new MAutoCorrect()
            .SetResolution(Width)
            .SetSource(fractal)
            .SetRange(0, 1)
            .Build();
            MBrightContrast brightContrast = new MBrightContrast()
            .SetSource(autoCorrect)
            .SetBright(Bright)
            .SetThreshold(Threshold)
            .SetFactor(Factor)
            .Build();
            Complete(brightContrast);
        });

        DrawImage();
    }
}
