using DG.Tweening;
using I2.Loc;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class NextStepDoubleEarnings : NextStep
{
	public Text coinCollected;

	public Text doubleCoinText;

	public GameObject coinObject;

	public GameObject adsButton;

	public GameObject doubleCoinsHolder;

	private int successWatchAds;

	public override bool ShouldShow()
	{
		this.coinCollected.text = GameManager.CurrencyToString((float)GameManager.ThisGameCoin);
		this.OnChangeLanguage();
		return  false /*GameManager.ThisGameCoin >= 20 && AdsControl.Instance.GetRewardAvailable()*/;
	}

	public void OnPressWatchAdsToDoubleCoins()
	{
		SoundManager.PlayTapSFX();
        /*
		if (HeyZapManager.IsAdReady())
		{
			HeyZapManager.ShowRewardedVideoWithCallBack(new HeyZapManager.CallBackFunction(this.SuccessShownAds), null);
		}
		*/
        // if (AdsControl.Instance.GetRewardAvailable())
        // {
        //     AdsControl.Instance.PlayDelegateRewardVideo(delegate
        //     {
                SuccessShownAds();
            // });
        // }
    }

	public void UpdateCoins()
	{
		this.coinCollected.text = GameManager.CurrencyToString((float)GameManager.ThisGameCoin);
	}

	protected override void OnChangeLanguage()
	{
		if (this.successWatchAds == 0)
		{
			this.doubleCoinText.text = ScriptLocalization.Get("Double Coins");
		}
		else if (this.successWatchAds == 1)
		{
			this.doubleCoinText.text = ScriptLocalization.Get("Triple Coins");
		}
	}

	private void SuccessShownAds()
	{
		this.successWatchAds++;
		if (this.successWatchAds == 1)
		{
			Analytics.CustomEvent("WatchAdsToDoubleCoin", new Dictionary<string, object>
			{
				{
					"ThisGameCoins",
					GameManager.ThisGameCoin
				},
				{
					"TotalCoins",
					GameManager.Coin
				}
			});
		}
		else if (this.successWatchAds == 2)
		{
			Analytics.CustomEvent("WatchAdsToTripleCoin", new Dictionary<string, object>
			{
				{
					"ThisGameCoins",
					GameManager.ThisGameCoin
				},
				{
					"TotalCoins",
					GameManager.Coin
				}
			});
		}
		if (GameManager.ThisGameCoin > 0)
		{
			if (this.successWatchAds == 1)
			{
				this.coinCollected.text = GameManager.CurrencyToString((float)GameManager.ThisGameCoin) + "x2";
			}
			else if (this.successWatchAds == 2)
			{
				this.coinCollected.text = GameManager.CurrencyToString((float)GameManager.ThisGameCoin) + "x3";
			}
			this.coinObject.GetComponent<RectTransform>().DOScale(Vector3.one * 1.75f, 0.2f).SetDelay(0.2f).OnComplete(delegate
			{
				GameManager.ExtraCoinExcludeThisGameCoin(GameManager.ThisGameCoin);
				EventManager.TriggerEvent("EventCoinChanges");
				this.coinObject.GetComponent<RectTransform>().DOScale(Vector3.one * 1f, 0.2f).SetDelay(0.8f);
			});
		}
		if (this.successWatchAds == 1)
		{
			this.OnChangeLanguage();
		}
		else
		{
			this.doubleCoinsHolder.SetActive(false);
		}
	}
}
