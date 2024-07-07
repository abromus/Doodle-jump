using System;
using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal interface ICameraService : IService
    {
        public Camera Camera { get; }

        public bool IsAttached { get; }

        public event Action<Transform> Attached;

        public void Init(Transform container);

        public void AttachTo(Transform parent);

        public void Detach();
    }
}
