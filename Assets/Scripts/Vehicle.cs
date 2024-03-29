using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	public enum ID
	{
		Tank01,
		Tank02,
		Tank03,
		Tank04,
		Tank05,
		Tank06,
		Tank07,
		Tank08,
		Tank09,
		Tank10
	}

	public string vehicleName;

	public string spcName;

	public HealthBarController healthBarPrefab;

	public Vector3 healthBarOffset;

	public Vehicle.ID vehicleID;

	public GameObject bottomWheels;

	public ParticleSystem explodeParticlePrefab;

	public int baseHP;

	public VehicleInfo vehicleInfoInstance;

	public int boltCostToUnlock;

	public int requiredLevelToUnlock;

	public GunController sidekickGunPrefab;

	public GunController[] gunPrefab;

	public float chargeFrequency;

	private GunController _gunInstance_k__BackingField;

	private GunController gunSecondaryInstance;

	private string savedFileName;

	private int currentArmorDefense;

	private int _currentHP_k__BackingField;

	private int maxHP;

	private HealthBarController _healthBarInstance_k__BackingField;

	private GameObject _originalSlotVehiclePage_k__BackingField;

	public Renderer myRenderer;

	public float gunYPos;

	private Tween blinkingTween;

	private float previousVehiclePosition;

	private float vehicleTravelDirection;

	public ParticleSystem rapidPickUpParticle;

	public GunController gunInstance
	{
		get;
		set;
	}

	public int currentHP
	{
		get;
		set;
	}

	public HealthBarController healthBarInstance
	{
		get;
		set;
	}

	public GameObject originalSlotVehiclePage
	{
		get;
		set;
	}

	private void Start()
	{
		this.UpdateStats();
	}

	private void LateUpdate()
	{
		this.vehicleTravelDirection = this.previousVehiclePosition - base.transform.position.x;
		this.previousVehiclePosition = base.transform.position.x;
		this.bottomWheels.transform.localRotation = Quaternion.Euler(new Vector3(Mathf.LerpAngle(this.bottomWheels.transform.localRotation.x, Mathf.Clamp(this.vehicleTravelDirection * 200f, -45f, 45f), 0.5f), 0f, 0f));
	}

	public void PlayRapidPickUpParticle()
	{
		this.rapidPickUpParticle.gameObject.SetActive(true);
		this.rapidPickUpParticle.Play();
	}

	public void DisableCollider()
	{
		base.GetComponent<Collider>().enabled = false;
	}

	public void UpdateShader(Shader inputShader)
	{
		this.myRenderer.material.shader = inputShader;
	}

	public void SetHealthBar(HealthBarController healthBarInput)
	{
		this.healthBarInstance = healthBarInput;
		this.healthBarInstance.ResetHealthBar();
	}

	private void UpdateHealthBar()
	{
		this.healthBarInstance = UnityEngine.Object.Instantiate<HealthBarController>(this.healthBarPrefab);
		this.healthBarInstance.transform.SetParent(GameManager.WorldCanvas.transform, true);
		this.healthBarInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - this.healthBarOffset.z);
		this.healthBarInstance.ResetHealthBar();
	}

	public void StopBlinking()
	{
		if (this.blinkingTween != null)
		{
			this.blinkingTween.Kill(false);
		}
		this.myRenderer.material.DOColor(Color.black, "_EmissionColor", 0.2f);
	}

	public void BlinkVehicle()
	{
		if (this.blinkingTween != null)
		{
			this.blinkingTween.Kill(false);
		}
		this.blinkingTween = DOTween.Sequence().SetLoops(20, LoopType.Restart).OnComplete(delegate
		{
			base.transform.parent.GetComponent<PlayerController>().TurnOffInvincible();
		}).Append(this.myRenderer.material.DOColor(Color.white, "_EmissionColor", 0.2f)).Append(this.myRenderer.material.DOColor(Color.black, "_EmissionColor", 0.2f));
		DOTween.To(() => this.blinkingTween.timeScale, delegate(float x)
		{
			this.blinkingTween.timeScale = x;
		}, 3f, 0.2f);
	}

	public void SendBackToOriginalSlot()
	{
		base.transform.SetParent(this.originalSlotVehiclePage.transform, false);
		base.transform.localPosition = Vector3.zero;
		base.transform.localScale = Vector3.one;
	}

	public void HealHP(float amount)
	{
		this.currentHP = Mathf.Min(this.currentHP + (int)(amount * (float)this.maxHP), this.maxHP);
		this.healthBarInstance.SetHealthBarTo((float)this.currentHP / (float)this.maxHP);
	}

	public void ApplyDamage(int amount)
	{
		this.currentHP = Mathf.Max(this.currentHP - amount, 0);
		this.healthBarInstance.SetHealthBarTo((float)this.currentHP / (float)this.maxHP);
	}

	public void DestroyVehicle()
	{
		base.gameObject.SetActive(false);
	}

	public void RessurectVehicle()
	{
		this.currentHP = this.maxHP;
		this.healthBarInstance.SetHealthBarTo((float)this.currentHP / (float)this.maxHP);
		this.healthBarInstance.gameObject.SetActive(true);
		base.transform.localScale = Vector3.zero;
		base.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		base.gameObject.SetActive(true);
	}

	public string GetConvertedHP()
	{
		return GameManager.CurrencyToString((float)this.currentHP / 100f);
	}

	public string GetBulletDamageMainGun()
	{
		return this.gunPrefab[0].GetBulletDamageWithAttackLevel(this.vehicleInfoInstance.attackLevel);
	}

	public string GetBulletDamageSecondaryGun()
	{
		return this.gunPrefab[1].GetBulletDamageWithAttackLevel(this.vehicleInfoInstance.chargeLevel);
	}

	public void UpdatePreferredGunForMainPlayer(bool forMainPlayer)
	{
		if (forMainPlayer)
		{
			this.gunInstance = UnityEngine.Object.Instantiate<GunController>(this.gunPrefab[0]);
			this.gunInstance.transform.SetParent(base.transform, false);
			this.gunInstance.transform.localPosition = new Vector3(0f, this.gunYPos, 0f);
			this.gunInstance.SetAttackLevel(this.vehicleInfoInstance.attackLevel);
			this.gunSecondaryInstance = UnityEngine.Object.Instantiate<GunController>(this.gunPrefab[1]);
			this.gunSecondaryInstance.transform.SetParent(base.transform, false);
			this.gunSecondaryInstance.transform.localPosition = Vector3.zero;
			this.gunSecondaryInstance.SetAttackLevel(this.vehicleInfoInstance.chargeLevel);
			this.gunSecondaryInstance.StopShooting();
		}
		else
		{
			this.gunInstance = UnityEngine.Object.Instantiate<GunController>(this.sidekickGunPrefab);
			this.gunInstance.transform.SetParent(base.transform, false);
			this.gunInstance.transform.localPosition = Vector3.zero;
			this.gunInstance.SetAttackLevel(this.vehicleInfoInstance.attackLevel);
		}
	}

	public void LevelUp()
	{
		this.vehicleInfoInstance.vehicleLevel++;
	}

	public void AddCards(int amount)
	{
		this.vehicleInfoInstance.SaveProgress();
	}

	public void UpgradeCharge()
	{
		this.vehicleInfoInstance.chargeLevel++;
		this.LevelUp();
		this.vehicleInfoInstance.SaveProgress();
	}

	public void UpgradeHP()
	{
		this.vehicleInfoInstance.hpLevel++;
		this.LevelUp();
		this.UpdateStats();
		this.vehicleInfoInstance.SaveProgress();
	}

	public void UpgradeAttack()
	{
		this.vehicleInfoInstance.attackLevel++;
		this.LevelUp();
		this.vehicleInfoInstance.SaveProgress();
	}

	public void SetVehicleToTeamPosition(int teamPosition)
	{
		this.vehicleInfoInstance.teamPosition = teamPosition;
		this.vehicleInfoInstance.SaveProgress();
	}

	public void UpdateStats()
	{
		this.maxHP = (int)((float)this.baseHP * (1f + 1f * (float)this.vehicleInfoInstance.hpLevel) * GameManager.StrengthMultiplier);
		this.currentHP = this.maxHP;
	}

	public int GetAttackUpgradeCost()
	{
		return this.vehicleInfoInstance.GetAttackUpgradeCost();
	}

	public int GetHPUpgradeCost()
	{
		return this.vehicleInfoInstance.GetHPUpgradeCost();
	}

	public int GetChargeUpgradeCost()
	{
		return this.vehicleInfoInstance.GetChargeUpgradeCost();
	}

	public void ActivateRapidMode(float duration)
	{
		this.gunInstance.ActivateRapidMode(duration);
		this.PlayRapidPickUpParticle();
	}

	public void StartShooting()
	{
		this.gunInstance.StartShooting();
	}

	public void StopShooting()
	{
		this.gunInstance.StopShooting();
	}

	public void StopReleasingSecondaryCharge()
	{
		this.gunSecondaryInstance.StopReleasingSecondaryCharge();
	}

	public void ReleaseSecondaryCharge(float amountCharged)
	{
		this.gunSecondaryInstance.ReleaseSecondaryCharge(amountCharged);
	}
}
