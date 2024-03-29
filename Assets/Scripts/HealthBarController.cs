using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
	public Image healthBarImage;

	public float progress;

	public void ResetHealthBar()
	{
		this.healthBarImage.fillAmount = 1f;
	}

	public void SetHealthBarTo(float amount)
	{
		this.progress = amount;
		this.healthBarImage.DOFillAmount(amount, 0.1f).SetEase(Ease.Linear);
	}
}
