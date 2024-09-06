using System.Collections.Generic;
using DoodleJump.Core.Factories;

namespace DoodleJump.Game.Factories
{
    internal static class FactoryExtensions
    {
        internal static IDoodlerFactory GetDoodlerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IDoodlerFactory>();
        }

        internal static IDoodlerFactory GetDoodlerFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IDoodlerFactory>(UiFactoryType.DoodlerFactory);
        }

        internal static IPlatformTriggerFactory GetPlatformTriggerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IPlatformTriggerFactory>();
        }

        internal static IEnemyTriggerFactory GetEnemyTriggerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IEnemyTriggerFactory>();
        }

        internal static IWorldFactory GetWorldFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IWorldFactory>();
        }

        internal static IWorldFactory GetWorldFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IWorldFactory>(UiFactoryType.WorldFactory);
        }
    }
}
