using ANoiseGPU;

public class ExampleRotateDomain : Example
{
    public float Angle = 0.5f;

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
            MRotateDomain rotateDomain = new MRotateDomain()
            .SetSource(autoCorrect)
            .SetAngle(Angle)
            .SetAxisXYZ(0, 0, 0)
            .Build();

            Complete(rotateDomain);
        });

        DrawImage();
    }
}
