using ANoiseGPU;

public class ExampleFractal : Example
{
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
            Complete(fractal);
        });

        DrawImage();
    }
}
