namespace DoodleJump.Game.Data
{
    internal static class DataExtensions
    {
        public static IPlayerData GetPlayerData(this IPersistentDataStorage persistentDataStorage)
        {
            return persistentDataStorage.GetData<IPlayerData>();
        }
    }
}
