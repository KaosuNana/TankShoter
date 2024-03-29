using System;

public class PowerUpHPHeal : PowerUp
{
	public float[] healAmount;

	public override void ActivatePowerUp()
	{
		float amount = this.healAmount[GameManager.instance.powerUpManagerInstance.GetLevelForPowerUp(PowerUp.PowerUpType.Heal)];
		GameManager.HealHP(amount);
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return (this.healAmount[level] * 100f).ToString() + "%";
	}
}
