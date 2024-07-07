using System.Collections.Generic;

namespace DoodleJump.Core.Factories
{
    internal static class FactoryExtensions
    {
        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IGameSceneControllerFactory>();
        }

        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IGameSceneControllerFactory>(UiFactoryType.GameSceneControllerFactory);
        }

        internal static TFactory GetFactory<TFactory>(this IReadOnlyList<IUiFactory> uiFactories, UiFactoryType factoryType) where TFactory : class, IFactory
        {
            foreach (var factory in uiFactories)
                if (factory.UiFactoryType == factoryType)
                    return factory as TFactory;

            return null;
        }
    }
}
