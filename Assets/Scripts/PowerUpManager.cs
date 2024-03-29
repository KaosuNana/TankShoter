using System;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpManager : MonoBehaviour
{
	public PowerUpInfo[] powerUpInfoList;

	public GameObject[] notificationBadge;

	public PowerUp[] powerUpPrefabs;

	private int cheapestUpgradeCost;

	private bool allUpgradeMaxed;

	private void Start()
	{
		this.UpdateCheapestUpgradeCost();
		this.UpdateNotificationBadge();
		EventManager.StartListening("EventCoinChanges", new UnityAction(this.UpdateNotificationBadge));
	}

	public void UpdateNotificationBadge()
	{
		if (!this.allUpgradeMaxed)
		{
			this.notificationBadge[0].SetActive(this.cheapestUpgradeCost <= GameManager.Coin);
			this.notificationBadge[1].SetActive(this.cheapestUpgradeCost <= GameManager.Coin);
		}
	}

	private void UpdateCheapestUpgradeCost()
	{
		this.cheapestUpgradeCost = 0;
		this.allUpgradeMaxed = true;
		for (int i = 0; i < this.powerUpInfoList.Length; i++)
		{
			if (this.powerUpInfoList[i].level < 9)
			{
				if (this.cheapestUpgradeCost == 0)
				{
					this.cheapestUpgradeCost = this.powerUpInfoList[i].GetUpgradeCost();
				}
				else if (this.powerUpInfoList[i].GetUpgradeCost() < this.cheapestUpgradeCost)
				{
					this.cheapestUpgradeCost = this.powerUpInfoList[i].GetUpgradeCost();
				}
				this.allUpgradeMaxed = false;
			}
		}
	}

	public string GetInfoForPowerUp(PowerUp.PowerUpType powerUpInput, int level)
	{
		string result = string.Empty;
		for (int i = 0; i < this.powerUpPrefabs.Length; i++)
		{
			if (powerUpInput == this.powerUpPrefabs[i].powerUpType)
			{
				result = this.powerUpPrefabs[i].GetPowerUpInfoForLevel(level);
			}
		}
		return result;
	}

	public void UpgradePowerUp(PowerUp.PowerUpType powerUpType)
	{
		for (int i = 0; i < this.powerUpInfoList.Length; i++)
		{
			if (this.powerUpInfoList[i].powerUpType == powerUpType)
			{
				this.powerUpInfoList[i].level++;
				this.powerUpInfoList[i].SaveProgress();
				this.UpdateCheapestUpgradeCost();
			}
		}
	}

	public void PlayerPrestige()
	{
		for (int i = 0; i < this.powerUpInfoList.Length; i++)
		{
			this.powerUpInfoList[i].ResetProgress();
		}
	}

	public int GetCurrentUpgradeCost(PowerUp.PowerUpType powerUpType)
	{
		for (int i = 0; i < this.powerUpInfoList.Length; i++)
		{
			if (this.powerUpInfoList[i].powerUpType == powerUpType)
			{
				return this.powerUpInfoList[i].GetUpgradeCost();
			}
		}
		return 0;
	}

	public int GetLevelForPowerUp(PowerUp.PowerUpType powerUpType)
	{
		for (int i = 0; i < this.powerUpInfoList.Length; i++)
		{
			if (this.powerUpInfoList[i].powerUpType == powerUpType)
			{
				return this.powerUpInfoList[i].level;
			}
		}
		return 0;
	}
}
