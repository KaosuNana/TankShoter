using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunLaser : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullet;

		internal void __m__0()
		{
			this.bullet.gameObject.SetActive(false);
		}
	}

	private float extraDamage;

	private float extraSize;

	public float sizeMultiplier = 4f;

	private Vector3 originalScale = new Vector3(2f, 2f, 20f);

	public override void ReleaseSecondaryCharge(float amountCharged)
	{
		this.extraDamage = amountCharged;
		this.extraSize = amountCharged * this.sizeMultiplier;
		this.currentBulletPrefab = this.bulletPrefabs[0];
		this.Shoot();
	}

	protected override void Shoot()
	{
		if (this.autoTarget)
		{
			if (this.target == null || !this.target.activeInHierarchy)
			{
				this.target = EnemyManager.GetEnemy();
			}
			if (this.target == null)
			{
				return;
			}
		}
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		Bullet bullet = base.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.transform.localScale = new Vector3(this.originalScale.x * (1f + this.extraSize), this.originalScale.y, this.originalScale.z);
		bullet.SetDamageAmount((int)((float)this.currentBulletDamage * (1f + this.extraDamage)));
		bullet.shootRate = this.currentShootRate;
		bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullet.gameObject.SetActive(false);
		});
	}
}
