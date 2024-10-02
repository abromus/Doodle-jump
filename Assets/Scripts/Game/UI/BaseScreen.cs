using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal abstract class BaseScreen : MonoBehaviour, IScreen
    {
        [SerializeField] private ScreenType _screenType;

        public ScreenType ScreenType => _screenType;

        public abstract void Init(Data.IGameData gameData, Worlds.IWorldData worldData, IScreenSystemService screenSystemService);

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            if (this != null && gameObject != null)
                gameObject.SetActive(false);
        }
    }
}
