namespace DoodleJump.Core
{
    [UnityEngine.RequireComponent(typeof(UnityEngine.RectTransform))]
    public class SafeArea : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private SafeAreaType _safeAreaType = SafeAreaType.All;

        private UnityEngine.RectTransform _rectTransform;
        private UnityEngine.Rect _lastSafeArea;

        private void Awake()
        {
            _rectTransform = GetComponent<UnityEngine.RectTransform>();

            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            _lastSafeArea = UnityEngine.Screen.safeArea;

            var anchorMin = _lastSafeArea.position;
            var anchorMax = _lastSafeArea.position + _lastSafeArea.size;
            var screenWidth = UnityEngine.Screen.width;
            var screenHeight = UnityEngine.Screen.height;

            if ((_safeAreaType & SafeAreaType.Left) != 0)
                anchorMin.x = UnityEngine.Mathf.Clamp01(_lastSafeArea.xMin / screenWidth);

            if ((_safeAreaType & SafeAreaType.Right) != 0)
                anchorMax.x = UnityEngine.Mathf.Clamp01(_lastSafeArea.xMax / screenWidth);

            if ((_safeAreaType & SafeAreaType.Bottom) != 0)
                anchorMin.y = UnityEngine.Mathf.Clamp01(_lastSafeArea.yMin / screenHeight);

            if ((_safeAreaType & SafeAreaType.Top) != 0)
                anchorMax.y = UnityEngine.Mathf.Clamp01(_lastSafeArea.yMax / screenHeight);

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }

        [System.Flags]
        private enum SafeAreaType
        {
            None = 0,
            Left = 1 << 0,
            Right = 1 << 1,
            Top = 1 << 2,
            Bottom = 1 << 3,
            All = Left | Right | Top | Bottom
        }
    }
}
