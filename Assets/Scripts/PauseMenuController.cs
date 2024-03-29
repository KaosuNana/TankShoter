using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
	public Slider sensitivitySlider;

	public GameObject sensitivityObj;

	public GameObject musicOnButton;

	public GameObject musicOffButton;

	public GameObject sfxOnButton;

	public GameObject sfxOffButton;

	private void Awake()
	{
		this.UpdateAudioPref();
		this.UpdateSensitivitySlider();
	}

	private void UpdateSensitivitySlider()
	{
		this.sensitivitySlider.value = PlayerPrefs.GetFloat("JoystickSensitivity", 0.8f);
	}

	private void UpdateAudioPref()
	{
		this.sfxOnButton.SetActive(SoundManager.IsSFXOn);
		this.sfxOffButton.SetActive(!SoundManager.IsSFXOn);
		this.musicOnButton.SetActive(SoundManager.IsMusicOn);
		this.musicOffButton.SetActive(!SoundManager.IsMusicOn);
	}

	public void ResumeGame()
	{
		SoundManager.PlayTapSFX();
		GameManager.ResumeGame();
	}

	public void OnPressSensitivitySlider()
	{
		PlayerPrefs.SetFloat("JoystickSensitivity", this.sensitivitySlider.value);
		GameManager.UpdateSensitivity();
	}

	public void OnPressMusicOnButton()
	{
		SoundManager.SetMusicOn(false);
		SoundManager.PlayTapSFX();
		this.UpdateAudioPref();
	}

	public void OnPressMusicOffButton()
	{
		SoundManager.SetMusicOn(true);
		SoundManager.PlayTapSFX();
		this.UpdateAudioPref();
	}

	public void OnPressSFXOnButton()
	{
		SoundManager.SetSFXOn(false);
		SoundManager.PlayTapSFX();
		this.UpdateAudioPref();
	}

	public void OnPressSFXOffButton()
	{
		SoundManager.SetSFXOn(true);
		SoundManager.PlayTapSFX();
		this.UpdateAudioPref();
	}

	public void OnPressTutorialButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.StartTutorialMode();
	}

	public void HidePauseMenu()
	{
		base.gameObject.SetActive(false);
	}

	public void OnPressResumeGame()
	{
		SoundManager.PlayTapSFX();
		GameManager.ResumeGame();
	}

	public void OnPressRestartGame()
	{
		SoundManager.PlayTapSFX();
		GameManager.RestartGame();
	}
}
