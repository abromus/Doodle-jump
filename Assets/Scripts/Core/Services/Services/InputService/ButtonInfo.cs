using System;

namespace DoodleJump.Core.Services
{
    [Serializable]
    public struct ButtonInfo
    {
        public KeyName Name;

        public ButtonPressType PressType;

        public ButtonRememberType RememberType;
    }
}
