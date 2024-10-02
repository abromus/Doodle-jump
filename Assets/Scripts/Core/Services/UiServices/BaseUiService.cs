using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal abstract class BaseUiService : MonoBehaviour, IUiService
    {
        public abstract UiServiceType UiServiceType { get; }
    }
}
