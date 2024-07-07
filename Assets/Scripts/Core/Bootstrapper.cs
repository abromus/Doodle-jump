using UnityEngine;

namespace DoodleJump.Core
{
    internal sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CoreSceneController _coreSceneController;

        private void Awake()
        {
            _coreSceneController.CreateGameData();
        }
    }
}
