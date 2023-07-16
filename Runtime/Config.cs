using UnityEngine;

namespace ANoiseGPU
{
    public static class Config
    {
        private const string input0 = "InputBuffer0";
        private const string input1 = "InputBuffer1";
        private const string input2 = "InputBuffer2";
        private const string input3 = "InputBuffer3";
        private const string input4 = "InputBuffer4";
        private const string input5 = "InputBuffer5";
        private const string input6 = "InputBuffer6";
        private const string input7 = "InputBuffer7";
        private const string input8 = "InputBuffer8";
        private const string input9 = "InputBuffer9";

        private const string coord2d = "Coord2Buffer";
        private const string coord3d = "Coord3Buffer";
        private const string coord4d = "Coord4Buffer";

        private const string domain2d = "DomainCoord2Buffer";
        private const string domain3d = "DomainCoord3Buffer";
        private const string domain4d = "DomainCoord4Buffer";





        public static string GetInputBufferName(int i)
        {
            switch (i)
            {
                case 0: return input0;
                case 1: return input1;
                case 2: return input2;
                case 3: return input3;
                case 4: return input4;
                case 5: return input5;
                case 6: return input6;
                case 7: return input7;
                case 8: return input8;
                case 9: return input9;
                default:
                    Debug.LogError(string.Format("不支持的第{0}个缓冲输入", i));
                    return "";
            }
        }

        public static string GetCoordBufferName(DimensionType dtype)
        {
            switch (dtype)
            {
                case DimensionType._2D: return coord2d;
                case DimensionType._3D: return coord3d;
                case DimensionType._4D: return coord4d;
                default: return coord2d;
            }
        }

        public static string GetDomainCoordBufferName(DimensionType dtype)
        {
            switch (dtype)
            {
                case DimensionType._2D: return domain2d;
                case DimensionType._3D: return domain3d;
                case DimensionType._4D: return domain4d;
                default: return coord2d;
            }
        }
    }
}