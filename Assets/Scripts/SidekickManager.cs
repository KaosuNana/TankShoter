using System;
using UnityEngine;

public class SidekickManager : MonoBehaviour
{
	public SidekickController[] sidekickList;

	public void StopShooting()
	{
		for (int i = 0; i < this.sidekickList.Length; i++)
		{
			if (this.sidekickList[i].gameObject.activeSelf)
			{
				this.sidekickList[i].StopShooting();
			}
		}
	}

	public void StartShooting()
	{
		for (int i = 0; i < this.sidekickList.Length; i++)
		{
			if (this.sidekickList[i].gameObject.activeSelf)
			{
				this.sidekickList[i].StartShooting();
			}
		}
	}

	public void ActivateSidekick(Vehicle.ID vehicleID, float duration)
	{
		if (this.sidekickList[(int)vehicleID].gameObject.activeSelf)
		{
			if (this.sidekickList[(int)vehicleID].vehicleInstance.vehicleID == vehicleID)
			{
				this.sidekickList[(int)vehicleID].ActivateSidekickPowerUp(vehicleID, duration);
				return;
			}
		}
		else if (!this.sidekickList[(int)vehicleID].gameObject.activeSelf)
		{
			this.sidekickList[(int)vehicleID].gameObject.SetActive(true);
			EventManager.TriggerEventPowerUp("CollectPowerUp", PowerUp.PowerUpType.TwinBee);
			this.sidekickList[(int)vehicleID].ActivateSidekickPowerUp(vehicleID, duration);
			return;
		}
	}
}
