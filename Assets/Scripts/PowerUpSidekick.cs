using System;
using UnityEngine;

public class PowerUpSidekick : PowerUp
{
	public Vehicle.ID vehicleID;

	public float durationAmount = 10f;

	public void EnableCollider()
	{
		base.GetComponent<Collider>().enabled = true;
	}

	public void DisableCollider()
	{
		base.GetComponent<Collider>().enabled = false;
	}

	public override void ActivatePowerUp()
	{
		GameManager.ActivateSidekick(this.vehicleID, this.durationAmount);
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Empty;
	}
}
