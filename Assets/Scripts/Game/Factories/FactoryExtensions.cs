using System.Collections.Generic;
using DoodleJump.Core.Factories;

namespace DoodleJump.Game.Factories
{
    internal static class FactoryExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IDoodlerFactory GetDoodlerFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IDoodlerFactory>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IDoodlerFactory GetDoodlerFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IDoodlerFactory>(UiFactoryType.DoodlerFactory);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IWorldFactory GetWorldFactory(this IFactoryStorage factoryStorage)
        {
            return factoryStorage.GetFactory<IWorldFactory>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IWorldFactory GetWorldFactory(this IReadOnlyList<IUiFactory> uiFactories)
        {
            return uiFactories.GetFactory<IWorldFactory>(UiFactoryType.WorldFactory);
        }
    }
}
