using DG.Tweening;
using System;
using UnityEngine;

public class TabsButtonController : MonoBehaviour
{
	public GameObject[] buttonList;

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			Vector2 anchoredPosition = base.GetComponent<RectTransform>().anchoredPosition;
			anchoredPosition.y = 60f;
			base.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
		}
	}

	public void ShowGameOverButtonWithDelayAndRestartButton(float delay, bool shouldShowRestartButton)
	{
		base.gameObject.SetActive(true);
		for (int i = 0; i < this.buttonList.Length; i++)
		{
			this.buttonList[i].gameObject.SetActive(true);
			this.buttonList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
			this.buttonList[i].GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f).SetDelay(delay + 0.1f * (float)i).SetEase(Ease.OutBack);
		}
	}

	public void ShowTabsButtonWithDelay(float delay)
	{
		for (int i = 0; i < this.buttonList.Length; i++)
		{
			this.buttonList[i].gameObject.SetActive(true);
			this.buttonList[i].GetComponent<RectTransform>().DOKill(false);
			this.buttonList[i].GetComponent<RectTransform>().localScale = Vector3.zero;
			this.buttonList[i].GetComponent<RectTransform>().DOScale(Vector3.one, 0.2f).SetDelay(delay + 0.1f * (float)i).SetEase(Ease.OutBack);
		}
	}

	public void HideTabsButton()
	{
		for (int i = 0; i < this.buttonList.Length; i++)
		{
			this.buttonList[i].GetComponent<RectTransform>().DOKill(false);
			this.buttonList[i].GetComponent<RectTransform>().DOScale(Vector3.zero, 0.2f).SetDelay(0.1f * (float)i).SetEase(Ease.InBack);
		}
	}
}
