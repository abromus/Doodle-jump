namespace DoodleJump.Game.Factories
{
    internal interface IBoosterFactory : Core.Factories.IFactory
    {
        public Worlds.Entities.Boosters.IBooster Create<T>(T boosterPrefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Entities.Boosters.IBooster;
    }
}
