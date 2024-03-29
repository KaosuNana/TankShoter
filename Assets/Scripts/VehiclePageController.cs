using DG.Tweening;
using I2.Loc;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VehiclePageController : MonoBehaviour
{
	public VehicleManager vehicleManagerInstance;

	public Text currentLevelText;

	public GameObject prestigeButton;

	public Text vehicleName;

	public Text spcName;

	public Color[] vehicleNameColor;

	public Color[] spcNameColor;

	public Text coinText;

	public Text boltText;

	public ScrollRect scrollRect;

	public AudioClip slamSFX;

	public AudioClip successUnlockSFX;

	public AudioClip upgradeSFX;

	public ParticleSystem upgradeBeamParticle;

	public Text[] levelTexts;

	public Text[] infoTexts;

	public Text[] costTexts;

	public Text requirementText;

	public GameObject requirementHolder;

	public Text unlockCostText;

	public GameObject slamParticle;

	public GameObject focusSlotVehicle;

	public RectTransform upgradeCover;

	public Shader standardShader;

	public Shader greyscaleShader;

	public AudioClip popSFX;

	public AudioClip swooshSFX;

	private Vehicle currentVehicle;

	public GameObject pageContent;

	public VehiclePageSlotHolder slotHolderPrefab;

	private List<int> teamPositionContent;

	private int upgradeCostAttack;

	private int upgradeCostHP;

	private int upgradeCostCharge;

	private List<Image> vehicleImageList;

	private Tween openTween;

	private static TweenCallback __f__am_cache0;

	private void Start()
	{
		this.GenerateVehicles();
		this.upgradeCover.gameObject.SetActive(false);
		this.upgradeCover.anchoredPosition = new Vector2(1000f, 0f);
		this.currentLevelText.text = ScriptLocalization.Get("LEVEL").ToUpper() + " " + GameManager.CurrentLevel.ToString();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (this.currentVehicle != null)
		{
			SoundManager.PlaySFXInArray(this.popSFX, base.transform.position, 1f);
			this.currentVehicle.SendBackToOriginalSlot();
			this.upgradeCover.gameObject.SetActive(true);
			this.openTween.Kill(false);
			this.upgradeCover.DOAnchorPosX(10f, 0.15f, false).OnComplete(delegate
			{
				this.UpdateVehicleInfo();
			});
			this.currentVehicle = other.GetComponent<Vehicle>();
			if (this.currentVehicle.vehicleInfoInstance.isUnlocked)
			{
				this.requirementHolder.SetActive(false);
				this.openTween = this.upgradeCover.DOAnchorPosX(1000f, 0.25f, false).SetDelay(0.2f).OnStart(delegate
				{
					SoundManager.PlaySFXInArray(this.swooshSFX, base.transform.position, 1f);
				}).OnComplete(delegate
				{
					this.upgradeCover.gameObject.SetActive(false);
				});
			}
			else
			{
				this.requirementHolder.SetActive(true);
				this.requirementText.text = string.Format(ScriptLocalization.Get("Unlock at Level {0}").ToUpper(), this.currentVehicle.requiredLevelToUnlock);
				this.unlockCostText.text = this.currentVehicle.boltCostToUnlock.ToString();
			}
		}
		else
		{
			this.currentVehicle = other.GetComponent<Vehicle>();
			this.UpdateVehicleInfo();
		}
		if (this.currentVehicle.vehicleInfoInstance.isUnlocked)
		{
			this.vehicleManagerInstance.SetMainVehicle(this.currentVehicle.vehicleID);
		}
		this.currentVehicle.transform.SetParent(this.focusSlotVehicle.transform, false);
		this.currentVehicle.transform.localPosition = Vector3.zero;
		this.currentVehicle.transform.localScale = Vector3.one;
		this.focusSlotVehicle.transform.DOKill(false);
		this.focusSlotVehicle.transform.DOScale(Vector3.one * 150f, 0.1f).OnComplete(delegate
		{
			this.focusSlotVehicle.transform.DOScale(Vector3.one * 120f, 0.1f);
		});
	}

	public void SetCurrentVehicle(Vehicle inputVehicle)
	{
		this.currentVehicle = inputVehicle;
		this.UpdateVehicleInfo();
	}

	private void UpdateVehicleInfo()
	{
		this.vehicleName.text = this.currentVehicle.vehicleName;
		this.vehicleName.color = this.vehicleNameColor[(int)this.currentVehicle.vehicleID];
		this.spcName.text = this.currentVehicle.spcName;
		this.spcName.color = this.spcNameColor[(int)this.currentVehicle.vehicleID];
		this.levelTexts[0].text = "Lvl." + (this.currentVehicle.vehicleInfoInstance.attackLevel + 1);
		this.levelTexts[1].text = "Lvl." + (this.currentVehicle.vehicleInfoInstance.hpLevel + 1);
		this.levelTexts[2].text = "Lvl." + (this.currentVehicle.vehicleInfoInstance.chargeLevel + 1);
		this.infoTexts[0].text = this.currentVehicle.GetBulletDamageMainGun();
		this.infoTexts[1].text = this.currentVehicle.GetConvertedHP();
		this.infoTexts[2].text = this.currentVehicle.GetBulletDamageSecondaryGun();
		this.upgradeCostAttack = this.currentVehicle.GetAttackUpgradeCost();
		this.upgradeCostHP = this.currentVehicle.GetHPUpgradeCost();
		this.upgradeCostCharge = this.currentVehicle.GetChargeUpgradeCost();
		this.costTexts[0].text = GameManager.CurrencyToString((float)this.upgradeCostAttack);
		this.costTexts[1].text = GameManager.CurrencyToString((float)this.upgradeCostHP);
		this.costTexts[2].text = GameManager.CurrencyToString((float)this.upgradeCostCharge);
	}

	public void StartUnlockSequence()
	{
		this.requirementHolder.SetActive(false);
		float endValue = (float)this.currentVehicle.vehicleID / (float)(this.vehicleManagerInstance.vehicleList.Length - 1);
		this.scrollRect.DOHorizontalNormalizedPos(endValue, 0f, false);
		this.scrollRect.enabled = false;
		SoundManager.PlaySFXInArray(this.successUnlockSFX, base.transform.position, 1f);
		this.currentVehicle.transform.DOScale(Vector3.one * 1.5f, 1f).SetEase(Ease.OutExpo);
		this.currentVehicle.transform.DOBlendableLocalRotateBy(new Vector3(0f, 720f, 0f), 1f, RotateMode.LocalAxisAdd);
		this.currentVehicle.transform.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutElastic).SetDelay(1f).OnStart(delegate
		{
			SoundManager.PlaySFXInArray(this.slamSFX, base.transform.position, 1f);
			this.slamParticle.gameObject.SetActive(true);
			base.GetComponent<RectTransform>().DOShakeAnchorPos(1f, new Vector3(30f, -30f, 0f), 20, 0f, false, true);
		}).OnComplete(delegate
		{
			this.upgradeCover.DOAnchorPosX(1000f, 0.25f, false);
			this.scrollRect.enabled = true;
		});
	}

	public void OnPressUnlockVehicle()
	{
		SoundManager.PlayTapSFX();
		if (GameManager.CurrentLevel < this.currentVehicle.requiredLevelToUnlock)
		{
			this.requirementText.transform.DOKill(false);
			this.requirementText.transform.DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(delegate
			{
				this.requirementText.transform.DOShakeRotation(1f, new Vector3(0f, 0f, 30f), 10, 10f, true);
				this.requirementText.transform.DOScale(Vector3.one, 0.2f).SetDelay(1f);
			});
			return;
		}
		if (GameManager.ReduceBoltByShouldOfferShop(this.currentVehicle.boltCostToUnlock, true))
		{
			this.StartUnlockSequence();
			this.currentVehicle.vehicleInfoInstance.UnlockVehicleAtLevel(this.currentVehicle.requiredLevelToUnlock - 1);
			this.vehicleManagerInstance.SetMainVehicle(this.currentVehicle.vehicleID);
			this.UpdateVehicleInfo();
			this.vehicleManagerInstance.UpdateCheapestUpgradeCost();
			EventManager.TriggerEvent("EventBoltChanges");
		}
	}

	public void OnPressPrestige()
	{
		SoundManager.PlayTapSFX();
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
		GameManager.ShowPrestigePage();
		base.gameObject.SetActive(false);
	}

	private void NudgeVehicle()
	{
		float endValue = (float)this.currentVehicle.vehicleID / (float)(this.vehicleManagerInstance.vehicleList.Length - 1);
		this.scrollRect.DOHorizontalNormalizedPos(endValue, 0f, false);
		SoundManager.PlaySFXInArray(this.upgradeSFX, base.transform.position, 1f);
		this.upgradeBeamParticle.Play();
		this.currentVehicle.transform.DOKill(false);
		this.currentVehicle.transform.DOScaleX(0.7f, 0.1f);
		this.currentVehicle.transform.DOScaleX(1.2f, 0.2f).SetDelay(0.1f);
		this.currentVehicle.transform.DOScaleX(0.9f, 0.2f).SetDelay(0.3f).OnComplete(delegate
		{
			this.currentVehicle.transform.DOScaleX(1f, 0.2f).SetEase(Ease.OutBack);
		});
		this.currentVehicle.transform.DOScaleY(1.3f, 0.1f);
		this.currentVehicle.transform.DOScaleY(0.8f, 0.2f).SetDelay(0.1f);
		this.currentVehicle.transform.DOScaleY(1.1f, 0.2f).SetDelay(0.3f).OnComplete(delegate
		{
			this.currentVehicle.transform.DOScaleY(1f, 0.2f).SetEase(Ease.OutBack);
		});
	}

	public void OnPressUpgradeAttack()
	{
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(this.upgradeCostAttack, true))
		{
			this.NudgeVehicle();
			this.currentVehicle.UpgradeAttack();
			this.UpdateVehicleInfo();
			this.vehicleManagerInstance.UpdateCheapestUpgradeCost();
			EventManager.TriggerEvent("EventCoinChanges");
		}
	}

	public void OnPressUpgradeCharge()
	{
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceBoltByShouldOfferShop(this.upgradeCostCharge, true))
		{
			this.NudgeVehicle();
			this.currentVehicle.UpgradeCharge();
			this.UpdateVehicleInfo();
			this.vehicleManagerInstance.UpdateCheapestUpgradeCost();
			EventManager.TriggerEvent("EventBoltChanges");
		}
	}

	public void OnPressUpgradeHP()
	{
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(this.upgradeCostHP, true))
		{
			this.NudgeVehicle();
			this.currentVehicle.UpgradeHP();
			this.UpdateVehicleInfo();
			this.vehicleManagerInstance.UpdateCheapestUpgradeCost();
			EventManager.TriggerEvent("EventCoinChanges");
		}
	}

	public void RestartGame()
	{
		if (!this.currentVehicle.vehicleInfoInstance.isUnlocked)
		{
			float endValue = (float)this.vehicleManagerInstance.GetMainVehicleID() / (float)(this.vehicleManagerInstance.vehicleList.Length - 1);
			this.scrollRect.DOHorizontalNormalizedPos(endValue, 0.25f, false).OnComplete(delegate
			{
				GameManager.RestartGame();
			});
		}
		else
		{
			GameManager.RestartGame();
		}
	}

	public void GenerateVehicles()
	{
		int num = this.vehicleManagerInstance.vehicleList.Length;
		for (int i = 0; i < num; i++)
		{
			VehiclePageSlotHolder vehiclePageSlotHolder = UnityEngine.Object.Instantiate<VehiclePageSlotHolder>(this.slotHolderPrefab);
			vehiclePageSlotHolder.transform.SetParent(this.pageContent.transform, false);
			Vehicle vehicle = this.vehicleManagerInstance.GetVehicle(this.vehicleManagerInstance.vehicleList[i].vehicleID);
			Rigidbody rigidbody = vehicle.gameObject.AddComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
			GameManager.SetLayerOnAllRecursive(vehicle.gameObject, 13);
			vehicle.transform.SetParent(vehiclePageSlotHolder.vehicleSlot.transform, false);
			vehicle.originalSlotVehiclePage = vehiclePageSlotHolder.vehicleSlot;
		}
		float endValue = (float)this.vehicleManagerInstance.GetMainVehicleID() / (float)(this.vehicleManagerInstance.vehicleList.Length - 1);
		this.scrollRect.DOHorizontalNormalizedPos(endValue, 0f, false);
	}

	private void OnEnable()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	private void OnDisable()
	{
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	private void OnPressBackButtonAndroid()
	{
		this.RestartGame();
	}
}
