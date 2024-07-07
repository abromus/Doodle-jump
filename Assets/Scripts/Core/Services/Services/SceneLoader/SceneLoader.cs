using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.Services
{
    internal sealed class SceneLoader : ISceneLoader
    {
        public void Load(SceneInfo sceneInfo)
        {
            LoadScene(sceneInfo);
        }

        public void Destroy() { }

        private async void LoadScene(SceneInfo sceneInfo)
        {
            if (IsSceneLoaded(sceneInfo.Name))
            {
                sceneInfo.Success.SafeInvoke();

                return;
            }

            if (sceneInfo.IsLoadAsync)
            {
                var operation = SceneManager.LoadSceneAsync(sceneInfo.Name, sceneInfo.Mode);
                operation.allowSceneActivation = true;

                await UniTask.WaitUntil(() => operation.isDone);
            }
            else
            {
                SceneManager.LoadScene(sceneInfo.Name, sceneInfo.Mode);
            }

            var currentScene = SceneManager.GetSceneByName(sceneInfo.Name);

            if (sceneInfo.IsActive)
                SceneManager.SetActiveScene(currentScene);

            sceneInfo.Success.SafeInvoke();
        }

        private bool IsSceneLoaded(string name)
        {
            var sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);

                if (scene.name.Equals(name))
                    return true;
            }

            return false;
        }
    }
}
