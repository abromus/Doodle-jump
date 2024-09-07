namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class QuantityPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private int _quantity;

        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        public int Quantity => _quantity;

        public QuantityPlatformCollisionInfo(IPlatform platform, int quantity)
        {
            _platform = platform;
            _quantity = quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            _quantity = quantity;
        }
    }
}
