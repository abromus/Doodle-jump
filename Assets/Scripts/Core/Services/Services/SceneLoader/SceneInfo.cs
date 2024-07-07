using System;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.Services
{
    internal readonly struct SceneInfo
    {
        private readonly string _name;
        private readonly LoadSceneMode _mode;
        private readonly bool _isLoadAsync;
        private readonly bool _isActive;
        private readonly Action _success;

        internal readonly string Name => _name;

        internal readonly LoadSceneMode Mode => _mode;

        internal bool IsLoadAsync => _isLoadAsync;

        internal readonly bool IsActive => _isActive;

        internal readonly Action Success => _success;

        internal SceneInfo(string name, LoadSceneMode mode, bool isLoadAsync, bool isActive, Action success)
        {
            _name = name;
            _mode = mode;
            _isLoadAsync = isLoadAsync;
            _isActive = isActive;
            _success = success;
        }
    }
}
