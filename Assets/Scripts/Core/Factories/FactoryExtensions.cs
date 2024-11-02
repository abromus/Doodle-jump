namespace DoodleJump.Core.Factories
{
    public static class FactoryExtensions
    {
        public static TFactory GetFactory<TFactory>(this System.Collections.Generic.IReadOnlyList<IUiFactory> uiFactories) where TFactory : class, IFactory
        {
            foreach (var uiFactory in uiFactories)
                if (uiFactory is TFactory factory)
                    return factory;

            return null;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IGameSceneControllerFactory>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IGameSceneControllerFactory GetGameSceneControllerFactory(this System.Collections.Generic.IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IGameSceneControllerFactory>();
        }
    }
}
