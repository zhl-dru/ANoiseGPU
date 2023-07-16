using System;
using UnityEngine;

namespace ANoiseGPU
{
    public class MAutoCorrect : MBase
    {
        private MBase m_source;
        private int m_resolution;
        private float m_low, m_high;
        private float m_scale2, m_offset2;
        private float m_scale3, m_offset3;
        private float m_scale4, m_offset4;

        private const string c_low = "ac_low";
        private const string c_high = "ac_high";
        private const string c_scale2 = "ac_scale2";
        private const string c_offset2 = "ac_offset2";
        private const string c_scale3 = "ac_scale3";
        private const string c_offset3 = "ac_offset3";
        private const string c_scale4 = "ac_scale4";
        private const string c_offset4 = "ac_offset4";

        public MAutoCorrect SetSource(MBase source) { m_source = source; return this; }
        public MAutoCorrect SetResolution(int resolution) { m_resolution = resolution; return this; }
        public MAutoCorrect SetRange(float low, float high) { m_low = low; m_high = high; return this; }
        public MAutoCorrect Build()
        {
            Evaluation();
            bufferDatas.Add(new ValueBufferData(0, m_source));
            return this;
        }

        private void Evaluation()
        {
            float[] data = new float[m_resolution];
            float mn, mx, v;

            ComputeBuffer b2 = m_source.Get2(m_resolution);
            b2.GetData(data);
            Dispose();
            b2.Dispose();
            Array.Sort(data);
            // Calculate 2D
            mn = 10000f; mx = -10000f;
            v = data[0]; if (v < mn) mn = v;
            v = data[m_resolution - 1]; if (v > mx) mx = v;
            m_scale2 = (m_high - m_low) / (mx - mn);
            m_offset2 = m_low - mn * m_scale2;

            ComputeBuffer b3 = m_source.Get3(m_resolution);
            b3.GetData(data);
            Dispose();
            b3.Dispose();
            Array.Sort(data);
            // Calculate 3D
            mn = 10000f; mx = -10000f;
            v = data[0]; if (v < mn) mn = v;
            v = data[m_resolution - 1]; if (v > mx) mx = v;
            m_scale3 = (m_high - m_low) / (mx - mn);
            m_offset3 = m_low - mn * m_scale3;

            ComputeBuffer b4 = m_source.Get4(m_resolution);
            b4.GetData(data);
            Dispose();
            b4.Dispose();
            Array.Sort(data);
            // Calculate 4D
            mn = 10000f; mx = -10000f;
            v = data[0]; if (v < mn) mn = v;
            v = data[m_resolution - 1]; if (v > mx) mx = v;
            m_scale4 = (m_high - m_low) / (mx - mn);
            m_offset4 = m_low - mn * m_scale4;
        }

        protected override int K2DId => Shader.FindKernel("KAutoCorrectMain2D");

        protected override int K3DId => Shader.FindKernel("KAutoCorrectMain3D");

        protected override int K4DId => Shader.FindKernel("KAutoCorrectMain4D");

        protected override string ResultBufferName => "KAutoCorrectOutputBuffer";

        protected override void SetVariableData()
        {
            Shader.SetFloat(c_low, m_low);
            Shader.SetFloat(c_high, m_high);
            Shader.SetFloat(c_scale2, m_scale2);
            Shader.SetFloat(c_offset2, m_offset2);
            Shader.SetFloat(c_scale3, m_scale3);
            Shader.SetFloat(c_offset3, m_offset3);
            Shader.SetFloat(c_scale4, m_scale4);
            Shader.SetFloat(c_offset4, m_offset4);
        }
    }
}