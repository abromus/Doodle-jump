using System.Collections.Generic;

namespace DoodleJump.Core.Factories
{
    public static class FactoryExtensions
    {
        public static TFactory GetFactory<TFactory>(this IReadOnlyList<IUiFactory> uiFactories, UiFactoryType factoryType) where TFactory : class, IFactory
        {
            foreach (var factory in uiFactories)
                if (factory.UiFactoryType == factoryType)
                    return factory as TFactory;

            return null;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IGameSceneControllerFactory>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IGameSceneControllerFactory>(UiFactoryType.GameSceneControllerFactory);
        }
    }
}
