using ANoiseGPU;

public class ExampleTranslateDomain : Example
{
    public float X = 1.0f;
    public float Y = 1.0f;

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
            MTranslateDomain translateDomain = new MTranslateDomain()
            .SetSource(autoCorrect)
            .SetAxisX(X)
            .SetAxisY(Y)
            .SetAxisZ(0)
            .SetAxisW(0)
            .Build();
            Complete(translateDomain);
        });

        DrawImage();
    }
}
