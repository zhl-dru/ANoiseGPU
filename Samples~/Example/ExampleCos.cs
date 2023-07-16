using ANoiseGPU;

public class ExampleCos : Example
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
            MAutoCorrect autoCorrect = new MAutoCorrect()
            .SetResolution(Width)
            .SetSource(fractal)
            .SetRange(0, 1)
            .Build();
            MCos cos = new MCos()
            .SetSource(autoCorrect)
            .Build();

            Complete(cos);
        });

        DrawImage();
    }
}
