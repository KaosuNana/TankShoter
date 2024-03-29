// using CodeStage.AntiCheat.ObscuredTypes;
using System;
using UnityEngine;

public class NextStepRateUs : NextStep
{
	public override bool ShouldShow()
	{
		// return GameSingleton.NumberOfGame % 6 == 5 && !ObscuredPrefs.GetBool("HasClickedOnRateUs", false);
		return GameSingleton.NumberOfGame % 6 == 5 && PlayerPrefs.GetInt("HasClickedOnRateUs", 0)==0;
	}

	public void OnPressRateUsButton()
	{
		Application.OpenURL("market://details?id=YOUR_APP_ID");
		SoundManager.PlayTapSFX();
		// ObscuredPrefs.SetBool("HasClickedOnRateUs", true);
		PlayerPrefs.SetInt("HasClickedOnRateUs", 1);
	}

	protected override void OnChangeLanguage()
	{
	}
}
