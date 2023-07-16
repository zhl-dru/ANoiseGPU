using ANoiseGPU;

public class ExampleSelect : Example
{
    public NoiseSet NoiseSet2;
    public float X1 = 0f;
    public float X2 = 0f;
    public float Y1 = 0f;
    public float Y2 = 1f;
    public float Falloff = 0.5f;
    public float Threshold = 0.5f;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MFractal fractal1 = new MFractal()
            .SetSeed(NoiseSet.Seed)
             .SetOctave(NoiseSet.Octave)
             .SetFrequency(NoiseSet.Frequency)
             .SetLacunarity(NoiseSet.Lacunarity)
             .SetGain(NoiseSet.Gain)
             .SetNoiseType(NoiseSet.NType)
             .SetFractalType(NoiseSet.FType)
             .Build();
            MFractal fractal2 = new MFractal()
            .SetSeed(NoiseSet2.Seed)
             .SetOctave(NoiseSet2.Octave)
             .SetFrequency(NoiseSet2.Frequency)
             .SetLacunarity(NoiseSet2.Lacunarity)
             .SetGain(NoiseSet2.Gain)
             .SetNoiseType(NoiseSet2.NType)
             .SetFractalType(NoiseSet2.FType)
             .Build();
            MAutoCorrect autoCorrect1 = new MAutoCorrect()
            .SetResolution(Width)
            .SetSource(fractal1)
            .SetRange(0, 1)
            .Build();
            MAutoCorrect autoCorrect2 = new MAutoCorrect()
            .SetResolution(Width)
            .SetSource(fractal2)
            .SetRange(0, 1)
            .Build();
            MGradient gradient = new MGradient()
            .SetX(X1, X2)
            .SetY(Y1, Y2)
            .SetZ(0, 0)
            .SetW(0, 0)
            .Build();
            MSelect select = new MSelect()
            .SetLowSource(autoCorrect1)
            .SetHighSource(autoCorrect2)
            .SetControlSource(gradient)
            .SetFalloff(Falloff)
            .SetThreshold(Threshold)
            .Build();

            Complete(select);
        });

        DrawImage();
    }
}