using I2.Loc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MissionSlotManager : MonoBehaviour
{
	public MissionManager missionManagerInstance;

	public GameObject MissionCountdown;

	public Button missionCountdownAdButton;

	public Text description;

	public Image progressImage;

	public int slot;

	public float timeCountdownRequirement;

	private string savedCountdownTimeString;

	public float timeRemaining;

	private Mission _activeMission_k__BackingField;

	private string localisedDescription;

	public Mission activeMission
	{
		get;
		set;
	}

	private void Awake()
	{
		LocalizationManager.OnLocalizeEvent += new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
		this.localisedDescription = ScriptLocalization.Get("New Mission in {0}");
		this.UpdateSlot();
	}

	public virtual void UpdateSlot()
	{
		this.savedCountdownTimeString = "StartCountdownTime" + this.slot.ToString();
		if (PlayerPrefs.HasKey(this.savedCountdownTimeString))
		{
			if (GameManager.HasPlayerPurchasedSomething)
			{
				this.SuccessShownAds();
				return;
			}
			this.MissionCountdown.SetActive(true);
			if (this.missionCountdownAdButton != null)
			{
				// if (AdsControl.Instance.GetRewardAvailable())
				// {
				// 	this.missionCountdownAdButton.gameObject.SetActive(true);
				// }
				// else
				// {
					this.missionCountdownAdButton.gameObject.SetActive(false);
				// }
			}
			this.UpdateTimeRemaining();
			if (this.activeMission != null)
			{
				UnityEngine.Object.Destroy(this.activeMission.gameObject);
			}
		}
		else
		{
			this.GetMission();
		}
	}

	private void OnDestroy()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnDisable()
	{
		LocalizationManager.OnLocalizeEvent -= new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
	}

	private void OnChangeLanguage()
	{
		this.localisedDescription = ScriptLocalization.Get("New Mission in {0}");
	}

	public void RewardActiveMissionWithDelay(float delay)
	{
		if (this.activeMission.GetComponent<Mission>() != null)
		{
			this.activeMission.GetComponent<Mission>().RewardWithDelay(delay);
			this.activeMission.GetComponent<Mission>().DeleteMissionProgress();
		}
		this.StartCountingDown();
	}

	public void OnPressRewardedVideo()
	{
		SoundManager.PlayTapSFX();
		Analytics.CustomEvent("WatchAdsToSkipCountdown", new Dictionary<string, object>
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
        //     });
        // }
    }

	private void SuccessShownAds()
	{
		this.RemoveCountdownTime();
		this.GetMission();
	}

	public void UpdateProgressBanner()
	{
		if (this.activeMission != null)
		{
			this.activeMission.UpdateDescription();
			this.activeMission.UpdateProgressImage();
		}
	}

	public bool IsActiveMissionCompleted()
	{
		return !(this.activeMission == null) && this.activeMission.isCompleted;
	}

	protected virtual void GetMission()
	{
		this.RemoveCountdownTime();
		this.MissionCountdown.SetActive(false);
		this.missionManagerInstance.GenerateMissionForSlot(this.slot);
	}

	public void refreshMission()
	{
		if (this.activeMission != null)
		{
			this.activeMission.DeleteMissionProgress();
			this.activeMission.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(this.activeMission.gameObject);
			this.GetMission();
		}
		else
		{
			this.MissionCountdown.SetActive(false);
			this.GetMission();
			this.RemoveCountdownTime();
		}
	}

	public void StartCountingDown()
	{
		this.SaveCountdownTime();
		this.UpdateTimeRemaining();
	}

	private void SaveCountdownTime()
	{
		DateTime dateTime = DateTime.Now;
		PlayerPrefs.SetString(this.savedCountdownTimeString, dateTime.ToFileTime().ToString());
	}

	private void RemoveCountdownTime()
	{
		if (PlayerPrefs.HasKey(this.savedCountdownTimeString))
		{
			PlayerPrefs.DeleteKey(this.savedCountdownTimeString);
		}
	}

	private void UpdateTimeRemaining()
	{
		if (PlayerPrefs.HasKey(this.savedCountdownTimeString))
		{
			this.timeRemaining = this.timeCountdownRequirement;
			DateTime d = DateTime.Now;
			DateTime d2 = DateTime.FromFileTime(Convert.ToInt64(PlayerPrefs.GetString(this.savedCountdownTimeString)));
			TimeSpan timeSpan = d - d2;
			float num = (float)(timeSpan.Hours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds);
			this.timeRemaining = this.timeCountdownRequirement - num;
			if (this.timeRemaining <= 0f)
			{
				this.GetMission();
			}
		}
	}

	private string ConvertFloatToTime(float timer)
	{
		if (timer >= 3600f)
		{
			return string.Format("{0}:{1:00}:{2:00}", (int)timer / 3600, (int)(timer % 3600f) / 60, (int)timer % 60);
		}
		return string.Format("{0:00}:{1:00}", (int)timer / 60, (int)timer % 60);
	}

	private void Update()
	{
		if (this.timeRemaining > 0f)
		{
			this.timeRemaining -= Time.unscaledDeltaTime;
			this.description.text = string.Format(this.localisedDescription, this.ConvertFloatToTime(this.timeRemaining));
			this.progressImage.fillAmount = (this.timeCountdownRequirement - this.timeRemaining) / this.timeCountdownRequirement;
			if (this.timeRemaining < 0f)
			{
				this.GetMission();
			}
		}
	}
}
