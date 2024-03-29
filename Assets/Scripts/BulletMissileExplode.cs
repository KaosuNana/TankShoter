using System;
using UnityEngine;

public class BulletMissileExplode : Bullet
{
	public int explosionDamage = 100;

	private void Awake()
	{
		base.damage = this.explosionDamage;
	}

	public override void SetDamageAmount(int amount)
	{
	}

	public override void AfterHitEnemy()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
