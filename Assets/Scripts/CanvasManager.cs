using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
	public ContinueMenuManager continueMenuInstance;

	public PauseMenuController pauseMenuInstance;

	public HUDMenuController hudMenuInstance;

	public MainMenuController mainMenuInstance;

	public ResultManager resultManagerInstance;

	public BossIncomingOverlayController bossIncomingInstance;

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			base.GetComponent<CanvasScaler>().matchWidthOrHeight = 0f;
		}
		else
		{
			base.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
		}
	}

	public void ShowResultWithStageClear(bool isStageCleared)
	{
		this.resultManagerInstance.gameObject.SetActive(true);
		this.resultManagerInstance.ShowStageClearResult(isStageCleared);
		this.HidePauseButton();
	}

	public void ShowNextSteps()
	{
		this.resultManagerInstance.GetComponent<ResultManager>().ShowNextSteps();
	}

	public void HideResultScreen()
	{
		this.resultManagerInstance.GetComponent<ResultManager>().HideResultScreen();
	}

	public void HidePauseButton()
	{
		this.hudMenuInstance.HidePauseButton();
	}

	public void WarnBossWave()
	{
		this.bossIncomingInstance.gameObject.SetActive(true);
		this.bossIncomingInstance.StartBlinkingTexts();
	}

	public void UpdateChargeBar(float progress, float duration)
	{
		this.hudMenuInstance.UpdateChargeBar(progress, duration);
	}

	public void ShowContinue()
	{
		this.hudMenuInstance.HidePauseButton();
		this.continueMenuInstance.gameObject.SetActive(true);
		this.continueMenuInstance.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -1000f);
		this.continueMenuInstance.GetComponent<RectTransform>().DOAnchorPosY(-280f, 0.3f, false).SetEase(Ease.OutBack);
	}

	public void ShowGamePlayHUD()
	{
		this.hudMenuInstance.OnShowGamePlayHUD();
	}

	public void HideContinueWithPauseButton(bool showPauseButton)
	{
		this.continueMenuInstance.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 0.2f, false).SetEase(Ease.InBack).OnComplete(delegate
		{
			this.continueMenuInstance.gameObject.SetActive(false);
		});
		if (showPauseButton)
		{
			this.hudMenuInstance.ShowPauseButton();
		}
	}

	public void ShowMainMenu(bool shouldShow)
	{
		if (shouldShow)
		{
			this.mainMenuInstance.gameObject.SetActive(true);
			this.mainMenuInstance.ShowMainMenuWithDelay(0f);
			this.hudMenuInstance.OnShowMainMenuHUD();
		}
		else
		{
			this.mainMenuInstance.HideMainMenu();
		}
	}

	public void HideContinueAndShowResult()
	{
		this.continueMenuInstance.GetComponent<RectTransform>().DOAnchorPosY(-1000f, 0.2f, false).SetEase(Ease.InBack).OnComplete(delegate
		{
			this.continueMenuInstance.gameObject.SetActive(false);
			this.ShowResultWithStageClear(false);
		});
		SoundManager.PlayTapSFX();
	}

	public void ShowPauseMenu()
	{
		this.hudMenuInstance.OnShowPauseHUD();
		this.pauseMenuInstance.gameObject.SetActive(true);
	}

	public void HidePauseMenu()
	{
		this.hudMenuInstance.OnShowGamePlayHUD();
		this.pauseMenuInstance.HidePauseMenu();
	}
}
