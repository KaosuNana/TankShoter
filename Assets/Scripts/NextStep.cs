using DG.Tweening;
using I2.Loc;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class NextStep : MonoBehaviour
{
	public NextStepManager nextStepManagerInstance;

	public RectTransform nextStepObj;

	private static TweenCallback __f__am_cache0;

	public abstract bool ShouldShow();

	private void Awake()
	{
		LocalizationManager.OnLocalizeEvent += new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
		this.ExtraAwakeMethods();
	}

	protected virtual void ExtraAwakeMethods()
	{
	}

	private void OnDestroy()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnDisable()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	protected abstract void OnChangeLanguage();

	public void SlidesInWithDelay(float delay)
	{
		Vector2 anchoredPosition = this.nextStepObj.anchoredPosition;
		anchoredPosition.x = -1000f;
		this.nextStepObj.anchoredPosition = anchoredPosition;
		this.nextStepObj.DOAnchorPosX(0f, 0.2f, false).SetEase(Ease.OutBack).SetDelay(delay).OnStart(delegate
		{
			SoundManager.PlaySwooshSFX();
		});
	}
}
