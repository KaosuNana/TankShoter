using System;
using UnityEngine;
using UnityEngine.Events;

public class CreditManager : MonoBehaviour
{
	public void OnPressBackButton()
	{
		SoundManager.PlayCancelSFX();
		base.gameObject.SetActive(false);
		EventManager.TriggerEvent("EventSettingListenToAndroidBackButton");
	}

	public void OnPressResaTwitter()
	{
		SoundManager.PlayTapSFX();
		Application.OpenURL("https://twitter.com/resaliputra");
	}

	public void OnPressJohanTwitter()
	{
		SoundManager.PlayTapSFX();
		Application.OpenURL("https://twitter.com/kewlj");
	}

	public void OnPressKisekiTwitterButton()
	{
		SoundManager.PlayTapSFX();
		Application.OpenURL("https://twitter.com/KisekiGames");
	}

	public void OnPressKisekiFacebook()
	{
		SoundManager.PlayTapSFX();
		Application.OpenURL("fb://profile/1156099021105113");
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
