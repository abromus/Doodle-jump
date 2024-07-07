using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal abstract class UiService : MonoBehaviour, IUiService
    {
        public abstract UiServiceType UiServiceType { get; }
    }
}
