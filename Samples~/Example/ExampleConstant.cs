using ANoiseGPU;

public class ExampleConstant : Example
{
    public float Value;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MConstant constant = new MConstant()
            .SetValue(Value)
            .Build();
            Complete(constant);
        });

        DrawImage();
    }
}