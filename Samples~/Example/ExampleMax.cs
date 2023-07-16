using ANoiseGPU;

public class ExampleMax : Example
{
    public float V1 = 0.1f;
    public float V2 = 0.5f;


    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            MConstant v1 = new MConstant().SetValue(V1).Build();
            MConstant v2 = new MConstant().SetValue(V2).Build();

            MMax max = new MMax()
            .SetSource1(v1)
            .SetSource2(v2)
            .Build();

            Complete(max);
        });

        DrawImage();
    }
}
