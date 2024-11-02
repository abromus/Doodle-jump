namespace DoodleJump.Core.Services
{
    internal sealed class EventSystemService : BaseUiService, IEventSystemService
    {
        [UnityEngine.SerializeField] private UnityEngine.EventSystems.EventSystem _eventSystemPrefab;

        private readonly System.Collections.Generic.Dictionary<UnityEngine.SceneManagement.Scene, UnityEngine.EventSystems.EventSystem> _eventSystems = new(8);

        public bool AddTo(in UnityEngine.SceneManagement.Scene scene)
        {
            if (_eventSystems.ContainsKey(scene))
                return false;

            var eventSystem = InstantiateEventSystem(_eventSystemPrefab, in scene);

            _eventSystems.Add(scene, eventSystem);

            return true;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Detach(in UnityEngine.SceneManagement.Scene scene)
        {
            _eventSystems.Remove(scene);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public UnityEngine.EventSystems.EventSystem Get(in UnityEngine.SceneManagement.Scene scene)
        {
            return _eventSystems.ContainsKey(scene) ? _eventSystems[scene] : null;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene)
        {
            return _eventSystems.TryGetValue(scene, out var eventSystem) && eventSystem.IsPointerOverGameObject();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool IsPointerOverGameObject(in UnityEngine.SceneManagement.Scene scene, int fingerId)
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

        private UnityEngine.EventSystems.EventSystem InstantiateEventSystem(UnityEngine.EventSystems.EventSystem eventSystemPrefab, in UnityEngine.SceneManagement.Scene scene)
        {
            var eventSystem = Instantiate(eventSystemPrefab);
            eventSystem.gameObject.RemoveCloneSuffix();

            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(eventSystem.gameObject, scene);

            eventSystem.transform.SetAsLastSibling();

            return eventSystem;
        }
    }
}
