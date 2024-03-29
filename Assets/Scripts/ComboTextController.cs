using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextController : MonoBehaviour
{
	private sealed class _Appear_c__AnonStorey0
	{
		internal float firstLineOriginalScale;

		internal float secondLineOriginalScale;

		internal ComboTextController _this;

		internal void __m__0()
		{
			this._this.firstLineText.transform.DOScale(this.firstLineOriginalScale, 0.2f).SetEase(Ease.OutCubic);
			this._this.firstLineText.DOFade(0f, 0.2f).SetDelay(1f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this._this.firstLineText.gameObject);
			});
		}

		internal void __m__1()
		{
			this._this.secondLineText.transform.DOScale(this.secondLineOriginalScale, 0.2f).SetEase(Ease.OutQuad);
			this._this.secondLineText.DOFade(0f, 0.2f).SetDelay(1f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this._this.secondLineText.gameObject);
				UnityEngine.Object.Destroy(this._this.gameObject);
			});
		}

		internal void __m__2()
		{
			UnityEngine.Object.Destroy(this._this.firstLineText.gameObject);
		}

		internal void __m__3()
		{
			UnityEngine.Object.Destroy(this._this.secondLineText.gameObject);
			UnityEngine.Object.Destroy(this._this.gameObject);
		}
	}

	public Text firstLineText;

	public Text secondLineText;

	private static TweenCallback __f__am_cache0;

	private static TweenCallback __f__am_cache1;

	public void SetComboText(string firstString, string secondString)
	{
		this.firstLineText.text = firstString;
		this.secondLineText.text = secondString;
	}

	public void Appear()
	{
		float firstLineOriginalScale = this.firstLineText.transform.localScale.x;
		float secondLineOriginalScale = this.secondLineText.transform.localScale.x;
		this.firstLineText.transform.DOScale(firstLineOriginalScale * 2f, 0.2f).SetEase(Ease.OutCubic).OnStart(delegate
		{
		}).OnComplete(delegate
		{
			this.firstLineText.transform.DOScale(firstLineOriginalScale, 0.2f).SetEase(Ease.OutCubic);
			this.firstLineText.DOFade(0f, 0.2f).SetDelay(1f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.firstLineText.gameObject);
			});
		});
		this.secondLineText.transform.DOScale(secondLineOriginalScale * 3f, 0.1f).SetEase(Ease.OutQuad).SetDelay(0.1f).OnStart(delegate
		{
		}).OnComplete(delegate
		{
			this.secondLineText.transform.DOScale(secondLineOriginalScale, 0.2f).SetEase(Ease.OutQuad);
			this.secondLineText.DOFade(0f, 0.2f).SetDelay(1f).OnComplete(delegate
			{
				UnityEngine.Object.Destroy(this.secondLineText.gameObject);
				UnityEngine.Object.Destroy(this.gameObject);
			});
		});
	}
}
