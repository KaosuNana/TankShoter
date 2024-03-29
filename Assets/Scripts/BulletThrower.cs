using System;
using UnityEngine;

public class BulletThrower : Bullet
{
	public ParticleSystem throwerParticle;

	public void StopThrowing()
	{
		this.throwerParticle.Stop();
		base.GetComponent<Collider>().enabled = false;
	}

	public void StartThrowing()
	{
		this.throwerParticle.Play();
		base.GetComponent<Collider>().enabled = true;
	}

	public override void AfterHitEnemy()
	{
		GameManager.ExtraCharge(this.chargeValue);
	}

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
	}
}
