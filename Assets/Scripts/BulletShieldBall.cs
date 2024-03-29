using System;
using UnityEngine;

public class BulletShieldBall : Bullet
{
	public override void AfterHitEnemy()
	{
		if (this.impactSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.impactArraySFX[UnityEngine.Random.Range(0, this.impactSFXLength)], base.transform.position, 0.5f);
		}
	}

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
	}
}
