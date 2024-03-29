using System;
using UnityEngine;

public class VehicleInfo : MonoBehaviour
{
	public float fileVersion;

	public int vehicleLevel;

	public int remainingLife;

	public int attackLevel;

	public int chargeLevel;

	public int hpLevel;

	public int teamPosition;

	public bool isUnlocked;

	public Vehicle.ID vehicleID;

	private string savedFileName;

	public void LoadProgress()
	{
		this.savedFileName = this.vehicleID.ToString();
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Load<VehicleInfo>(this.savedFileName, this);
		}
		else
		{
			this.fileVersion = 1f;
			this.remainingLife = 3;
			this.attackLevel = 0;
			this.chargeLevel = 0;
			this.hpLevel = 0;
			this.vehicleLevel = 1;
			this.teamPosition = -1;
			this.isUnlocked = false;
			if (this.vehicleID == Vehicle.ID.Tank01)
			{
				this.isUnlocked = true;
			}
			ES2.Save<VehicleInfo>(this, this.savedFileName);
		}
	}

	public void UnlockVehicleAtLevel(int inputLevel)
	{
		this.attackLevel = inputLevel;
		this.hpLevel = inputLevel;
		this.chargeLevel = inputLevel;
		this.isUnlocked = true;
		this.SaveProgress();
	}

	public void SaveProgress()
	{
		ES2.Save<VehicleInfo>(this, this.savedFileName);
	}

	public void ResetProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Delete(this.savedFileName);
		}
	}

	public int GetAttackUpgradeCost()
	{
		return (int)Mathf.Floor(200f * Mathf.Pow(1.15f, (float)this.attackLevel));
	}

	public int GetHPUpgradeCost()
	{
		return (int)Mathf.Floor(600f * Mathf.Pow(1.2f, (float)this.hpLevel));
	}

	public int GetChargeUpgradeCost()
	{
		return (int)Mathf.Floor(5f * Mathf.Pow(1.11f, (float)this.chargeLevel));
	}
}
