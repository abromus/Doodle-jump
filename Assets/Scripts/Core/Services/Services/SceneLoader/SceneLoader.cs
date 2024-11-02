namespace DoodleJump.Core.Services
{
    internal sealed class SceneLoader : ISceneLoader
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Load(in SceneInfo sceneInfo)
        {
            LoadScene(sceneInfo).Forget();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Destroy() { }

        private async Cysharp.Threading.Tasks.UniTaskVoid LoadScene(SceneInfo sceneInfo)
        {
            if (IsSceneLoaded(sceneInfo.Name))
            {
                sceneInfo.Success.SafeInvoke();

                return;
            }

            if (sceneInfo.IsLoadAsync)
            {
                var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneInfo.Name, sceneInfo.Mode);
                operation.allowSceneActivation = true;

                await Cysharp.Threading.Tasks.UniTask.WaitUntil(() => operation.isDone);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneInfo.Name, sceneInfo.Mode);
            }

            var currentScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneInfo.Name);

            if (sceneInfo.IsActive)
                UnityEngine.SceneManagement.SceneManager.SetActiveScene(currentScene);

            sceneInfo.Success.SafeInvoke();
        }

        private bool IsSceneLoaded(string name)
        {
            var sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);

                if (scene.name.Equals(name))
                    return true;
            }

            return false;
        }
    }
}
