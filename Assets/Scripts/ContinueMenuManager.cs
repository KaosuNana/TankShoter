using DG.Tweening;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContinueMenuManager : MonoBehaviour
{
	public AudioClip continueTickingSFX;

	public Text countdownText;

	public Text continueCost;

	public float maxCountdown = 8f;

	private float timer;

	public GameObject[] continueButtonList;

	public Text amountInfo;

	public Text feedbackText;

	private bool timeIsUp;

	private bool shouldStopCountDown;

	private int boltCost;

	private void OnEnable()
	{
		this.timer = this.maxCountdown;
		this.timeIsUp = false;
		this.shouldStopCountDown = false;
		this.amountInfo.text = GameManager.ContinueAmount.ToString() + "/2";
		this.boltCost = 10 * GameManager.ContinueAmount;
		this.continueCost.text = GameManager.CurrencyToString((float)this.boltCost);
		this.continueButtonList[0].SetActive(!GameManager.HasPurchasedFreeContinue);
		this.continueButtonList[1].SetActive(!GameManager.HasPurchasedFreeContinue);
		this.continueButtonList[2].SetActive(GameManager.HasPurchasedFreeContinue);
		this.feedbackText.gameObject.SetActive(false);
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressRejectButton));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressRejectButton));
	}

	private void Update()
	{
		if (!this.timeIsUp && !this.shouldStopCountDown)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.timer -= 1f;
			}
			string text = ((int)this.timer).ToString();
			if (!string.Equals(text, this.countdownText.text, StringComparison.Ordinal) && SoundManager.IsSFXOn)
			{
				base.GetComponent<AudioSource>().clip = this.continueTickingSFX;
				base.GetComponent<AudioSource>().Play();
			}
			this.countdownText.text = text;
			this.timer -= Time.deltaTime;
			if (this.timer <= 0f)
			{
				this.timeIsUp = true;
				GameManager.RejectOrContinueCountdownFinish();
			}
		}
	}

	private static void SetLayerOnAllRecursive(GameObject obj, int layer)
	{
		obj.layer = layer;
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				ContinueMenuManager.SetLayerOnAllRecursive(transform.gameObject, layer);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public void OnPressRejectButton()
	{
		SoundManager.PlayTapSFX();
		base.GetComponent<AudioSource>().Stop();
		GameManager.RejectOrContinueCountdownFinish();
	}

	public void OnPressWatchAdsToContinue()
	{
		SoundManager.PlayTapSFX();
		base.GetComponent<AudioSource>().Stop();
		this.shouldStopCountDown = true;
		Analytics.CustomEvent("WatchAdsToContinue", new Dictionary<string, object>
		{
			{
				"thisGameCoins",
				GameManager.Coin
			},
			{
				"continueAmount",
				GameManager.ContinueAmount
			}
		});
        /*
		if (HeyZapManager.IsAdReady())
		{
			HeyZapManager.ShowRewardedVideoWithCallBack(new HeyZapManager.CallBackFunction(this.SuccessToContinue),
             new HeyZapManager.CallBackFunction(this.FailToContinue));
		}
		*/
        SuccessToContinue();
  //       if(AdsControl.Instance.GetRewardAvailable())
  //       {
  //           AdsControl.Instance.PlayDelegateRewardVideo(delegate
  //           {
  //               SuccessToContinue();
  //           });
  //       }
  //       else
		// {
		// 	this.AnimateFeedback(ScriptLocalization.Get("Ads not ready. Please try again."));
		// }
	}

	private void AnimateFeedback(string feedback)
	{
		this.feedbackText.gameObject.SetActive(true);
		this.feedbackText.text = feedback;
		this.feedbackText.DOKill(false);
		this.feedbackText.DOFade(0f, 0f);
		this.feedbackText.DOFade(1f, 0.2f).OnComplete(delegate
		{
			this.feedbackText.DOFade(0f, 0.5f).SetDelay(1f).OnComplete(delegate
			{
				this.feedbackText.gameObject.SetActive(false);
			});
		});
	}

	public void OnPressFreeToContinue()
	{
		SoundManager.PlayTapSFX();
		GameManager.ContinueGame();
	}

	private void SuccessToContinue()
	{
		this.shouldStopCountDown = false;
		GameManager.ContinueGame();
	}

	private void FailToContinue()
	{
		this.shouldStopCountDown = false;
		GameManager.RejectOrContinueCountdownFinish();
	}

	public void OnPressPayCoinToContinue()
	{
		SoundManager.PlayTapSFX();
		this.shouldStopCountDown = true;
		if (GameManager.ReduceBoltByShouldOfferShop(10 * GameManager.ContinueAmount, false))
		{
			base.GetComponent<AudioSource>().Stop();
			Analytics.CustomEvent("PayCoinContinue", new Dictionary<string, object>
			{
				{
					"thisGameCoins",
					GameManager.Coin
				},
				{
					"continueAmount",
					GameManager.ContinueAmount
				}
			});
			GameManager.ContinueGame();
		}
		else
		{
			this.AnimateFeedback(ScriptLocalization.Get("Not enough bolts"));
		}
	}
}
