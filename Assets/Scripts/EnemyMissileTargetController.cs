using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMissileTargetController : MonoBehaviour
{
	private Image radarImage;

	private Vector3 originalScale;

	private void Awake()
	{
		this.radarImage = base.GetComponent<Image>();
		this.originalScale = this.radarImage.transform.localScale;
		this.radarImage.transform.localScale = Vector3.zero;
	}

	public void AppearWithDelay(float delay)
	{
		this.radarImage.transform.DOScale(this.originalScale * 1.5f, 0.3f).SetDelay(delay).OnComplete(delegate
		{
			this.radarImage.transform.DOScale(this.originalScale * 0.8f, 0.2f).SetDelay(0.3f);
			this.radarImage.transform.DOScale(this.originalScale, 0.2f).SetDelay(0.5f);
		});
	}

	public void Disappear()
	{
		this.radarImage.transform.DOScale(Vector3.zero, 0f);
	}
}
