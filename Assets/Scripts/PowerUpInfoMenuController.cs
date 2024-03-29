using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpInfoMenuController : MonoBehaviour
{
	public Text powerUpInfo;

	public Image background;

	public void ShowPowerUpInfo(string powerUpName)
	{
		Vector2 anchoredPosition = this.powerUpInfo.GetComponent<RectTransform>().anchoredPosition;
		anchoredPosition.x = 1000f;
		this.powerUpInfo.text = powerUpName;
		this.powerUpInfo.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
		this.powerUpInfo.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.25f, false).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			this.powerUpInfo.GetComponent<RectTransform>().DOAnchorPosX(-100f, 0.25f, false).SetEase(Ease.InCirc).SetDelay(1f);
		});
		Vector2 anchoredPosition2 = this.background.GetComponent<RectTransform>().anchoredPosition;
		anchoredPosition2.x = -1000f;
		this.background.GetComponent<RectTransform>().anchoredPosition = anchoredPosition2;
		this.background.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.25f, false).SetEase(Ease.OutCirc).OnComplete(delegate
		{
			this.background.GetComponent<RectTransform>().DOAnchorPosX(1000f, 0.25f, false).SetEase(Ease.InCirc).SetDelay(1f).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		});
	}
}
