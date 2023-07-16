using ANoiseGPU;

public class ExampleGain : Example
{
    public float Gain = 0.2f;

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
            MGain gain = new MGain()
            .SetSource(autoCorrect)
            .SetGain(Gain)
            .Build();
            Complete(gain);
        });

        DrawImage();
    }
}
