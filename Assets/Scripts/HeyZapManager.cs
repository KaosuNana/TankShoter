/*
using Heyzap;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HeyZapManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	private static HeyZapManager instance;

	protected HeyZapManager.CallBackFunction callbackFct;

	protected HeyZapManager.CallBackFunction failCallbackFct;

	private int totalNumberOfSessionNotWatchingAds;

	private static HZIncentivizedAd.AdDisplayListener __f__am_cache0;

	private static HZVideoAd.AdDisplayListener __f__am_cache1;

	private static HZIncentivizedAd.AdDisplayListener __f__am_cache2;

	private static HZIncentivizedAd.AdDisplayListener __f__am_cache3;

	private void Awake()
	{
		if (HeyZapManager.instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
		HeyZapManager.instance = this;
		HeyzapAds.Start("d8d21c3e9c6844f147314aa69fda3bd2", 0);
		HeyzapAds.HideDebugLogs();
		HZIncentivizedAd.Fetch();
	}

	private void Start()
	{
		HZIncentivizedAd.AdDisplayListener displayListener = delegate(string adState, string adTag)
		{
			if (adState.Equals("available"))
			{
				EventManager.TriggerEvent("EventAdsAvailable");
			}
		};
		HZIncentivizedAd.SetDisplayListener(displayListener);
	}

	private void ResetStatsBecauseUserHasWatchedAds()
	{
		this.totalNumberOfSessionNotWatchingAds = 0;
	}

	public static void ResumeExpensiveWork()
	{
		HeyzapAds.ResumeExpensiveWork();
	}

	public static bool IsAdReady()
	{
		if (!HZIncentivizedAd.IsAvailable())
		{
			HeyZapManager.instance.RequestPermission();
		}
		return HZIncentivizedAd.IsAvailable();
	}

	private void RequestPermission()
	{
		if (!UniAndroidPermission.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE))
		{
			UniAndroidPermission.RequestPremission(AndroidPermission.WRITE_EXTERNAL_STORAGE, null, null);
		}
	}

	public static void ShouldShowSkippableVideo()
	{
		HeyZapManager.instance.totalNumberOfSessionNotWatchingAds++;
		if (GameManager.HasPlayerPurchasedSomething)
		{
			return;
		}
		if (!HZVideoAd.IsAvailable() && HeyZapManager.instance.totalNumberOfSessionNotWatchingAds % 3 == 2)
		{
			HZVideoAd.Fetch();
		}
		if (HeyZapManager.instance.totalNumberOfSessionNotWatchingAds < 3)
		{
			return;
		}
		if (HZVideoAd.IsAvailable())
		{
			HZVideoAd.Show();
			HeyZapManager.instance.ResetStatsBecauseUserHasWatchedAds();
			SoundManager.PauseMusic();
			HZVideoAd.AdDisplayListener displayListener = delegate(string adState, string adTag)
			{
				if (adState.Equals("audio_finished"))
				{
					SoundManager.UnPauseMusic();
				}
			};
			HZVideoAd.SetDisplayListener(displayListener);
		}
	}

	public static void ShowRewardedVideoWithCallBack(HeyZapManager.CallBackFunction myCallBack, HeyZapManager.CallBackFunction failCallBack)
	{
		HeyZapManager.instance.callbackFct = myCallBack;
		HeyZapManager.instance.failCallbackFct = failCallBack;
		if (HZIncentivizedAd.IsAvailable())
		{
			HZIncentivizedAd.Show();
			SoundManager.PauseMusic();
			HZIncentivizedAd.AdDisplayListener displayListener = delegate(string adState, string adTag)
			{
				if (adState.Equals("incentivized_result_complete"))
				{
					HeyZapManager.instance.ResetStatsBecauseUserHasWatchedAds();
					SoundManager.UnPauseMusic();
					HeyZapManager.instance.RequestPermission();
					HeyZapManager.instance.callbackFct();
				}
				if (adState.Equals("incentivized_result_incomplete"))
				{
					SoundManager.UnPauseMusic();
					HeyZapManager.instance.RequestPermission();
					if (HeyZapManager.instance.failCallbackFct != null)
					{
						HeyZapManager.instance.failCallbackFct();
					}
				}
				if (adState.Equals("available"))
				{
					EventManager.TriggerEvent("EventAdsAvailable");
				}
			};
			HZIncentivizedAd.SetDisplayListener(displayListener);
		}
	}

	public static void ShowRewardedVideoWithCallBack(HeyZapManager.CallBackFunction myCallBack)
	{
		HeyZapManager.instance.callbackFct = myCallBack;
		if (HZIncentivizedAd.IsAvailable())
		{
			HZIncentivizedAd.Show();
			SoundManager.PauseMusic();
			HZIncentivizedAd.AdDisplayListener displayListener = delegate(string adState, string adTag)
			{
				if (adState.Equals("incentivized_result_complete"))
				{
					HeyZapManager.instance.ResetStatsBecauseUserHasWatchedAds();
					SoundManager.UnPauseMusic();
					HeyZapManager.instance.callbackFct();
				}
				if (adState.Equals("incentivized_result_incomplete"))
				{
					SoundManager.UnPauseMusic();
					if (HeyZapManager.instance.failCallbackFct != null)
					{
						HeyZapManager.instance.failCallbackFct();
					}
				}
			};
			HZIncentivizedAd.SetDisplayListener(displayListener);
		}
	}
}
*/