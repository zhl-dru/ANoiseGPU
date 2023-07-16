using ANoiseGPU;

public class ExampleFloor : Example
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
            MMult mult = new MMult()
            .SetSource1(autoCorrect)
            .SetSource2(8)
            .Build();
            MFloor floor = new MFloor()
            .SetSource(mult)
            .Build();
            MAutoCorrect floorAutoCorrect = new MAutoCorrect()
            .SetSource(floor)
            .SetResolution(Width)
            .SetRange(0, 1)
            .Build();
            Complete(floorAutoCorrect);
        });

        DrawImage();
    }
}
