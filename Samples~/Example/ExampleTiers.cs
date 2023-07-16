using ANoiseGPU;

public class ExampleTiers : Example
{
    public int NumTiers = 5;
    public bool Smooth = true;

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
            MTiers tiers = new MTiers()
            .SetSource(autoCorrect)
            .SetNumTiers(NumTiers)
            .SetSmooth(Smooth)
            .Build();

            Complete(tiers);
        });

        DrawImage();
    }
}
