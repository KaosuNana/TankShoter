using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
	public enum Language
	{
		English,
		French,
		German,
		Spanish,
		Italian,
		Portuguese,
		Indonesian,
		Malaysia,
		Japanese,
		Korean,
		ChineseTraditional,
		ChineseSimplified
	}

	public Text title;

	public void SetDefaultLanguage(int language)
	{
		SoundManager.PlayTapSFX();
		if (language <= 9)
		{
			LanguageManager.Language language2 = (LanguageManager.Language)language;
			LocalizationManager.CurrentLanguage = language2.ToString();
		}
		else if (language == 10)
		{
			LocalizationManager.CurrentLanguage = "Chinese (Traditional)";
		}
		else if (language == 11)
		{
			LocalizationManager.CurrentLanguage = "Chinese (Simplified)";
		}
		this.title.text = ScriptLocalization.Get("LANGUAGE").ToUpper();
	}

	public void OnPressBackButton()
	{
		SoundManager.PlayCancelSFX();
		base.gameObject.SetActive(false);
		EventManager.TriggerEvent("EventSettingListenToAndroidBackButton");
	}

	private void OnEnable()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}
}
