using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using ANoiseGPU;
using System.Diagnostics;
using System;

public class Example : MonoBehaviour
{
    public ComputeShader Shader;
    public RawImage Image;
    public int Width = 512;
    public NoiseSet NoiseSet;

    private float[] values;
    private Stopwatch stopwatch = new Stopwatch();

    [Button]
    protected virtual void Generate()
    {
        if (MBase.Shader == null)
        {
            MBase.Shader = Shader;
        }
    }

    protected void Complete(MBase module)
    {
        ComputeBuffer buffer = module.Get2(Width);
        values = new float[Width * Width];
        buffer.GetData(values);
        MBase.Dispose();
        buffer.Dispose();
    }
    protected void DrawImage()
    {
        Texture2D texture = new Texture2D(Width, Width);
        for (int y = 0, i = 0; y < Width; y++)
        {
            for (int x = 0; x < Width; x++, i++)
            {
                float v = values[i];
                Color c = new Color(v, v, v);
                texture.SetPixel(x, y, c);
            }
        }
        texture.Apply();
        Image.texture = texture;
    }

    protected void ModuleRun(Action action)
    {
        stopwatch.Restart();
        action.Invoke();
        stopwatch.Stop();
        UnityEngine.Debug.Log(string.Format("{0}สนำรมห{1}ms", GetType().Name, stopwatch.Elapsed.TotalMilliseconds));
    }
}

[Serializable]
public class NoiseSet
{
    public int Seed = 10000;
    public int Octave = 8;
    public float Frequency = 1f;
    public float Lacunarity = 2f;
    public float Gain = 0.5f;
    public NoiseType NType = NoiseType.SIMPLEX;
    public FractalType FType = FractalType.FBM;
}
