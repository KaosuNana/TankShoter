using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
	public GameObject creditPage;

	public GameObject languagePage;

	public Slider sensitivitySlider;

	public GameObject sensitivityObj;

	public Text shadowIsOnText;

	public Text shadowIsOffText;

	public GameObject musicOnButton;

	public GameObject musicOffButton;

	public GameObject sfxOnButton;

	public GameObject sfxOffButton;

	private void Awake()
	{
		this.UpdateSensitivitySlider();
		this.UpdateShadowButton();
		this.UpdateAudioPref();
	}

	private void UpdateAudioPref()
	{
		this.sfxOnButton.SetActive(SoundManager.IsSFXOn);
		this.sfxOffButton.SetActive(!SoundManager.IsSFXOn);
		this.musicOnButton.SetActive(SoundManager.IsMusicOn);
		this.musicOffButton.SetActive(!SoundManager.IsMusicOn);
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

	public void UpdateShadowButton()
	{
		this.shadowIsOnText.gameObject.SetActive(GameSingleton.ShadowPreference);
		this.shadowIsOffText.gameObject.SetActive(!GameSingleton.ShadowPreference);
	}

	public void OnPressBackButton()
	{
		SoundManager.PlayCancelSFX();
		GameManager.HideSettingMenu();
		EventManager.TriggerEvent("EventResultListenToAndroidBackButton");
	}

	public void OnPressLanguageButton()
	{
		SoundManager.PlayTapSFX();
		this.languagePage.SetActive(true);
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	public void OnPressCreditButton()
	{
		SoundManager.PlayTapSFX();
		this.creditPage.SetActive(true);
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	public void OnPressTutorialButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.StartTutorialMode();
	}

	public void OnPressMoreGames()
	{
		SoundManager.PlayTapSFX();
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.kisekigames.bouncyhero");
	}

	public void OnPressRestorePurchases()
	{
		SoundManager.PlayTapSFX();
		// Purchaser.RestorePurchases();
	}

	public void OnPressContactUsButton()
	{
		SoundManager.PlayTapSFX();
		string text = "gameskiseki@gmail.com";
		string text2 = this.MyEscapeURL("Tank Buddies - v" + Application.version + " " + SystemInfo.deviceModel);
		string text3 = this.MyEscapeURL("Dear Developer,\r\n");
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			text,
			"?subject=",
			text2,
			"&body=",
			text3
		}));
	}

	private string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}

	public void OnPressShadowButton()
	{
		SoundManager.PlayTapSFX();
		GameSingleton.UpdateShadowPreference();
		this.UpdateShadowButton();
	}

	public void OnPressLeaderboardButton()
	{
		SoundManager.PlayTapSFX();
		GameSingleton.ShowLeaderboard();
	}

	private void UpdateSensitivitySlider()
	{
		this.sensitivitySlider.value = PlayerPrefs.GetFloat("JoystickSensitivity", 0.8f);
	}

	public void OnPressSensitivitySlider()
	{
		PlayerPrefs.SetFloat("JoystickSensitivity", this.sensitivitySlider.value);
	}

	private void StartListenToAndroidBackButton()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	private void OnEnable()
	{
		this.StartListenToAndroidBackButton();
		EventManager.StartListening("EventSettingListenToAndroidBackButton", new UnityAction(this.StartListenToAndroidBackButton));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
		EventManager.StopListening("EventSettingListenToAndroidBackButton", new UnityAction(this.StartListenToAndroidBackButton));
	}
}
