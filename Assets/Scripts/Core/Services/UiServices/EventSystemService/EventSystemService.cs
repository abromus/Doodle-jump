using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.Services
{
    internal sealed class EventSystemService : BaseUiService, IEventSystemService
    {
        [SerializeField] private EventSystem _eventSystemPrefab;

        private readonly Dictionary<Scene, EventSystem> _eventSystems = new(8);

        public override UiServiceType UiServiceType => UiServiceType.EventSystemService;

        public bool AddTo(Scene scene)
        {
            if (_eventSystems.ContainsKey(scene))
                return false;

            var eventSystem = InstantiateEventSystem(_eventSystemPrefab, scene);

            _eventSystems.Add(scene, eventSystem);

            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Detach(Scene scene)
        {
            _eventSystems.Remove(scene);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public EventSystem Get(Scene scene)
        {
            return _eventSystems.ContainsKey(scene) ? _eventSystems[scene] : null;
        }

        public void Destroy()
        {
            foreach (var eventSystem in _eventSystems.Values)
                if (eventSystem != null)
                    Destroy(eventSystem.gameObject);

            _eventSystems.Clear();
        }

        private EventSystem InstantiateEventSystem(EventSystem eventSystemPrefab, Scene scene)
        {
            var eventSystem = Instantiate(eventSystemPrefab);
            eventSystem.gameObject.RemoveCloneSuffix();

            SceneManager.MoveGameObjectToScene(eventSystem.gameObject, scene);

            eventSystem.transform.SetAsLastSibling();

            return eventSystem;
        }
    }
}
