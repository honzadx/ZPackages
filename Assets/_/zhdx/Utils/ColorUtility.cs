using UnityEngine;

namespace zhdx
{
    namespace Utils
    {
        public static class ColorUtility
        {
            public static Color PushHue(Color color, float push)
            {
                float hue;
                float saturation;
                float value;
                Color.RGBToHSV(color, out hue, out saturation, out value);
                hue += push;
                if (hue > 1)
                    hue %= 1;
                if (hue < 0)
                    hue = 1 + (hue % 1);

                return Color.HSVToRGB(hue, saturation, value);
            }
        }
    }
}