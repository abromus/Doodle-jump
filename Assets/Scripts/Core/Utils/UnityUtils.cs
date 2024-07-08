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

        private sealed class Keys
        {
            internal const string Empty = "";
            internal const string CloneSuffix = "(Clone)";
        }
    }
}
