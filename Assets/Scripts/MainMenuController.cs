using DG.Tweening;
using I2.Loc;
using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	public TabsButtonController tabButtonInstance;

	public CanvasGroup visualInstruction;

	public GameObject[] mainTitleObj;

	private bool isChinese;

	public void ShowMainMenuWithDelay(float delay)
	{
		this.tabButtonInstance.ShowTabsButtonWithDelay(0f);
		this.visualInstruction.DOFade(1f, 0.5f);
		string currentLanguageCode = LocalizationManager.CurrentLanguageCode;
		this.isChinese = (currentLanguageCode == "zh-TW" || currentLanguageCode == "zh-CN");
		this.mainTitleObj[(!this.isChinese) ? 0 : 1].SetActive(true);
	}

	public void HideMainMenu()
	{
		this.tabButtonInstance.HideTabsButton();
		this.visualInstruction.DOFade(0f, 0.5f);
		this.mainTitleObj[(!this.isChinese) ? 0 : 1].GetComponent<Animator>().SetTrigger("TransitionOut");
		base.Invoke("DeactivateMainTitle", 0.5f);
	}

	private void DeactivateMainTitle()
	{
		this.mainTitleObj[(!this.isChinese) ? 0 : 1].SetActive(false);
		base.gameObject.SetActive(false);
	}

	public void OnPressVehicleButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.ShowVehiclePage();
	}

	public void OnPressPowerUpButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.ShowPowerUpPage();
	}
}
