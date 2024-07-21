using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal abstract class UiService : MonoBehaviour, IUiService
    {
        public abstract UiServiceType UiServiceType { get; }
    }
}
