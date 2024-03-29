using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoinHUDController : MonoBehaviour
{
	private sealed class _UpdateCoinsHUDNoScaling_c__AnonStorey0
	{
		internal int currentCoin;

		internal CoinHUDController _this;

		internal int __m__0()
		{
			return this.currentCoin;
		}

		internal void __m__1(int x)
		{
			this.currentCoin = x;
		}

		internal void __m__2()
		{
			this._this.coinsText.text = this.currentCoin.ToString();
		}
	}

	public Text coinsText;

	public GameObject[] coinHolderList;

	public Text[] coinTextList;

	private int currentCoin;

	private void Awake()
	{
		EventManager.StartListening("EventCoinChanges", new UnityAction(this.UpdateCoinsHUD));
	}

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			this.coinsText = this.coinTextList[1];
			this.coinHolderList[0].SetActive(false);
			this.coinHolderList[1].SetActive(true);
			this.coinTextList[0].gameObject.SetActive(false);
		}
		else
		{
			this.coinsText = this.coinTextList[0];
			this.coinHolderList[0].SetActive(true);
			this.coinHolderList[1].SetActive(false);
			this.coinTextList[1].gameObject.SetActive(false);
		}
		this.currentCoin = GameManager.Coin;
		this.coinsText.gameObject.SetActive(true);
		this.coinsText.text = GameManager.CurrencyToString((float)this.currentCoin);
	}

	private void UpdateCoinsHUDNoScaling()
	{
		int currentCoin = int.Parse(this.coinsText.text);
		DOTween.To(() => currentCoin, delegate(int x)
		{
			currentCoin = x;
		}, GameManager.Coin, 1f * Time.timeScale).OnUpdate(delegate
		{
			this.coinsText.text = currentCoin.ToString();
		});
	}

	private void UpdateCoinsHUD()
	{
		base.transform.GetComponent<RectTransform>().DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(delegate
		{
			DOTween.To(() => this.currentCoin, delegate(int x)
			{
				this.currentCoin = x;
			}, GameManager.Coin, 1f * Time.timeScale).OnUpdate(delegate
			{
				this.coinsText.text = GameManager.CurrencyToString((float)this.currentCoin);
			}).OnComplete(delegate
			{
				base.transform.GetComponent<RectTransform>().DOScale(Vector3.one * 1f, 0.2f);
			});
		});
	}
}
