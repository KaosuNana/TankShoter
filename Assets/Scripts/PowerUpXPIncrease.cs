using System;
using UnityEngine;

public class PowerUpXPIncrease : PowerUp
{
	public int minXPAmount;

	public int maxXPAmount;

	public override void ActivatePowerUp()
	{
		int extraScore = UnityEngine.Random.Range(this.minXPAmount, this.maxXPAmount + 1);
		GameManager.ExtraScore(extraScore, base.transform.position, "Extra XP");
		GameManager.Player.PlayXPPickUp();
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Empty;
	}
}
