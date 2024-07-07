using UnityEngine;

namespace DoodleJump.Core.Factories
{
    internal abstract class UiFactory : MonoBehaviour, IUiFactory
    {
        public abstract UiFactoryType UiFactoryType { get; }

        public virtual void Destroy() { }
    }
}
