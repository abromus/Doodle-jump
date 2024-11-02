namespace DoodleJump.Game.Factories
{
    internal interface IBoosterTriggerFactory : Core.Factories.IFactory
    {
        public void Init(Data.IPersistentDataStorage persistentDataStorage, Worlds.Entities.IDoodler doodler);

        public Worlds.Boosters.IBoosterTrigger Create(Worlds.Boosters.IBoosterCollisionInfo info, Worlds.Boosters.BoosterTriggerType triggerType);

        public Worlds.Boosters.IBoosterTrigger Create(Worlds.Boosters.IBoosterCollisionInfo info, Settings.IWorldBoosterConfig worldBoosterConfig);
    }
}
