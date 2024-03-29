using I2.Loc;
using System;

public class PowerUpMagnet : PowerUp
{
	public float[] durationAmount;

	public override void ActivatePowerUp()
	{
		GameManager.ActivateMagnetMode(this.durationAmount[GameManager.instance.powerUpManagerInstance.GetLevelForPowerUp(PowerUp.PowerUpType.Magnet)]);
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Format(ScriptLocalization.Get("{0}s"), this.durationAmount[level]);
	}
}
