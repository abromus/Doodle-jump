using DoodleJump.Game.Data;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct WorldInitializerArgs
    {
        private readonly IGameData _gameData;
        private readonly WorldArgs _args;
        private readonly Transform _worldTransform;
        private readonly Transform _platformsContainer;
        private readonly Transform _enemiesContainer;
        private readonly Transform _boostersContainer;
        private readonly Transform _projectilesContainer;
        private readonly SpriteRenderer[] _backgrounds;
        private readonly IDoodler _doodler;

        internal readonly IGameData GameData => _gameData;

        internal readonly WorldArgs WorldArgs => _args;

        internal readonly Transform WorldTransform => _worldTransform;

        internal readonly Transform PlatformsContainer => _platformsContainer;

        internal readonly Transform EnemiesContainer => _enemiesContainer;

        internal readonly Transform BoostersContainer => _boostersContainer;

        internal readonly Transform ProjectilesContainer => _projectilesContainer;

        internal readonly SpriteRenderer[] Backgrounds => _backgrounds;

        internal readonly IDoodler Doodler => _doodler;

        internal WorldInitializerArgs(
            IGameData gameData,
            WorldArgs args,
            Transform worldTransform,
            Transform platformsContainer,
            Transform enemiesContainer,
            Transform boostersContainer,
            Transform projectilesContainer,
            SpriteRenderer[] backgrounds,
            IDoodler doodler)
        {
            _gameData = gameData;
            _args = args;
            _worldTransform = worldTransform;
            _platformsContainer = platformsContainer;
            _enemiesContainer = enemiesContainer;
            _boostersContainer = boostersContainer;
            _projectilesContainer = projectilesContainer;
            _backgrounds = backgrounds;
            _doodler = doodler;
        }
    }
}
