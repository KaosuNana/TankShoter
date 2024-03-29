using I2.Loc;
using System;

public class PowerUpShield : PowerUp
{
	public float[] durationAmount;

	public override void ActivatePowerUp()
	{
		float duration = this.durationAmount[GameManager.instance.powerUpManagerInstance.GetLevelForPowerUp(PowerUp.PowerUpType.Shield)];
		GameManager.ActivateShieldMode(duration);
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Format(ScriptLocalization.Get("{0}s"), this.durationAmount[level]);
	}
}
