namespace DoodleJump.Core.Services
{
    internal readonly struct SceneInfo
    {
        private readonly string _name;
        private readonly UnityEngine.SceneManagement.LoadSceneMode _mode;
        private readonly bool _isLoadAsync;
        private readonly bool _isActive;
        private readonly System.Action _success;

        internal readonly string Name => _name;

        internal readonly UnityEngine.SceneManagement.LoadSceneMode Mode => _mode;

        internal readonly bool IsLoadAsync => _isLoadAsync;

        internal readonly bool IsActive => _isActive;

        internal readonly System.Action Success => _success;

        internal SceneInfo(string name, UnityEngine.SceneManagement.LoadSceneMode mode, bool isLoadAsync, bool isActive, System.Action success)
        {
            _name = name;
            _mode = mode;
            _isLoadAsync = isLoadAsync;
            _isActive = isActive;
            _success = success;
        }
    }
}
