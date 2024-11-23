using DoodleJump.Core;

namespace DoodleJump.Game.Factories
{
    internal sealed class BoosterFactory : IBoosterFactory
    {
        public Worlds.Entities.Boosters.IBooster Create<T>(T boosterPrefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Entities.Boosters.IBooster
        {
            var booster = UnityEngine.Object.Instantiate(boosterPrefab, container);
            booster.gameObject.RemoveCloneSuffix();

            return booster;
        }
    }
}
