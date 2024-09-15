using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class AudioService : UiService, IAudioService
    {
        [SerializeField] private AudioSource _backgroundMusic;
        [SerializeField] private AudioSource _oneShotSounds;
        [SerializeField] private AudioSource _loopSoundPrefab;
        [SerializeField] private Transform _loopSoundsContainer;
        [SerializeField] private BackgroundInfo[] _backgroundInfos;
        [Separator(CustomColor.Lime)]
        [SerializeField] private PlatformClipInfo[] _platformClipInfos;
        [Separator(CustomColor.MediumTurquoise)]
        [SerializeField] private EnemyClipInfo[] _enemyClipInfos;
        [SerializeField] private EnemyTriggerClipInfo[] _enemyTriggerClipInfos;
        [Separator(CustomColor.Elsie)]
        [SerializeField] private ProjectileClipInfo[] _projectileClipInfos;
        [Separator(CustomColor.Presley)]
        [SerializeField] private BoosterClipInfo[] _boosterClipInfos;

        private IUpdater _updater;
        private IObjectPool<AudioSource> _loopSoundPool;

        private readonly List<AudioSource> _loopSounds = new(32);

        public override UiServiceType UiServiceType => UiServiceType.AudioService;

        public void Init(IUpdater updater)
        {
            _updater = updater;

            Subscribe();
        }

        public void PlayBackground(BackgroundType backgroundType)
        {
            if (backgroundType == BackgroundType.None)
                return;

            var clip = GetBackgroundClip(backgroundType);

            if (clip == null)
                return;

            _backgroundMusic.clip = clip;
            _backgroundMusic.loop = true;
            _backgroundMusic.Play();
        }

        public void PlaySound(PlatformClipType clipType)
        {
            if (clipType == PlatformClipType.None)
                return;

            var clip = GetClip(clipType);

            if (clip == null)
                return;

            _oneShotSounds.PlayOneShot(clip);
        }

        public void PlaySound(EnemyTriggerClipType clipType)
        {
            if (clipType == EnemyTriggerClipType.None)
                return;

            var clip = GetClip(clipType);

            if (clip == null)
                return;

            _oneShotSounds.PlayOneShot(clip);
        }

        public void PlaySound(ProjectileClipType clipType)
        {
            if (clipType == ProjectileClipType.None)
                return;

            var clip = GetClip(clipType);

            if (clip == null)
                return;

            _oneShotSounds.PlayOneShot(clip);
        }

        public AudioSource PlayLoopSound(EnemyClipType clipType)
        {
            if (clipType == EnemyClipType.None)
                return null;

            var clip = GetClip(clipType);

            if (clip == null)
                return null;

            var loopSound = _loopSoundPool.Get();
            loopSound.gameObject.SetActive(true);
            loopSound.clip = clip;
            loopSound.Play();

            _loopSounds.Add(loopSound);

            return loopSound;
        }

        public AudioSource PlayLoopSound(BoosterClipType clipType)
        {
            if (clipType == BoosterClipType.None)
                return null;

            var clip = GetClip(clipType);

            if (clip == null)
                return null;

            var loopSound = _loopSoundPool.Get();
            loopSound.gameObject.SetActive(true);
            loopSound.clip = clip;
            loopSound.Play();

            _loopSounds.Add(loopSound);

            return loopSound;
        }

        public void StopLoopSound(AudioSource loopSound)
        {
            if (loopSound == null || _loopSounds.Contains(loopSound) == false)
                return;

            loopSound.Stop();
            loopSound.gameObject.SetActive(false);

            _loopSounds.Remove(loopSound);
            _loopSoundPool.Release(loopSound);
        }

        public void SetActiveBackgroundMusic(bool isActive)
        {
            _backgroundMusic.enabled = isActive;
        }

        public void SetActiveSounds(bool isActive)
        {
            _oneShotSounds.enabled = isActive;

            foreach (var loopSound in _loopSounds)
                loopSound.enabled = isActive;
        }

        public void SetBackgroundMusicVolume(float volume)
        {
            _backgroundMusic.volume = volume;
        }

        public void SetSoundsVolume(float volume)
        {
            _oneShotSounds.volume = volume;

            foreach (var loopSound in _loopSounds)
                loopSound.volume = volume;
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
            {
                _backgroundMusic.Pause();
                _oneShotSounds.Pause();

                foreach (var loopSound in _loopSounds)
                    loopSound.Pause();
            }
            else
            {
                _backgroundMusic.UnPause();
                _oneShotSounds.UnPause();

                foreach (var loopSound in _loopSounds)
                    loopSound.UnPause();
            }
        }

        public void Destroy()
        {
            Unsubscribe();

            foreach (var loopSound in _loopSounds)
            {
                loopSound.Stop();

                _loopSoundPool.Release(loopSound);

                Destroy(loopSound.gameObject);
            }

            _loopSounds.Clear();
        }

        private void Awake()
        {
            _loopSoundPool = new ObjectPool<AudioSource>(CreateLoopSound);
        }

        private AudioSource CreateLoopSound()
        {
            var loopSound = Instantiate(_loopSoundPrefab, _loopSoundsContainer);
            loopSound.gameObject.RemoveCloneSuffix();

            loopSound.name = $"{loopSound.name} {_loopSounds.Count + 1}";

            return loopSound;
        }

        private AudioClip GetBackgroundClip(BackgroundType backgroundType)
        {
            foreach (var backgroundInfo in _backgroundInfos)
                if (backgroundInfo.BackgroundType == backgroundType)
                    return backgroundInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(PlatformClipType clipType)
        {
            foreach (var clipInfo in _platformClipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(EnemyClipType clipType)
        {
            foreach (var clipInfo in _enemyClipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(EnemyTriggerClipType clipType)
        {
            foreach (var clipInfo in _enemyTriggerClipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(ProjectileClipType clipType)
        {
            foreach (var clipInfo in _projectileClipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }

        private AudioClip GetClip(BoosterClipType clipType)
        {
            foreach (var clipInfo in _boosterClipInfos)
                if (clipInfo.ClipType == clipType)
                    return clipInfo.AudioClip;

            return null;
        }

        private void Subscribe()
        {
            _updater.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemovePausable(this);
        }
    }
}
