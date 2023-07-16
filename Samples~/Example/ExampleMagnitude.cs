using ANoiseGPU;

public class ExampleMagnitude : Example
{
    public float Y = 0.5f;

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
            MMagnitude magnitude = new MMagnitude()
            .SetX(autoCorrect)
            .SetY(Y)
            .SetZ(0)
            .SetW(0)
            .Build();

            Complete(magnitude);
        });

        DrawImage();
    }
}
