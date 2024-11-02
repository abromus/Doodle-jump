namespace DoodleJump.Core
{
    public static class DelegateUtils
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void SafeInvoke(this System.Action block)
        {
            block?.Invoke();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void SafeInvoke<T>(this System.Action<T> block, T value)
        {
            block?.Invoke(value);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static void SafeInvoke<T1, T2>(this System.Action<T1, T2> block, T1 value1, T2 value2)
        {
            block?.Invoke(value1, value2);
        }
    }
}
