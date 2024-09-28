namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class ShootingStrategyResolver : IShootingStrategyResolver
    {
        private IShootingStrategy _shootingStrategy;

        public IShootingStrategy ShootingStrategy => _shootingStrategy;

        internal ShootingStrategyResolver(Core.Services.ICameraService cameraService, Settings.IDoodlerConfig doodlerConfig)
        {
            Init(cameraService, doodlerConfig);
        }

        private void Init(Core.Services.ICameraService cameraService, Settings.IDoodlerConfig doodlerConfig)
        {
            var shootingMode = doodlerConfig.ShootingMode;

            switch (shootingMode)
            {
                case ShootingMode.Around:
                    _shootingStrategy = new AroundShootingStrategy(cameraService);
                    break;
                case ShootingMode.Upwards:
                    _shootingStrategy = new UpwardsShootingStrategy();
                    break;
                case ShootingMode.Cone:
                    _shootingStrategy = new ConeShootingStrategy(cameraService, doodlerConfig.MaxAngle);
                    break;
                default:
                    break;
            }
        }
    }
}
