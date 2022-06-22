using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INFramework
{
	public class AudioMgr : MonoSingleton<AudioMgr>
	{
		private AudioListener mAudioListener; 
		private AudioSource mAudioSound;
        private AudioSource mAudioMusic;
		private Dictionary<string, AudioClip> mCacheClips = new Dictionary<string, AudioClip>();


		private AudioMgr()
        {

        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="rName"></param>
        /// <param name="bIsCache"></param>
		public void PlaySound(string rName, bool bIsCache = true)
        {
			var path = rName;
            CheckAudioListener();
            if (mAudioSound == null)
            {
                mAudioSound = gameObject.AddComponent<AudioSource>();
                mAudioSound.playOnAwake = false;
            }
            if (!mCacheClips.TryGetValue(path, out var audioClip))
            {
				ResMgr.Instance.LoadAsync<AudioClip>(path, (clip) => {
                    audioClip = clip;
                    if (audioClip == null) return;
                    if (bIsCache)
                    {
                        mCacheClips.Add(path, audioClip);
                    }
                    mAudioSound.clip = audioClip;
                    mAudioSound.Play();
                });
            }
            else
            {
                mAudioSound.clip = audioClip;
                mAudioSound.Play();
            }
        }
        public void PlaySoundMix(string rName)
        {
            //TODO对象池
        }
        public void SoundOn()
        {
            if(mAudioSound!=null)
            {
                mAudioSound.mute = false;
            }
        }
        public void SoundOff()
        {
            if (mAudioSound != null)
            {
                mAudioSound.mute = true;
            }
        }
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="rName"></param>
        /// <param name="bIsLoop"></param>
        public void PlayMusic(string rName, bool bIsLoop = true)
        {
            CheckAudioListener();
            string path = rName;
            if (mAudioMusic == null)
            {
                mAudioMusic = gameObject.AddComponent<AudioSource>();
            }
            mAudioMusic.loop = bIsLoop;
            if (mAudioMusic.clip == null || mAudioMusic.clip.name != rName)
            {
                ResMgr.Instance.LoadAsync<AudioClip>(path, (clip) =>
                {
                    mAudioMusic.clip = clip;
                    mAudioMusic.Play();
                });
            }
        }
        /// <summary>
        /// 停止播放音乐
        /// </summary>
        public void StopMusic()
        {
            if(mAudioMusic != null)
            {
                mAudioMusic.Stop();
                mAudioMusic.clip = null;
            }
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        public void PasueMusic()
        {
            if (mAudioMusic != null)
            {
                mAudioMusic.Pause();
            }
        }
        /// <summary>
        /// 恢复播放
        /// </summary>
        public void UnPauseMusic()
        {
            if (mAudioMusic != null)
            {
                mAudioMusic.UnPause();
            }
        }
        public void MusicOn()
        {
            if (mAudioMusic != null)
            {
                mAudioMusic.mute = false;
            }
        }
        public void MusicOff()
        {
            if (mAudioMusic != null)
            {
                mAudioMusic.mute = true;
            }
        }
        private void CheckAudioListener()
        {
            if (mAudioListener == null)
            {
                mAudioListener = gameObject.AddComponent<AudioListener>();
            }
        }
	}
}

