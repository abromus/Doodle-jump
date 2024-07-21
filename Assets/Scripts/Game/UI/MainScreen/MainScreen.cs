using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal sealed class MainScreen : ScreenBase
    {
        [Core.Separator(Core.CustomColor.Lime)]
        [SerializeField] private Button _settings;
        [SerializeField] private TMP_Text _score;

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _settings.onClick.AddListener(OnSettingsClicked);
        }

        private void Unsubscribe()
        {
            _settings.onClick.RemoveListener(OnSettingsClicked);
        }

        private void OnSettingsClicked()
        {
            Debug.Log("Settings clicked");
        }
    }
}
