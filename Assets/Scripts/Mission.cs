using DG.Tweening;
using I2.Loc;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public abstract class Mission : MonoBehaviour
{
	public enum MissionLevel
	{
		Level1,
		Level2,
		Level3,
		Level4,
		Level5,
		Level6
	}

	public enum MissionReward
	{
		RewardCoin,
		RewardBolt
	}

	public MissionPlaceholder missionPlaceholderPrefab;

	private MissionManager _missionManagerInstance_k__BackingField;

	private Text _missionDescriptionText_k__BackingField;

	private int _slotIndex_k__BackingField;

	private int _missionIndex_k__BackingField;

	private bool _isCompleted_k__BackingField;

	private GameObject _rewardObject_k__BackingField;

	private Image _progressImage_k__BackingField;

	public string missionDescription;

	protected string localisedMissionDescription;

	public int[] successAmountRandom;

	public int successAmount;

	private Text _rewardDescription_k__BackingField;

	public Mission.MissionLevel missionLevel;

	protected string savedFileName;

	private float _fileVersion_k__BackingField;

	public Mission.MissionReward missionReward;

	public int rewardAmount;

	public int progressAmount;

	private static TweenCallback __f__am_cache0;

	private static TweenCallback __f__am_cache1;

	public MissionManager missionManagerInstance
	{
		get;
		set;
	}

	public Text missionDescriptionText
	{
		get;
		set;
	}

	public int slotIndex
	{
		get;
		set;
	}

	public int missionIndex
	{
		get;
		set;
	}

	public bool isCompleted
	{
		get;
		set;
	}

	public GameObject rewardObject
	{
		get;
		set;
	}

	public Image progressImage
	{
		get;
		set;
	}

	public Text rewardDescription
	{
		get;
		set;
	}

	public float fileVersion
	{
		get;
		set;
	}

	public virtual void LoadMissionProgress()
	{
	}

	public void RewardWithDelay(float delay)
	{
		Mission.MissionReward missionReward = this.missionReward;
		if (missionReward != Mission.MissionReward.RewardCoin)
		{
			if (missionReward == Mission.MissionReward.RewardBolt)
			{
				this.rewardObject.GetComponent<RectTransform>().DOScale(2f, 0.3f).SetDelay(delay).SetEase(Ease.OutElastic).OnStart(delegate
				{
				}).OnComplete(delegate
				{
					this.rewardObject.GetComponent<RectTransform>().DOScale(0f, 0.2f).SetDelay(0.1f).SetEase(Ease.InBack).OnComplete(delegate
					{
						GameManager.ExtraBoltWithBoltBurstMissionReward(this.rewardAmount);
					});
				});
			}
		}
		else
		{
			this.rewardObject.GetComponent<RectTransform>().DOScale(2f, 0.3f).SetDelay(delay).SetEase(Ease.OutElastic).OnStart(delegate
			{
			}).OnComplete(delegate
			{
				this.rewardObject.GetComponent<RectTransform>().DOScale(0f, 0.2f).SetDelay(0.1f).SetEase(Ease.InBack).OnComplete(delegate
				{
					GameManager.ExtraCoinWithCoinBurstMissionReward(this.rewardAmount);
				});
			});
		}
	}

	public void DeleteMissionProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Delete(this.savedFileName);
		}
	}

	private void Awake()
	{
		if (this.missionReward == Mission.MissionReward.RewardCoin)
		{
			this.rewardAmount *= GameManager.CoinMultiplier;
		}
		this.UpdateMissionPlaceholder();
		LocalizationManager.OnLocalizeEvent += new LocalizationManager.OnLocalizeCallback(this.OnChangeLanguage);
		this.localisedMissionDescription = ScriptLocalization.Get(this.missionDescription);
		if (this.successAmountRandom.Length > 0)
		{
			this.successAmount = this.successAmountRandom[UnityEngine.Random.Range(0, this.successAmountRandom.Length)];
		}
		this.savedFileName = base.gameObject.name;
		this.LoadMissionProgress();
		this.UpdateReward();
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
		this.localisedMissionDescription = ScriptLocalization.Get(this.missionDescription);
		this.UpdateDescription();
	}

	private void UpdateMissionPlaceholder()
	{
		this.missionPlaceholderPrefab = UnityEngine.Object.Instantiate<MissionPlaceholder>(this.missionPlaceholderPrefab);
		this.missionPlaceholderPrefab.transform.SetParent(base.transform, false);
		this.missionDescriptionText = this.missionPlaceholderPrefab.missionDescriptionText;
		this.progressImage = this.missionPlaceholderPrefab.progressImage;
		this.rewardDescription = this.missionPlaceholderPrefab.rewardDescription[(int)this.missionReward];
		this.rewardObject = this.missionPlaceholderPrefab.rewardObject[(int)this.missionReward];
		this.rewardObject.SetActive(true);
		if (this.missionReward == Mission.MissionReward.RewardCoin)
		{
			this.rewardDescription.gameObject.SetActive(true);
		}
	}

	private void UpdateReward()
	{
		this.rewardDescription.text = GameManager.CurrencyToString((float)this.rewardAmount);
	}

	protected void UpdateProgress()
	{
		if (this.progressAmount < this.successAmount)
		{
			this.StartListening();
		}
		this.UpdateProgressImage();
	}

	public abstract void UpdateDescription();

	public void UpdateProgressImage()
	{
		if (this.progressAmount < this.successAmount)
		{
			this.progressImage.color = this.missionPlaceholderPrefab.progressColor[0];
			this.progressImage.DOKill(false);
			this.progressImage.DOFillAmount((float)this.progressAmount / (float)this.successAmount, 0.5f).SetDelay(0.5f).SetUpdate(true);
		}
		else
		{
			this.progressImage.color = this.missionPlaceholderPrefab.progressColor[1];
			this.progressImage.DOKill(false);
			this.progressImage.DOFillAmount(1f, 0.2f).SetDelay(0.3f).SetUpdate(true);
			this.isCompleted = true;
		}
	}

	private void OnEnable()
	{
		this.UpdateProgress();
	}

	public virtual void StartListening()
	{
	}

	public virtual void StopListening()
	{
	}

	protected void MissionComplete(bool timedMission)
	{
		if (this.isCompleted)
		{
			return;
		}
		this.isCompleted = true;
		PlayerPrefs.SetInt("NumberOfMissionCompleted", PlayerPrefs.GetInt("NumberOfMissionCompleted", 0) + 1);
		this.missionManagerInstance.CompleteMission(this.slotIndex, timedMission);
		this.UpdateProgressImage();
	}
}
