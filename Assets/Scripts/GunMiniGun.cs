using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunMiniGun : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullet;

		internal void __m__0()
		{
			this.bullet.gameObject.SetActive(false);
		}
	}

	public float minX = -1f;

	public float maxX = 1f;

	public int chargeMultiplication = 30;

	public override void ReleaseSecondaryCharge(float amountCharged)
	{
		int num = (int)Mathf.Floor(amountCharged * (float)this.secondaryMaxNumberOfBullets);
		this.currentBulletPrefab = this.bulletPrefabs[0];
		num *= this.chargeMultiplication;
		for (int i = 0; i < num; i++)
		{
			base.Invoke("Shoot", this.currentShootRate * (float)i);
		}
	}

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		Bullet bullet = base.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x + UnityEngine.Random.Range(this.minX, this.maxX), base.transform.position.y + UnityEngine.Random.Range(1.2f, 1.3f), base.transform.position.z + UnityEngine.Random.Range(1.2f, 1.8f));
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.SetDamageAmount(this.currentBulletDamage);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullet.gameObject.SetActive(false);
		});
	}
}
