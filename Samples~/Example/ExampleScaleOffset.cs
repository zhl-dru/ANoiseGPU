using ANoiseGPU;

public class ExampleScaleOffset : Example
{
    public float Scale = 0.5f;
    public float Offset = 0.5f;

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
            MScaleOffset scaleOffset = new MScaleOffset()
            .SetSource(autoCorrect)
            .SetScale(Scale)
            .SetOffset(Offset)
            .Build();

            Complete(scaleOffset);
        });

        DrawImage();
    }
}
