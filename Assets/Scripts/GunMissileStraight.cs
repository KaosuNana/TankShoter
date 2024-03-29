using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunMissileStraight : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullet;

		internal void __m__0()
		{
			this.bullet.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullet.gameObject.SetActive(false);
		}
	}

	public ParticleSystem smokeParticle;

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		Bullet bullet = base.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 1f);
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.SetDamageAmount(this.currentBulletDamage);
		this.smokeParticle.Play();
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		if (this.target == null)
		{
			bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
		else
		{
			bullet.transform.rotation = Quaternion.LookRotation(this.target.transform.position - base.transform.position);
			bullet.transform.DOMove(this.target.transform.position, this.bulletTravelDurationAutoShoot, false).SetEase(Ease.Linear).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
	}
}
