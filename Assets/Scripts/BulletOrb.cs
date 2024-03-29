using System;

public class BulletOrb : Bullet
{
	public override void AfterHitEnemy()
	{
	}

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
	}
}
