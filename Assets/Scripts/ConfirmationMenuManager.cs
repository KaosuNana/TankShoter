using System;
using UnityEngine;

public class ConfirmationMenuManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	protected ConfirmationMenuManager.CallBackFunction callbackFct;

	public void ShowMenuWithCallBack(ConfirmationMenuManager.CallBackFunction myCallbackFct)
	{
		this.callbackFct = myCallbackFct;
	}

	public void OnPressYesButton()
	{
		SoundManager.PlayTapSFX();
		this.callbackFct();
		base.gameObject.SetActive(false);
	}

	public void OnPressNoButton()
	{
		SoundManager.PlayTapSFX();
		base.gameObject.SetActive(false);
	}
}
