using System;

namespace DoodleJump.Core
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : BaseAttribute
    {
        private readonly string _methodName;

        public string MethodName => _methodName;

        public ButtonAttribute() : this(string.Empty) { }

        public ButtonAttribute(string methodName)
        {
            _methodName = methodName;
        }
    }
}
