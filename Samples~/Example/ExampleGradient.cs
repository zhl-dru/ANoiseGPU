using ANoiseGPU;

public class ExampleGradient : Example
{
    public float X1 = 0.5f;
    public float X2 = 1.0f;
    public float Y1 = 0.0f;
    public float Y2 = 1.0f;
    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MGradient gradient = new MGradient()
            .SetX(X1, X2)
            .SetY(Y1, Y2)
            .SetZ(0, 0)
            .SetW(0, 0)
            .Build();
            Complete(gradient);
        });

        DrawImage();
    }
}
