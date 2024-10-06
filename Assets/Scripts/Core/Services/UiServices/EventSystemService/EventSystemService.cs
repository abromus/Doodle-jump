using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DoodleJump.Core.Services
{
    internal sealed class EventSystemService : BaseUiService, IEventSystemService
    {
        [SerializeField] private EventSystem _eventSystemPrefab;

        private readonly Dictionary<UnityEngine.SceneManagement.Scene, EventSystem> _eventSystems = new(8);

        public override UiServiceType UiServiceType => UiServiceType.EventSystemService;

        public bool AddTo(UnityEngine.SceneManagement.Scene scene)
        {
            if (_eventSystems.ContainsKey(scene))
                return false;

            var eventSystem = InstantiateEventSystem(_eventSystemPrefab, scene);

            _eventSystems.Add(scene, eventSystem);

            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Detach(UnityEngine.SceneManagement.Scene scene)
        {
            _eventSystems.Remove(scene);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public EventSystem Get(UnityEngine.SceneManagement.Scene scene)
        {
            return _eventSystems.ContainsKey(scene) ? _eventSystems[scene] : null;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(UnityEngine.SceneManagement.Scene scene)
        {
            return _eventSystems.TryGetValue(scene, out var eventSystem) && eventSystem.IsPointerOverGameObject();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(UnityEngine.SceneManagement.Scene scene, int fingerId)
        {
            return _eventSystems.TryGetValue(scene, out var eventSystem) && eventSystem.IsPointerOverGameObject(fingerId);
        }

        public void Destroy()
        {
            foreach (var eventSystem in _eventSystems.Values)
                if (eventSystem != null)
                    Destroy(eventSystem.gameObject);

            _eventSystems.Clear();
        }

        private EventSystem InstantiateEventSystem(EventSystem eventSystemPrefab, UnityEngine.SceneManagement.Scene scene)
        {
            var eventSystem = Instantiate(eventSystemPrefab);
            eventSystem.gameObject.RemoveCloneSuffix();

            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(eventSystem.gameObject, scene);

            eventSystem.transform.SetAsLastSibling();

            return eventSystem;
        }
    }
}
