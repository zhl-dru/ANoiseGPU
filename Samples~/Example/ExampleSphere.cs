using ANoiseGPU;

public class ExampleSphere : Example
{
    public float Cx = 0.5f;
    public float Cy = 0.5f;
    public float R = 0.5f;

    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MSphere sphere = new MSphere()
            .SetCenterX(Cx)
            .SetCenterY(Cy)
            .SetCenterZ(0)
            .SetCenterW(0)
            .SetRadius(R)
            .Build();

            Complete(sphere);
        });

        DrawImage();
    }
}
