using DG.Tweening;
using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
	public NextStepManager nextStepManagerInstance;

	public GameObject tabButtons;

	private bool hasDoubledCoins;

	public Text resultInfoText;

	public Image overlayImage;

	private bool stageCleared;

	public void ShowStageClearResult(bool isStageCleared)
	{
		this.overlayImage.gameObject.SetActive(true);
		Color black = Color.black;
		black.a = 0f;
		this.overlayImage.color = black;
		this.overlayImage.DOFade(0.7f, 0.2f);
		this.resultInfoText.gameObject.SetActive(true);
		this.resultInfoText.DOFade(1f, 0.2f);
		if (!isStageCleared)
		{
			this.resultInfoText.text = ScriptLocalization.Get("GAME OVER");
		}
		else
		{
			this.resultInfoText.text = "STAGE CLEARED";
		}
		this.stageCleared = isStageCleared;
		this.nextStepManagerInstance.gameObject.SetActive(false);
		this.tabButtons.SetActive(false);
	}

	public void ShowNextSteps()
	{
		this.resultInfoText.DOFade(0f, 0.2f);
		this.nextStepManagerInstance.gameObject.SetActive(true);
		this.nextStepManagerInstance.DecideNextSteps();
		this.nextStepManagerInstance.AnimateNextStep();
		this.tabButtons.GetComponent<TabsButtonController>().ShowGameOverButtonWithDelayAndRestartButton(0.5f, this.stageCleared);
	}

	public void HideResultScreen()
	{
		this.nextStepManagerInstance.HideNextSteps();
		this.tabButtons.GetComponent<TabsButtonController>().HideTabsButton();
		this.overlayImage.DOFade(0f, 0.2f);
		this.resultInfoText.gameObject.SetActive(false);
		base.Invoke("DeactivateResultScreen", 1f);
	}

	private void DeactivateResultScreen()
	{
		base.gameObject.SetActive(false);
	}

	private void OnEnableFunctions()
	{
		this.StartListeningToBackButton();
		EventManager.StartListening("EventResultListenToAndroidBackButton", new UnityAction(this.StartListeningToBackButton));
	}

	private void OnEnable()
	{
		GameManager.instance.powerUpManagerInstance.UpdateNotificationBadge();
		GameManager.instance.vehicleManagerInstance.UpdateNotificationBadge();
		this.OnEnableFunctions();
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventResultListenToAndroidBackButton", new UnityAction(this.StartListeningToBackButton));
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	private void StartListeningToBackButton()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}

	private bool ShouldShowShareButton()
	{
		return true;
	}

	public void OnPressNextButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.NextLevelShouldHideBanner(true);
	}

	private void OnPressBackButton()
	{
		GameManager.RestartGame();
	}

	public void OnPressSettingButton()
	{
		SoundManager.PlayTapSFX();
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
		GameManager.ShowSettingMenu();
	}

	public void OnPressVehicleButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.ShowVehiclePage();
	}

	public void OnPressRestartButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.RestartGame();
	}

	public void OnPressShopButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.ShowShopPageWithNotEnoughCoinOrBolt(false, false);
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButton));
	}
}
