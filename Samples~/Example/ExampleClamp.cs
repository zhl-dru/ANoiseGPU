using ANoiseGPU;

public class ExampleClamp : Example
{
    public float Low = 0.2f;
    public float High = 0.8f;

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
            MClamp clamp = new MClamp()
            .SetSource(autoCorrect)
            .SetRange(Low, High)
            .Build();
            Complete(clamp);
        });

        DrawImage();
    }
}
