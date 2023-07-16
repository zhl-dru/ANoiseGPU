using ANoiseGPU;

public class ExampleAutoCorrect : Example
{
    public float Low = 0f;
    public float High = 1f;

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
            .SetRange(Low, High)
            .Build();
            Complete(autoCorrect);
        });

        DrawImage();
    }
}
