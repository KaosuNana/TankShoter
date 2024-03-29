using DG.Tweening;
using System;
using UnityEngine;

public class GunHoming : GunController
{
	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
			if (this.target == null)
			{
				return;
			}
		}
		Bullet pooledBullets = base.GetPooledBullets();
		pooledBullets.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + 1f);
		pooledBullets.gameObject.SetActive(true);
		pooledBullets.transform.DOKill(false);
		pooledBullets.SetDamageAmount(this.currentBulletDamage);
	}
}
