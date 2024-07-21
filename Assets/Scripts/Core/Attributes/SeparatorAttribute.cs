using System;

namespace DoodleJump.Core
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class SeparatorAttribute : BaseAttribute
    {
        private readonly float _height;
        private readonly float _spacing;
        private readonly CustomColor _color;

        public float Height => _height;

        public float Spacing => _spacing;

        public CustomColor Color => _color;

        public SeparatorAttribute() : this(CustomColor.Lime) { }

        public SeparatorAttribute(CustomColor color) : this(1f, 5f, color) { }

        public SeparatorAttribute(float height, float width, CustomColor color)
        {
            _height = height;
            _spacing = width;
            _color = color;
        }
    }
}
