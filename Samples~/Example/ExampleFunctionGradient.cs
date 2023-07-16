using ANoiseGPU;

public class ExampleFunctionGradient : Example
{
    public float Spacing = 0.01f;
    public EFunctionGradientAxis Axis;

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
            MAutoCorrect autoCorrect1 = new MAutoCorrect()
            .SetSource(fractal)
            .SetResolution(Width)
            .SetRange(0, 1)
            .Build();
            MFunctionGradient functionGradient = new MFunctionGradient()
            .SetSource(fractal)
            .SetSpacing(Spacing)
            .SetAxis(Axis)
            .Build();
            MAutoCorrect autoCorrect2 = new MAutoCorrect()
            .SetSource(functionGradient)
            .SetResolution(Width)
            .SetRange(0, 1)
            .Build();
            Complete(autoCorrect2);
        });

        DrawImage();
    }
}