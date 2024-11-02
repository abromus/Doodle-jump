namespace DoodleJump.Core
{
    internal sealed class Bootstrapper : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private CoreSceneController _coreSceneController;

        private void Awake()
        {
            _coreSceneController.CreateCoreData();
        }
    }
}
