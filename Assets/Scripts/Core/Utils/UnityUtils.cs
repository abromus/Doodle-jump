using System;
using UnityEngine;

namespace DoodleJump.Core
{
    public static class UnityUtils
    {
        public static void RemoveCloneSuffix(this GameObject value)
        {
            if (value == null)
                return;

            var name = value.name.Replace(Keys.CloneSuffix, Keys.Empty);
            value.name = name;
        }

        public static Color GetColor(this CustomColor color)
        {
            var colorSpan = ((int)color).ToString(Keys.Hex).AsSpan();
            var startIndex = 0;
            var endIndex = 2;
            var offset = 2;

            var r = GetColorValue(colorSpan, startIndex, endIndex);
            startIndex = endIndex;
            endIndex += offset;
            var g = GetColorValue(colorSpan, startIndex, endIndex);
            startIndex = endIndex;
            endIndex += offset;
            var b = GetColorValue(colorSpan, startIndex, endIndex);

            return new Color(r, g, b);
        }

        private static float GetColorValue(ReadOnlySpan<char> colorSpan, int index, int count)
        {
            var multiplier = 1f / byte.MaxValue;
            var style = System.Globalization.NumberStyles.HexNumber;
            var color = int.Parse(colorSpan[index..count], style) * multiplier;

            return color;
        }

        private sealed class Keys
        {
            internal const string Empty = "";
            internal const string CloneSuffix = "(Clone)";
            internal const string Hex = "X6";
        }
    }
}
