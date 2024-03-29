using DG.Tweening;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public enum Music
	{
		MainMusic,
		BossMusic
	}

	public AudioClip[] tapSound;

	public AudioClip[] swooshSFX;

	public AudioClip titleMeshSFX;

	public AudioClip titleBounceSFX;

	public AudioClip tapCancelSound;

	public AudioClip[] music;

	public AudioClip bossaNovaEnding;

	private static SoundManager instance;

	private AudioSource sfxAudio;

	private AudioSource musicAudio;

	private AudioSource ambientSFXAudio;

	public AudioSource powerUpRandomiserSource;

	public AudioSource[] audioSourceArray;

	public AudioSource[] animalAudioSourceArray;

	private AudioSource playerAudioSource;

	private AudioSource powerUpAudioSource;

	private bool isSFXOn;

	private bool isMusicOn;

	private bool isMainMusicPaused;

	public static bool IsSFXOn
	{
		get
		{
			return SoundManager.instance.isSFXOn;
		}
	}

	public static bool IsMusicOn
	{
		get
		{
			return SoundManager.instance.isMusicOn;
		}
	}

	private void Awake()
	{
		if (SoundManager.instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
		SoundManager.instance = this;
		this.sfxAudio = base.gameObject.AddComponent<AudioSource>();
		this.musicAudio = base.gameObject.AddComponent<AudioSource>();
		this.ambientSFXAudio = base.gameObject.AddComponent<AudioSource>();
		this.sfxAudio.playOnAwake = false;
		this.musicAudio.playOnAwake = false;
		SoundManager.UpdateMusicPreference();
		SoundManager.UpdateSFXPreference();
		this.musicAudio.loop = true;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		SoundManager.instance = this;
	}

	public static void PauseLoopSFX()
	{
		SoundManager.instance.playerAudioSource.Pause();
		SoundManager.instance.powerUpAudioSource.Pause();
		SoundManager.instance.powerUpRandomiserSource.Pause();
	}

	public static void UnPauseLoopSFX()
	{
		SoundManager.instance.playerAudioSource.UnPause();
		SoundManager.instance.powerUpAudioSource.UnPause();
		SoundManager.instance.powerUpRandomiserSource.UnPause();
	}

	public static void SetPlayerAudioSource(AudioSource audioSource)
	{
		SoundManager.instance.playerAudioSource = audioSource;
		SoundManager.instance.playerAudioSource.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
	}

	public static void SetPowerUpAudioSource(AudioSource audioSource)
	{
		SoundManager.instance.powerUpAudioSource = audioSource;
		SoundManager.instance.powerUpAudioSource.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
	}

	public static void MutePlayerLoopSFX()
	{
		SoundManager.instance.playerAudioSource.volume = 0f;
	}

	public static void UnMutePlayerLoopSFX()
	{
		if (!SoundManager.IsSFXOn)
		{
			return;
		}
		SoundManager.instance.playerAudioSource.volume = 1f;
	}

	public static void PlayPlayerLoopSFX(AudioClip audioClip)
	{
		SoundManager.instance.playerAudioSource.clip = audioClip;
		SoundManager.instance.playerAudioSource.loop = true;
		SoundManager.instance.playerAudioSource.Play();
	}

	public static void PlaySwooshSFX()
	{
		if (SoundManager.instance.swooshSFX.Length > 0)
		{
			SoundManager.PlaySFXInArray(SoundManager.instance.swooshSFX[UnityEngine.Random.Range(0, SoundManager.instance.swooshSFX.Length)], Vector3.zero, 1f);
		}
	}

	public static void PlayPowerUpRandomiser(AudioClip audioClip)
	{
		SoundManager.instance.powerUpRandomiserSource.clip = audioClip;
		SoundManager.instance.powerUpRandomiserSource.loop = true;
		SoundManager.instance.powerUpRandomiserSource.Play();
	}

	public static void PlayPowerUpLoopSFX(AudioClip audioClip, float volume)
	{
		SoundManager.instance.powerUpAudioSource.clip = audioClip;
		SoundManager.instance.powerUpAudioSource.loop = true;
		if (SoundManager.instance.isSFXOn)
		{
			SoundManager.instance.powerUpAudioSource.volume = volume;
		}
		SoundManager.instance.powerUpAudioSource.Play();
	}

	public static void StopPowerUpRandomiserSFX()
	{
		SoundManager.instance.powerUpRandomiserSource.Stop();
	}

	public static void StopPlayerLoopSFX()
	{
		SoundManager.instance.playerAudioSource.Stop();
	}

	public static void StopPowerUpLoopSFX()
	{
		if (SoundManager.instance.powerUpAudioSource != null)
		{
			SoundManager.instance.powerUpAudioSource.Stop();
		}
	}

	public static void PlaySFXInArray(AudioClip clipSFX, Vector3 pos, float volume)
	{
		if (clipSFX == null)
		{
			return;
		}
		for (int i = 0; i < SoundManager.instance.audioSourceArray.Length; i++)
		{
			if (!SoundManager.instance.audioSourceArray[i].isPlaying)
			{
				SoundManager.instance.audioSourceArray[i].clip = clipSFX;
				if (SoundManager.instance.isSFXOn)
				{
					SoundManager.instance.audioSourceArray[i].volume = volume;
				}
				SoundManager.instance.audioSourceArray[i].transform.position = pos;
				SoundManager.instance.audioSourceArray[i].PlayOneShot(clipSFX);
				return;
			}
		}
	}

	public static void PlayAnimalSFXInArray(AudioClip clipSFX, Vector3 pos)
	{
		if (clipSFX == null)
		{
			return;
		}
		for (int i = 0; i < SoundManager.instance.animalAudioSourceArray.Length; i++)
		{
			if (!SoundManager.instance.animalAudioSourceArray[i].isPlaying)
			{
				SoundManager.instance.transform.position = pos;
				SoundManager.instance.animalAudioSourceArray[i].PlayOneShot(clipSFX);
				return;
			}
		}
	}

	public static void PlayCancelSFX()
	{
		SoundManager.instance.sfxAudio.PlayOneShot(SoundManager.instance.tapCancelSound);
	}

	public static void PlayTapSFX()
	{
		if (SoundManager.instance.tapSound.Length > 0)
		{
			SoundManager.instance.sfxAudio.PlayOneShot(SoundManager.instance.tapSound[UnityEngine.Random.Range(0, SoundManager.instance.tapSound.Length)]);
		}
	}

	public static void UpdateMusicPreference()
	{
		SoundManager.instance.isMusicOn = PlayerPrefsX.GetBool("MusicOnPref", true);
		SoundManager.instance.musicAudio.volume = ((!SoundManager.instance.isMusicOn) ? 0f : 1f);
	}

	public static void SetSFXOn(bool input)
	{
		SoundManager.instance.isSFXOn = input;
		PlayerPrefsX.SetBool("SfxOnPref", input);
		SoundManager.UpdateSFXPreference();
	}

	public static void SetMusicOn(bool input)
	{
		SoundManager.instance.isMusicOn = input;
		PlayerPrefsX.SetBool("MusicOnPref", input);
		SoundManager.UpdateMusicPreference();
	}

	public static void UpdateSFXPreference()
	{
		SoundManager.instance.isSFXOn = PlayerPrefsX.GetBool("SfxOnPref", true);
		SoundManager.instance.sfxAudio.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		SoundManager.instance.ambientSFXAudio.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		if (SoundManager.instance.playerAudioSource != null)
		{
			SoundManager.instance.playerAudioSource.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		}
		if (SoundManager.instance.powerUpAudioSource != null)
		{
			SoundManager.instance.powerUpAudioSource.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		}
		if (SoundManager.instance.powerUpRandomiserSource != null)
		{
			SoundManager.instance.powerUpRandomiserSource.volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		}
		for (int i = 0; i < SoundManager.instance.audioSourceArray.Length; i++)
		{
			SoundManager.instance.audioSourceArray[i].volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		}
		for (int j = 0; j < SoundManager.instance.animalAudioSourceArray.Length; j++)
		{
			SoundManager.instance.animalAudioSourceArray[j].volume = ((!SoundManager.instance.isSFXOn) ? 0f : 1f);
		}
	}

	public static void PauseMusic()
	{
		if (SoundManager.instance.musicAudio.isPlaying)
		{
			SoundManager.instance.musicAudio.Pause();
			SoundManager.instance.isMainMusicPaused = true;
		}
	}

	public static void UnPauseMusic()
	{
		if (SoundManager.instance.isMainMusicPaused)
		{
			SoundManager.instance.musicAudio.UnPause();
			SoundManager.instance.isMainMusicPaused = false;
		}
	}

	public static void PlayMusic(SoundManager.Music musicType, float vol)
	{
		if (SoundManager.instance.musicAudio.isPlaying && SoundManager.instance.musicAudio.clip == SoundManager.instance.music[(int)musicType])
		{
			SoundManager.FadeMusicTo(vol, 0.1f, 0f);
			return;
		}
		SoundManager.instance.musicAudio.loop = true;
		SoundManager.instance.musicAudio.clip = SoundManager.instance.music[(int)musicType];
		SoundManager.instance.musicAudio.time = 0f;
		SoundManager.instance.musicAudio.volume = ((!SoundManager.instance.isMusicOn) ? 0f : vol);
		SoundManager.instance.musicAudio.Play();
	}

	public static void StopMainMusic()
	{
		SoundManager.instance.musicAudio.Stop();
	}

	public static void FadeMusicTo(float volume, float time, float delay)
	{
		if (!SoundManager.instance.isMusicOn)
		{
			return;
		}
		SoundManager.instance.musicAudio.DOFade(volume, time).SetDelay(delay);
	}
}
