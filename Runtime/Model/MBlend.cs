using UnityEngine;

namespace ANoiseGPU
{
    public class MBlend : MBase
    {
        private MBase m_control;
        private MBase m_low;
        private MBase m_high;

        public MBlend SetControlSource(MBase control) { m_control = control; return this; }
        public MBlend SetLowSource(MBase low) { m_low = low; return this; }
        public MBlend SetHighSource(MBase high) { m_high = high; return this; }
        public MBlend SetControlSource(float control) { m_control = new MConstant(control); return this; }
        public MBlend SetLowSource(float low) { m_low = new MConstant(low); return this; }
        public MBlend SetHighSource(float high) { m_high = new MConstant(high); return this; }
        public MBlend Build()
        {
            bufferDatas.Add(new ValueBufferData(0, m_control));
            bufferDatas.Add(new ValueBufferData(1, m_low));
            bufferDatas.Add(new ValueBufferData(2, m_high));
            return this;
        }

        //public ComputeBuffer Get(params ValueBufferData[] datas)
        //{
        //    for(int i = 0; i < datas.Length; i++)
        //    {
        //        ValueBufferData data= datas[i];
        //        ComputeBuffer buffer = data.Module.Get2();
        //        Shader.SetBuffer(i, "", buffer);
        //    }
        //    ComputeBuffer cbuffer = new ComputeBuffer();
        //    Shader.SetBuffer(kId, Config.GetCoordBufferName(dtype), coordBuffer);
        //    Shader.SetBuffer(kId, ResultBufferName, buffer);
        //    Shader.Dispatch(kId, numthreads, numthreads, 1);
        //    return bufferDatas;
        //}


        protected override int K2DId => Shader.FindKernel("KBlendMain");

        protected override int K3DId => Shader.FindKernel("KBlendMain");

        protected override int K4DId => Shader.FindKernel("KBlendMain");

        protected override string ResultBufferName => "KBlendOutputBuffer";
    }
}