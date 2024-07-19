using System;
using UnityEngine;

namespace DoodleJump.Core
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public sealed class SeparatorAttribute : BaseAttribute
    {
        private readonly float _height;
        private readonly float _spacing;
        private readonly Color _color;

        public float Height => _height;

        public float Spacing => _spacing;

        public Color Color => _color;

        public SeparatorAttribute() : this(1f, 5f, Color.white) { }

        public SeparatorAttribute(float height, float width, Color color)
        {
            _height = height;
            _spacing = width;
            _color = color;
        }
    }
}
