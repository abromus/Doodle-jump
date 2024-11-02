using DoodleJump.Game.Factories;

namespace DoodleJump.Game.States
{
    internal sealed class GameInitializationState : Core.Services.IEnterState<GameStateMachineArgs>
    {
        private readonly Data.IGameData _gameData;
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameInitializationState(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _gameData = gameData;
            _stateMachine = stateMachine;
        }

        public void Enter(GameStateMachineArgs args)
        {
            var factoryStorage = _gameData.FactoryStorage;

            CreateDoodler(factoryStorage, ref args);
            CreateWorld(factoryStorage, ref args);

            var gameStateArgs = new GameStateArgs(args.World, args.Doodler);

            _stateMachine.Enter<GameStartState, GameStateArgs>(gameStateArgs);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void CreateDoodler(Core.Factories.IFactoryStorage factoryStorage, ref GameStateMachineArgs args)
        {
            var factory = factoryStorage.GetDoodlerFactory();

            args.Doodler = factory.Create();
        }

        private void CreateWorld(Core.Factories.IFactoryStorage factoryStorage, ref GameStateMachineArgs args)
        {
            var factory = factoryStorage.GetWorldFactory();

            args.World = factory.CreateWorld(args.Doodler);
        }
    }
}
