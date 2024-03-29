using System;

public class BulletChildren : Bullet
{
	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
	}
}
