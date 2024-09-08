namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IProjectile
    {
        public UnityEngine.GameObject GameObject { get; }

        public event System.Action<IProjectile> Destroyed;

        public void Init(Services.IAudioService audioService, Core.Services.IUpdater updater);

        public void InitPosition(UnityEngine.Vector3 position, float doodlerDirection, UnityEngine.Vector3 shootDirection);

        public void Clear();
    }
}
