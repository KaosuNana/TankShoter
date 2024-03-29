using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	private sealed class _ShowMissionBannerIfAnyActiveForDuration_c__AnonStorey1
	{
		internal float duration;

		internal MissionManager _this;

		internal void __m__0()
		{
			this._this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this._this.yHidePos, 0.3f, false).SetUpdate(true).SetDelay(this.duration);
		}
	}

	private sealed class _WaitForToasterBeforeShowMissionToReward_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal MissionManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _WaitForToasterBeforeShowMissionToReward_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				if (!this._this.isShowingMissionComplete)
				{
					goto IL_6A;
				}
				break;
			case 1u:
				break;
			default:
				return false;
			}
			if (this._this.isShowingMissionComplete)
			{
				this._current = new WaitForSeconds(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			IL_6A:
			this._this.ShowMissionToReward();
			this._PC = -1;
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public GameObject missionCanvas;

	public int startIndexOfRandomisation;

	public Text missionProgressText;

	public MissionTimerController missionTimerInstance;

	public GameObject okButton;

	public MissionSlotManager[] slotManagerInstance;

	public Mission[] missionPrefabs;

	private bool _needToShowMissionComplete_k__BackingField;

	protected bool isShowingMissionComplete;

	private bool hasFinishedRewarding;

	private int completedMission;

	protected float yHidePos = 155f;

	protected float yShowPosToaster;

	protected float yShowPosResultScreen = -145f;

	protected float yExtraPadding;

	private static TweenCallback __f__am_cache0;

	public bool needToShowMissionComplete
	{
		get;
		set;
	}

	private void Awake()
	{
	}

	private void Start()
	{
		this.yExtraPadding = (float)((!GameSingleton.IsiPhoneX) ? 0 : (-80));
	}

	private List<int> GetAllMissionPrefabIndexForLevel(int level)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.missionPrefabs.Length; i++)
		{
			this.missionPrefabs[i].missionIndex = i;
			if (this.missionPrefabs[i].missionLevel == (Mission.MissionLevel)(level - 1))
			{
				list.Add(i);
			}
			else if (level > 1 && this.missionPrefabs[i].missionLevel == (Mission.MissionLevel)level)
			{
				list.Add(i);
			}
		}
		return list;
	}

	public void SetMissionTimer(float timer)
	{
		this.missionTimerInstance.SetTimer(timer);
	}

	public void EnableMission()
	{
		this.missionCanvas.SetActive(true);
	}

	public void DisableMission()
	{
		this.missionCanvas.SetActive(false);
	}

	public float GetTheEarliestTimeCountdown()
	{
		float num = 0f;
		for (int i = 0; i < this.slotManagerInstance.Length; i++)
		{
			if (this.slotManagerInstance[i].timeRemaining > 0f)
			{
				if (num == 0f)
				{
					num = this.slotManagerInstance[i].timeRemaining;
				}
				else if (this.slotManagerInstance[i].timeRemaining < num)
				{
					num = this.slotManagerInstance[i].timeRemaining;
				}
			}
		}
		return num;
	}

	public void ShowMissionBannerIfAnyActiveForDuration(float duration)
	{
		if (this.slotManagerInstance[0].activeMission == null)
		{
			return;
		}
		this.missionTimerInstance.ShowTimer();
		this.missionCanvas.GetComponent<RectTransform>().DOKill(false);
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yShowPosToaster + this.yExtraPadding, 0.3f, false).SetUpdate(true).OnComplete(delegate
		{
			this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yHidePos, 0.3f, false).SetUpdate(true).SetDelay(duration);
		});
		this.slotManagerInstance[0].UpdateProgressBanner();
	}

	public virtual void ShowMissionBanner()
	{
		this.missionCanvas.GetComponent<RectTransform>().DOKill(false);
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yShowPosResultScreen + this.yExtraPadding, 0.3f, false).SetEase(Ease.OutBack).SetUpdate(true);
		this.slotManagerInstance[0].UpdateProgressBanner();
	}

	public virtual void HideMissionBanner()
	{
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yHidePos, 0.2f, false).SetEase(Ease.InBack).SetUpdate(true);
	}

	public virtual void GenerateMissionForSlot(int slot)
	{
		int num;
		if (PlayerPrefs.HasKey("Mission" + slot))
		{
			num = PlayerPrefs.GetInt("Mission" + slot);
		}
		else
		{
			int @int = PlayerPrefs.GetInt("NumberOfMissionCompleted", 0);
			if (@int >= this.startIndexOfRandomisation)
			{
				num = UnityEngine.Random.Range(this.startIndexOfRandomisation, this.missionPrefabs.Length);
			}
			else
			{
				num = @int;
			}
		}
		PlayerPrefs.SetInt("Mission" + slot, num);
		Mission mission = UnityEngine.Object.Instantiate<Mission>(this.missionPrefabs[num]);
		mission.missionManagerInstance = this;
		mission.transform.SetParent(this.slotManagerInstance[slot].transform, false);
		mission.slotIndex = slot;
		this.slotManagerInstance[slot].activeMission = mission;
	}

	public virtual void StartMissionToReward()
	{
		if (this.isShowingMissionComplete)
		{
			base.StartCoroutine("WaitForToasterBeforeShowMissionToReward");
		}
		else
		{
			this.ShowMissionToReward();
		}
	}

	protected void ShowMissionToReward()
	{
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yShowPosToaster + this.yExtraPadding, 0.5f, false).SetEase(Ease.OutBack).OnComplete(delegate
		{
			this.RewardMission();
			this.needToShowMissionComplete = false;
		});
	}

	public void ResetPosition()
	{
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yHidePos, 0f, false);
	}

	private IEnumerator WaitForToasterBeforeShowMissionToReward()
	{
		MissionManager._WaitForToasterBeforeShowMissionToReward_c__Iterator0 _WaitForToasterBeforeShowMissionToReward_c__Iterator = new MissionManager._WaitForToasterBeforeShowMissionToReward_c__Iterator0();
		_WaitForToasterBeforeShowMissionToReward_c__Iterator._this = this;
		return _WaitForToasterBeforeShowMissionToReward_c__Iterator;
	}

	public virtual void RewardMission()
	{
		this.completedMission = 0;
		for (int i = 0; i < this.slotManagerInstance.Length; i++)
		{
			if (this.slotManagerInstance[i].activeMission.isCompleted)
			{
				PlayerPrefs.DeleteKey("Mission" + i);
				this.slotManagerInstance[i].RewardActiveMissionWithDelay(0.25f + 0.5f * (float)i);
				this.completedMission++;
			}
		}
		base.Invoke("FinishedRewardingMission", 1.18f + 0.35f * (float)this.completedMission);
	}

	private void HideMissionBannerAndRemoveRewardedMission()
	{
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yHidePos, 0.2f, false).SetEase(Ease.InBack).OnComplete(delegate
		{
			this.slotManagerInstance[0].UpdateSlot();
			if (PlayerPrefs.GetInt("NumberOfMissionCompleted", 0) > 1)
			{
				GameManager.UpdateAllLocalNotification();
			}
		});
	}

	protected virtual void FinishedRewardingMission()
	{
		this.HideMissionBannerAndRemoveRewardedMission();
	}

	protected void ShowOkButton()
	{
		this.okButton.SetActive(true);
		this.okButton.GetComponent<RectTransform>().localScale = Vector3.zero;
		this.okButton.GetComponent<RectTransform>().DOScale(Vector3.one, 0.3f).SetDelay(0.4f).SetEase(Ease.OutBack);
	}

	public void OnPressNextButton()
	{
		SoundManager.PlayTapSFX();
		this.FinishUpMissionAndCallback();
	}

	public virtual void FinishUpMissionAndCallback()
	{
		this.okButton.SetActive(false);
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(550f, 0.3f, false).OnComplete(delegate
		{
		});
	}

	public void UpdateMissionProgress(string progressText)
	{
		this.missionProgressText.text = progressText;
		this.missionProgressText.gameObject.SetActive(true);
		Color color = this.missionProgressText.color;
		color.a = 1f;
		this.missionProgressText.color = color;
		this.missionProgressText.DOKill(false);
		this.missionProgressText.DOFade(0f, 0.5f).SetDelay(1f).OnComplete(delegate
		{
			this.missionProgressText.gameObject.SetActive(false);
		});
	}

	public bool IsAnyMissionCompleted()
	{
		for (int i = 0; i < this.slotManagerInstance.Length; i++)
		{
			if (this.slotManagerInstance[i].IsActiveMissionCompleted())
			{
				return true;
			}
		}
		return false;
	}

	public void CompleteMission(int index, bool timedMission)
	{
		this.needToShowMissionComplete = true;
		this.isShowingMissionComplete = true;
		if (timedMission)
		{
			this.missionTimerInstance.gameObject.SetActive(false);
		}
		this.missionCanvas.SetActive(true);
		this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yShowPosToaster + this.yExtraPadding, 0.3f, false).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(delegate
		{
			this.missionCanvas.GetComponent<RectTransform>().DOAnchorPosY(this.yHidePos, 0.2f, false).SetDelay(1.5f).SetEase(Ease.InBack).OnComplete(delegate
			{
				this.isShowingMissionComplete = false;
			});
		});
	}
}
