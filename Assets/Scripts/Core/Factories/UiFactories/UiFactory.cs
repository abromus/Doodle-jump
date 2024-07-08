using UnityEngine;

namespace DoodleJump.Core.Factories
{
    public abstract class UiFactory : MonoBehaviour, IUiFactory
    {
        public abstract UiFactoryType UiFactoryType { get; }

        public virtual void Destroy() { }
    }
}
