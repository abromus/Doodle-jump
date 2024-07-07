using System;

namespace DoodleJump.Core
{
    internal static class DelegateUtils
    {
        internal static void SafeInvoke(this Action block)
        {
            block?.Invoke();
        }

        internal static void SafeInvoke<T>(this Action<T> block, T value)
        {
            block?.Invoke(value);
        }

        internal static void SafeInvoke<T1, T2>(this Action<T1, T2> block, T1 value1, T2 value2)
        {
            block?.Invoke(value1, value2);
        }
    }
}
