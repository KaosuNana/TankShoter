using System;
using UnityEngine;

public class BulletMissileStraight : Bullet
{
	public BulletMissileExplode explodeRadarPrefab;

	public GameObject rocket;

	public ParticleSystem rocketParticleFX;

	private Collider myCollider;

	private void OnEnable()
	{
		this.myCollider = base.GetComponent<Collider>();
		this.myCollider.enabled = true;
	}

	public override void SetDamageAmount(int amount)
	{
		base.CancelInvoke("DisableAll");
		base.damage = amount;
		base.UpdateDamageValue();
		this.rocket.SetActive(true);
		this.rocketParticleFX.Play();
		this.myCollider.enabled = true;
	}

	public override void AfterHitEnemy()
	{
		GameManager.ExtraCharge(this.chargeValue);
		this.rocketParticleFX.Stop();
		this.explodeRadarPrefab.explosionDamage = base.damage;
		UnityEngine.Object.Instantiate<BulletMissileExplode>(this.explodeRadarPrefab, base.transform.position, Quaternion.identity);
		this.myCollider.enabled = false;
		this.rocket.SetActive(false);
		base.Invoke("DisableAll", 2f);
	}

	private void DisableAll()
	{
		base.gameObject.SetActive(false);
	}
}
