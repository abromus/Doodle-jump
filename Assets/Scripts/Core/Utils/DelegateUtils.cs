using System;

namespace DoodleJump.Core
{
    public static class DelegateUtils
    {
        public static void SafeInvoke(this Action block)
        {
            block?.Invoke();
        }

        public static void SafeInvoke<T>(this Action<T> block, T value)
        {
            block?.Invoke(value);
        }

        public static void SafeInvoke<T1, T2>(this Action<T1, T2> block, T1 value1, T2 value2)
        {
            block?.Invoke(value1, value2);
        }
    }
}
