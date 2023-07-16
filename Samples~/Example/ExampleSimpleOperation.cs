using ANoiseGPU;

public class ExampleSimpleOperation : Example
{
    public float V1 = 0.1f;
    public float V2 = 0.5f;
    public float V3 = 0.7f;
    public float V4 = 0.3f;
    public float V5 = 2.0f;
    public float V6 = 4.0f;


    protected override void Generate()
    {
        base.Generate();

        ModuleRun(() =>
        {
            // r = ((v1 + v2 + v3 - v4) * v5) / v6;

            MConstant v1 = new MConstant().SetValue(V1).Build();
            MConstant v2 = new MConstant().SetValue(V2).Build();
            MConstant v3 = new MConstant().SetValue(V3).Build();
            MConstant v4 = new MConstant().SetValue(V4).Build();
            MConstant v5 = new MConstant().SetValue(V5).Build();
            MConstant v6 = new MConstant().SetValue(V6).Build();

            // 1
            MAdd step1 = new MAdd()
            .SetSource1(v1)
            .SetSource2(v2)
            .Build();
            MAdd step2 = new MAdd()
            .SetSource1(step1)
            .SetSource2(v3)
            .Build();
            MSub step3 = new MSub()
            .SetSource1(step2)
            .SetSource2(v4)
            .Build();
            MMult step4 = new MMult()
            .SetSource1(step3)
            .SetSource2(v5)
            .Build();
            MDiv step5 = new MDiv()
            .SetSource1(step4)
            .SetSource2(v6)
            .Build();

            // 2
            MBase r = ((v1 + v2 + v3 - v4) * v5) / v6;

            Complete(r);
        });

        DrawImage();
    }
}
