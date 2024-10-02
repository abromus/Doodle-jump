namespace DoodleJump.Game.Data
{
    internal static class DataExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IPlayerData GetPlayerData(this IPersistentDataStorage persistentDataStorage)
        {
            return persistentDataStorage.GetData<IPlayerData>();
        }
    }
}
