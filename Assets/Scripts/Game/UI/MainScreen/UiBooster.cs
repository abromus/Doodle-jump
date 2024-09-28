using DoodleJump.Core;

namespace DoodleJump.Game.UI
{
    internal sealed class UiBooster : UnityEngine.MonoBehaviour, IUiBooster, IPoolable
    {
        [UnityEngine.SerializeField] private UnityEngine.UI.Button _button;
        [UnityEngine.SerializeField] private TMPro.TMP_Text _countText;

        private Worlds.Boosters.BoosterType _boosterType;
        private int _count;

        public UnityEngine.GameObject GameObject => gameObject;

        public Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public int Count => _count;

        public event System.Action<IUiBooster> Clicked;

        public void Init(Worlds.Boosters.BoosterType boosterType, int count)
        {
            _boosterType = boosterType;
            _count = count;

            _button.onClick.AddListener(OnClicked);

            UpdateCountView();

            gameObject.SetActive(true);
        }

        public void UpdateCount(int count)
        {
            _count = count;

            UpdateCountView();
        }

        public void Clear()
        {
            gameObject.SetActive(false);

            _button.onClick.RemoveListener(OnClicked);
        }

        private void UpdateCountView()
        {
            _countText.text = _count.ToString();
        }

        private void OnClicked()
        {
            Clicked.SafeInvoke(this);
        }
    }
}
