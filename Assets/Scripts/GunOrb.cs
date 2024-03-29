using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunOrb : GunController
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

	private int orbSize;

	public override void ReleaseSecondaryCharge(float amountCharged)
	{
		int num = (int)Mathf.Floor(amountCharged * (float)this.secondaryMaxNumberOfBullets);
		this.currentBulletPrefab = this.bulletPrefabs[0];
		this.orbSize = num;
		this.Shoot();
	}

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		Bullet bullet = base.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullet.transform.localScale = Vector3.zero;
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.transform.DOScale(Vector3.one * (float)this.orbSize, 0.1f);
		bullet.SetDamageAmount(this.currentBulletDamage);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		if (this.target == null)
		{
			bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
		else
		{
			bullet.transform.LookAt(this.target.transform);
			Vector3 vector = (this.target.transform.position - base.transform.position).normalized;
			vector *= 50f;
			bullet.transform.DOBlendableMoveBy(vector, this.bulletTravelDurationAutoShoot, false).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
	}
}
