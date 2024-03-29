using I2.Loc;
using System;

public class PowerUpRapidFire : PowerUp
{
	public float[] durationAmount;

	private string localisedString;

	public override void ActivatePowerUp()
	{
		GameManager.ActivateRapidFire(this.durationAmount[GameManager.instance.powerUpManagerInstance.GetLevelForPowerUp(PowerUp.PowerUpType.RapidFire)]);
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Format(ScriptLocalization.Get("{0}s"), this.durationAmount[level]);
	}
}
