using ANoiseGPU;

public class ExampleNormalizeCoords : Example
{
    public float length = 1f;

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
            MNormalizeCoords normalizeCoords = new MNormalizeCoords()
            .SetSource(fractal)
            .SetLength(length)
            .Build();
            MAutoCorrect autoCorrect = new MAutoCorrect()
            .SetSource(normalizeCoords)
            .SetResolution(Width)
            .SetRange(0, 1)
            .Build();
            Complete(autoCorrect);
        });

        DrawImage();
    }
}
