using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CoinUIAnimationController : MonoBehaviour
{
	private sealed class _AnimateAmountWithScaleDown_c__AnonStorey0
	{
		internal int amount;

		internal int tempAmount;

		internal bool shouldScaleDown;

		internal CoinUIAnimationController _this;

		internal void __m__0()
		{
			DOTween.To(() => this.tempAmount, delegate(int x)
			{
				this.tempAmount = x;
			}, this.amount, 0.25f).SetDelay(0.2f).OnStart(delegate
			{
				SoundManager.PlaySFXInArray(this._this.countingUpSFX, this._this.transform.position, 1f);
			}).OnComplete(delegate
			{
				if (this.shouldScaleDown)
				{
					this._this.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.25f).SetDelay(1.5f);
				}
			}).OnUpdate(delegate
			{
				this._this.coinText.text = GameManager.CurrencyToString((float)this.tempAmount);
			});
		}

		internal int __m__1()
		{
			return this.tempAmount;
		}

		internal void __m__2(int x)
		{
			this.tempAmount = x;
		}

		internal void __m__3()
		{
			SoundManager.PlaySFXInArray(this._this.countingUpSFX, this._this.transform.position, 1f);
		}

		internal void __m__4()
		{
			if (this.shouldScaleDown)
			{
				this._this.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.25f).SetDelay(1.5f);
			}
		}

		internal void __m__5()
		{
			this._this.coinText.text = GameManager.CurrencyToString((float)this.tempAmount);
		}
	}

	public Text coinText;

	public AudioClip countingUpSFX;

	public bool shouldDisableHUDWhenDisabled;

	public void AnimateAmountWithScaleDown(int amount, bool shouldScaleDown)
	{
		base.GetComponent<RectTransform>().localScale = Vector3.zero;
		this.coinText.gameObject.SetActive(true);
		this.coinText.text = "0";
		int tempAmount = 0;
		base.GetComponent<RectTransform>().DOScale(Vector3.one * 1.5f, 0.25f).OnStart(delegate
		{
			DOTween.To(() => tempAmount, delegate(int x)
			{
				tempAmount = x;
			}, amount, 0.25f).SetDelay(0.2f).OnStart(delegate
			{
				SoundManager.PlaySFXInArray(this.countingUpSFX, this.transform.position, 1f);
			}).OnComplete(delegate
			{
				if (shouldScaleDown)
				{
					this.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.25f).SetDelay(1.5f);
				}
			}).OnUpdate(delegate
			{
				this.coinText.text = GameManager.CurrencyToString((float)tempAmount);
			});
		});
	}

	private void OnDisable()
	{
	}
}
