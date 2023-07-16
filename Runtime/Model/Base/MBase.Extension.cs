namespace ANoiseGPU
{
    public abstract partial class MBase
    {
        public static MAdd operator +(MBase m1, MBase m2)
        {
            return new MAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MSub operator -(MBase m1, MBase m2)
        {
            return new MSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MMult operator *(MBase m1, MBase m2)
        {
            return new MMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MDiv operator /(MBase m1, MBase m2)
        {
            return new MDiv().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MAdd operator +(float m1, MBase m2)
        {
            return new MAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MSub operator -(float m1, MBase m2)
        {
            return new MSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MMult operator *(float m1, MBase m2)
        {
            return new MMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MDiv operator /(float m1, MBase m2)
        {
            return new MDiv().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MAdd operator +(MBase m1, float m2)
        {
            return new MAdd().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MSub operator -(MBase m1, float m2)
        {
            return new MSub().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MMult operator *(MBase m1, float m2)
        {
            return new MMult().SetSource1(m1).SetSource2(m2).Build();
        }
        public static MDiv operator /(MBase m1, float m2)
        {
            return new MDiv().SetSource1(m1).SetSource2(m2).Build();
        }
    }

    public static partial class MBaseExtension
    {
        public static MCos cos(this MBase source)
        {
            return new MCos().SetSource(source).Build();
        }
        public static MClamp clamp(this MBase source, float low, float high)
        {
            return new MClamp().SetSource(source).SetRange(low, high).Build();
        }
        public static MFloor floor(this MBase source)
        {
            return new MFloor().SetSource(source).Build();
        }
        public static MSin sin(this MBase source)
        {
            return new MSin().SetSource(source).Build();
        }
        public static MTiers tiers(this MBase source, int numtiers, bool smooth)
        {
            return new MTiers().SetSource(source).SetNumTiers(numtiers).SetSmooth(smooth).Build();
        }
        public static MFunctionGradient fg(this MBase source, float spacing, EFunctionGradientAxis axis)
        {
            return new MFunctionGradient().SetSource(source).SetSpacing(spacing).SetAxis(axis).Build();
        }
        public static MMax max(this MBase source, MBase other)
        {
            return new MMax().SetSource1(source).SetSource2(other).Build();
        }
        public static MMax max(this MBase source, float other)
        {
            return new MMax().SetSource1(source).SetSource2(other).Build();
        }
        public static MMin min(this MBase source, MBase other)
        {
            return new MMin().SetSource1(source).SetSource2(other).Build();
        }
        public static MMin min(this MBase source, float other)
        {
            return new MMin().SetSource1(source).SetSource2(other).Build();
        }
        public static MAutoCorrect ac(this MBase source, int resolution, float low, float high)
        {
            return new MAutoCorrect().SetSource(source).SetResolution(resolution).SetRange(low, high).Build();
        }
        public static MBias bias(this MBase source, MBase bias)
        {
            return new MBias().SetSource(source).SetBias(bias).Build();
        }
        public static MBias bias(this MBase source, float bias)
        {
            return new MBias().SetSource(source).SetBias(bias).Build();
        }
        public static MGain gain(this MBase source, MBase gain)
        {
            return new MGain().SetSource(source).SetGain(gain).Build();
        }
        public static MGain gain(this MBase source, float gain)
        {
            return new MGain().SetSource(source).SetGain(gain).Build();
        }
        public static MNormalizeCoords nc(this MBase source, MBase length)
        {
            return new MNormalizeCoords().SetSource(source).SetLength(length).Build();
        }
        public static MNormalizeCoords nc(this MBase source, float length)
        {
            return new MNormalizeCoords().SetSource(source).SetLength(length).Build();
        }
        public static MPow pow(this MBase source, MBase power)
        {
            return new MPow().SetSource(source).SetPower(power).Build();
        }
        public static MPow pow(this MBase source, float power)
        {
            return new MPow().SetSource(source).SetPower(power).Build();
        }
        public static MSawtooth sawtooth(this MBase source, MBase period)
        {
            return new MSawtooth().SetSource(source).SetPeriod(period).Build();
        }
        public static MSawtooth sawtooth(this MBase source, float period)
        {
            return new MSawtooth().SetSource(source).SetPeriod(period).Build();
        }
    }
}
