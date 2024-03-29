using System;
using UnityEngine;

public class BulletEnemyExplode : Bullet
{
	public int explosionDamage = 100;

	private void Awake()
	{
		base.damage = this.explosionDamage + (int)((float)this.explosionDamage * 0.75f * Mathf.Floor((float)(GameManager.CurrentLevel / 2)));
	}

	public override void SetDamageAmount(int amount)
	{
	}

	public override void AfterHitEnemy()
	{
		EventManager.TriggerEvent("EnemyBomb");
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
