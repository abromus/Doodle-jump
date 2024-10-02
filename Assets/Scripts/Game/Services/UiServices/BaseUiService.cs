using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal abstract class BaseUiService : MonoBehaviour, IUiService
    {
        public abstract UiServiceType UiServiceType { get; }
    }
}
