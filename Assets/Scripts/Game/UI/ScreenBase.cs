﻿using UnityEngine;

namespace DoodleJump.Game.UI
{
    [System.Serializable]
    internal abstract class ScreenBase : MonoBehaviour, IScreen
    {
        [SerializeField] private ScreenType _screenType;

        public ScreenType ScreenType => _screenType;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (this != null && gameObject != null)
                gameObject.SetActive(false);
        }
    }
}
