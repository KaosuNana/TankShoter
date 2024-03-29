// using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VehicleManager : MonoBehaviour
{
	public VehicleInfo vehicleInfoPrefab;

	public Vehicle[] vehicleList;

	private List<VehicleInfo> vehicleInfoList;

	public GameObject[] notificationBadge;

	private int cheapestUpgradeCoinCost;

	private int cheapestUpgradeBoltCost;

	private void Awake()
	{
		this.LoadVehicleProgressOnStart();
		this.UpdateCheapestUpgradeCost();
		this.UpdateNotificationBadge();
		EventManager.StartListening("EventCoinChanges", new UnityAction(this.UpdateNotificationBadge));
		EventManager.StartListening("EventBoltChanges", new UnityAction(this.UpdateNotificationBadge));
	}

	public void UpdateNotificationBadge()
	{
		this.notificationBadge[0].SetActive(this.cheapestUpgradeCoinCost <= GameManager.Coin || this.cheapestUpgradeBoltCost <= GameManager.Bolt);
		this.notificationBadge[1].SetActive(this.cheapestUpgradeCoinCost <= GameManager.Coin || this.cheapestUpgradeBoltCost <= GameManager.Bolt);
	}

	public void UpdateCheapestUpgradeCost()
	{
		this.cheapestUpgradeCoinCost = 0;
		for (int i = 0; i < this.vehicleInfoList.Count; i++)
		{
			if (this.vehicleInfoList[i].isUnlocked)
			{
				if (this.cheapestUpgradeCoinCost < this.vehicleInfoList[i].GetAttackUpgradeCost() || this.cheapestUpgradeCoinCost == 0)
				{
					this.cheapestUpgradeCoinCost = this.vehicleInfoList[i].GetAttackUpgradeCost();
				}
				if (this.cheapestUpgradeCoinCost < this.vehicleInfoList[i].GetHPUpgradeCost())
				{
					this.cheapestUpgradeCoinCost = this.vehicleInfoList[i].GetHPUpgradeCost();
				}
				if (this.cheapestUpgradeBoltCost < this.vehicleInfoList[i].GetChargeUpgradeCost() || this.cheapestUpgradeBoltCost == 0)
				{
					this.cheapestUpgradeBoltCost = this.vehicleInfoList[i].GetChargeUpgradeCost();
				}
			}
		}
	}

	public void PlayerPrestige()
	{
		for (int i = 0; i < this.vehicleInfoList.Count; i++)
		{
			this.vehicleInfoList[i].ResetProgress();
		}
		this.SetMainVehicle(Vehicle.ID.Tank01);
	}

	public int GetPreferredVehicleIndex()
	{
		// return ObscuredPrefs.GetInt("MainVehiclePrefs", 0);
		return PlayerPrefs.GetInt("MainVehiclePrefs", 0);
	}

	public Vehicle GetMainVehicle()
	{
		// return this.GetVehicle((Vehicle.ID)ObscuredPrefs.GetInt("MainVehiclePrefs", 0));
		return this.GetVehicle((Vehicle.ID)PlayerPrefs.GetInt("MainVehiclePrefs", 0));
	}

	public Vehicle.ID GetMainVehicleID()
	{
		// return (Vehicle.ID)ObscuredPrefs.GetInt("MainVehiclePrefs", 0);
		return (Vehicle.ID)PlayerPrefs.GetInt("MainVehiclePrefs", 0);
	}

	public void SetMainVehicle(Vehicle.ID vehicleID)
	{
		// ObscuredPrefs.SetInt("MainVehiclePrefs", (int)vehicleID);
		PlayerPrefs.SetInt("MainVehiclePrefs", (int)vehicleID);
	}

	public Vehicle GetVehicle(Vehicle.ID vehicleID)
	{
		for (int i = 0; i < this.vehicleList.Length; i++)
		{
			if (this.vehicleList[i].vehicleID == vehicleID)
			{
				Vehicle vehicle = UnityEngine.Object.Instantiate<Vehicle>(this.vehicleList[i]);
				vehicle.vehicleInfoInstance = this.vehicleInfoList[i];
				return vehicle;
			}
		}
		return null;
	}

	public int GetNumberOfVehicleUpgrades()
	{
		int num = 0;
		for (int i = 0; i < this.vehicleInfoList.Count; i++)
		{
			num = num + this.vehicleInfoList[i].attackLevel + this.vehicleInfoList[i].hpLevel + this.vehicleInfoList[i].chargeLevel;
		}
		return num;
	}

	private void LoadVehicleProgressOnStart()
	{
		this.vehicleInfoList = new List<VehicleInfo>();
		for (int i = 0; i < this.vehicleList.Length; i++)
		{
			VehicleInfo vehicleInfo = UnityEngine.Object.Instantiate<VehicleInfo>(this.vehicleInfoPrefab);
			vehicleInfo.vehicleID = this.vehicleList[i].vehicleID;
			vehicleInfo.LoadProgress();
			vehicleInfo.transform.SetParent(base.transform, false);
			this.vehicleInfoList.Add(vehicleInfo);
		}
	}
}
