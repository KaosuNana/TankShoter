using DG.Tweening;
using System;
using UnityEngine;

public class GunThrower : GunController
{
	private Bullet bullet;

	public override void StartShooting()
	{
		this.Shoot();
	}

	public override void StopShooting()
	{
		this.bullet.GetComponent<BulletThrower>().StopThrowing();
	}

	public override void SetAttackLevel(int attackLevel)
	{
		base.currentAttackLevel = attackLevel;
		this.currentBulletDamage = (int)((float)this.baseBulletDamage * (1f + 0.1f * (float)attackLevel) * GameManager.StrengthMultiplier);
	}

	public override void ReleaseSecondaryCharge(float amountCharged)
	{
		int num = (int)Mathf.Floor(amountCharged * (float)this.secondaryMaxNumberOfBullets);
		this.currentBulletPrefab = this.bulletPrefabs[0];
		this.Shoot();
	}

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		if (this.bullet == null)
		{
			this.bullet = base.GetPooledBullets();
		}
		this.bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1.2f, base.transform.position.z);
		this.bullet.gameObject.SetActive(true);
		this.bullet.transform.DOKill(false);
		this.bullet.transform.SetParent(base.transform, false);
		this.bullet.SetDamageAmount(this.currentBulletDamage);
		this.bullet.shootRate = this.shootRate[0];
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		this.bullet.GetComponent<BulletThrower>().StartThrowing();
	}
}
