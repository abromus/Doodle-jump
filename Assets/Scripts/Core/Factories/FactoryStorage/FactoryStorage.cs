using System;
using System.Collections.Generic;
using DoodleJump.Core.Settings;

namespace DoodleJump.Core.Factories
{
    internal sealed class FactoryStorage : IFactoryStorage
    {
        private Dictionary<Type, IFactory> _factories;

        private readonly IUiFactoryConfig _uiFactoryConfig;

        internal FactoryStorage(IConfigStorage configStorage)
        {
            _uiFactoryConfig = configStorage.GetCoreUiFactoryConfig();

            var uiFactories = _uiFactoryConfig.UiFactories;
            var gameSceneControllerFactory = uiFactories.GetGameSceneControllerFactory();

            _factories = new(4)
            {
                [typeof(IGameSceneControllerFactory)] = gameSceneControllerFactory,
            };
        }

        public void AddFactory<TFactory>(TFactory factory) where TFactory : class, IFactory
        {
            var type = typeof(TFactory);

            if (_factories.ContainsKey(type))
                _factories[type] = factory;
            else
                _factories.Add(type, factory);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        public void Destroy()
        {
            foreach (var factory in _factories.Values)
                factory.Destroy();

            _factories.Clear();
            _factories = null;
        }
    }
}
