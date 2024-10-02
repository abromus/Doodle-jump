using UnityEngine;

namespace DoodleJump.Core.Factories
{
    public abstract class BaseUiFactory : MonoBehaviour, IUiFactory
    {
        public abstract UiFactoryType UiFactoryType { get; }

        public virtual void Destroy() { }
    }
}
