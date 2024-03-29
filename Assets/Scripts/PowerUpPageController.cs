using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerUpPageController : MonoBehaviour
{
	private PowerUpManager powerUpManagerInstance;

	public Text[] levelText;

	public Text[] infoText;

	public Text[] costText;

	public Button[] buttonList;

	private void Awake()
	{
		this.powerUpManagerInstance = GameManager.instance.powerUpManagerInstance;
	}

	private void Start()
	{
		this.UpdateInfo();
	}

	private void UpdateInfo()
	{
		for (int i = 0; i < this.levelText.Length; i++)
		{
			int level = this.powerUpManagerInstance.powerUpInfoList[i].level;
			if (level >= 9)
			{
				this.levelText[i].text = ScriptLocalization.Get("MAX LEVEL");
				this.buttonList[i].gameObject.SetActive(false);
			}
			else
			{
				this.levelText[i].text = ScriptLocalization.Get("LEVEL") + " " + (level + 1).ToString();
				this.costText[i].text = GameManager.CurrencyToString((float)this.powerUpManagerInstance.powerUpInfoList[i].GetUpgradeCost());
			}
			this.infoText[i].text = this.powerUpManagerInstance.GetInfoForPowerUp(this.powerUpManagerInstance.powerUpInfoList[i].powerUpType, level);
		}
	}

	public void OnPressUpgradeRapidFireButton()
	{
		int currentUpgradeCost = this.powerUpManagerInstance.GetCurrentUpgradeCost(PowerUp.PowerUpType.RapidFire);
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(currentUpgradeCost, true))
		{
			this.powerUpManagerInstance.UpgradePowerUp(PowerUp.PowerUpType.RapidFire);
			this.UpdateInfo();
			EventManager.TriggerEvent("EventCoinChanges");
		}
	}

	public void OnPressUpgradeHealButton()
	{
		int currentUpgradeCost = this.powerUpManagerInstance.GetCurrentUpgradeCost(PowerUp.PowerUpType.Heal);
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(currentUpgradeCost, true))
		{
			this.powerUpManagerInstance.UpgradePowerUp(PowerUp.PowerUpType.Heal);
			this.UpdateInfo();
			EventManager.TriggerEvent("EventCoinChanges");
		}
	}

	public void OnPressUpgradeMagnetButton()
	{
		int currentUpgradeCost = this.powerUpManagerInstance.GetCurrentUpgradeCost(PowerUp.PowerUpType.Magnet);
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(currentUpgradeCost, true))
		{
			this.powerUpManagerInstance.UpgradePowerUp(PowerUp.PowerUpType.Magnet);
			this.UpdateInfo();
			EventManager.TriggerEvent("EventCoinChanges");
		}
	}

	public void OnPressUpgradeShieldButton()
	{
		int currentUpgradeCost = this.powerUpManagerInstance.GetCurrentUpgradeCost(PowerUp.PowerUpType.Shield);
		SoundManager.PlayTapSFX();
		if (GameManager.ReduceCoinByShouldOfferShop(currentUpgradeCost, true))
		{
			this.powerUpManagerInstance.UpgradePowerUp(PowerUp.PowerUpType.Shield);
			this.UpdateInfo();
			EventManager.TriggerEvent("EventCoinChanges");
		}
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
		GameManager.RestartGame();
	}
}
