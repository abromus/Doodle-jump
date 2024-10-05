using DG.Tweening;
using DoodleJump.Core;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    internal sealed class GameOverScreen : BaseScreen
    {
        [Separator(CustomColor.Lime)]
        [SerializeField] private RectTransform _root;
        [SerializeField] private Button _buttonRestart;
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScore;
        [Separator(CustomColor.MediumTurquoise)]
        [SerializeField] private float _animationDelay;
        [SerializeField] private float _hideDuration;
        [SerializeField] private float _moveDuration;
        [SerializeField] private AnimationCurve _animationCurve;

        private IWorldData _worldData;
        private IPlayerData _playerData;
        private bool _initialized;

        private GameOverScreenArgs _args;
        private Sequence _sequence;

        public override void Init(IGameData gameData, IWorldData worldData, IScreenSystemService screenSystemService)
        {
            _worldData = worldData;

            var gameServiceStorage = gameData.ServiceStorage;
            var saveLoadService = gameServiceStorage.GetSaveLoadService();
            _playerData = saveLoadService.PersistentDataStorage.GetPlayerData();

            Subscribe();

            _initialized = true;
        }

        public override void SetArgs(IScreenArgs args)
        {
            base.SetArgs(args);

            _args = (GameOverScreenArgs)args;
        }

        public override void Show()
        {
            base.Show();

            UpdateScore(_playerData.CurrentScore, _playerData.MaxScore);

            var defaultLocalPosition = transform.localPosition.y;

            _sequence = DOTween.Sequence()
                .Append(_root.DOLocalMoveY(defaultLocalPosition - _root.rect.height, _hideDuration));

            if (_args.GameOverType == GameOverType.EnemyCollided)
                _sequence.AppendInterval(_animationDelay);

            _sequence.Append(_root.DOLocalMoveY(defaultLocalPosition, _moveDuration).SetEase(_animationCurve));
        }

        private void OnEnable()
        {
            if (_initialized)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_initialized)
                Unsubscribe();
        }

        private void UpdateScore(int currentScore, int maxScore)
        {
            _currentScore.text = $"{currentScore}";
            _maxScore.text = $"{maxScore}";
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartClicked);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _buttonRestart.onClick.RemoveListener(OnButtonRestartClicked);
        }

        private void OnButtonRestartClicked()
        {
            _worldData.Restart();
        }
    }
}
