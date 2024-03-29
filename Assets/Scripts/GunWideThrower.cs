using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunWideThrower : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullets05;

		internal void __m__0()
		{
			this.bullets05.gameObject.SetActive(false);
		}
	}

	private sealed class _Shoot_c__AnonStorey1
	{
		internal Bullet bullets02;

		internal Bullet bullets01;

		internal void __m__0()
		{
			this.bullets02.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullets01.gameObject.SetActive(false);
		}
	}

	private sealed class _Shoot_c__AnonStorey2
	{
		internal Bullet bullets04;

		internal Bullet bullets03;

		internal void __m__0()
		{
			this.bullets04.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullets03.gameObject.SetActive(false);
		}
	}

	private Bullet invisibleBullet;

	private List<Bullet> invisibleBulletPool;

	public Bullet invisibleBulletPrefab;

	private float amountCharged;

	public AudioClip[] gunThrowerSFX;

	private float invisibleBulletSizeX = 8f;

	private void Start()
	{
		this.invisibleBulletPool = new List<Bullet>();
	}

	public override void ReleaseSecondaryCharge(float input)
	{
		if (input > 0.02f)
		{
			this.amountCharged = input;
			this.currentBulletPrefab = this.bulletPrefabs[0];
			this.Shoot();
		}
	}

	protected Bullet GetInvisiblePooledBullets()
	{
		for (int i = 0; i < this.invisibleBulletPool.Count; i++)
		{
			if (!this.invisibleBulletPool[i].gameObject.activeSelf)
			{
				return this.invisibleBulletPool[i];
			}
		}
		Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.invisibleBulletPrefab);
		this.invisibleBulletPool.Add(bullet);
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		return bullet;
	}

	protected override void Shoot()
	{
		if (this.gunThrowerSFX.Length > 0)
		{
			SoundManager.PlaySFXInArray(this.gunThrowerSFX[UnityEngine.Random.Range(0, this.gunThrowerSFX.Length)], base.transform.position, 0.5f);
		}
		this.invisibleBullet = this.GetInvisiblePooledBullets();
		this.invisibleBullet.gameObject.SetActive(true);
		this.invisibleBullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1.2f, base.transform.position.z + 1f);
		this.invisibleBullet.SetDamageAmount((int)((float)this.currentBulletDamage * (1f + this.amountCharged)));
		this.invisibleBullet.shootRate = this.currentShootRate;
		Vector3 localScale = new Vector3(8f, 5f, 5f);
		if (!this.autoTarget)
		{
			if (this.amountCharged < 0.33f)
			{
				localScale.x = 2f;
			}
			else if (this.amountCharged < 0.66f)
			{
				localScale.x = 5f;
			}
		}
		this.invisibleBullet.transform.localScale = localScale;
		this.invisibleBullet.GetComponent<BulletInvisibleWideThrower>().ShootForward(30f, this.bulletTravelDuration);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		Bullet bullets05 = base.GetPooledBullets();
		bullets05.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets05.gameObject.SetActive(true);
		Vector3 positionWithAngle = this.GetPositionWithAngle(bullets05.transform.position, 30f, 0);
		positionWithAngle.y = bullets05.transform.position.y;
		bullets05.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		bullets05.SetDamageAmount(this.currentBulletDamage);
		bullets05.transform.DOMove(positionWithAngle, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets05.gameObject.SetActive(false);
		});
		if (this.amountCharged >= 0.33f || this.autoTarget)
		{
			Bullet bullets02 = base.GetPooledBullets();
			bullets02.transform.position = new Vector3(base.transform.position.x + 0.1f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets02.gameObject.SetActive(true);
			bullets02.SetDamageAmount(this.currentBulletDamage);
			bullets02.transform.rotation = Quaternion.Euler(0f, 5f, 0f);
			Vector3 positionWithAngle2 = this.GetPositionWithAngle(bullets02.transform.position, 30f, 5);
			positionWithAngle2.y = bullets02.transform.position.y;
			bullets02.transform.DOMove(positionWithAngle2, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets02.gameObject.SetActive(false);
			});
			Bullet bullets01 = base.GetPooledBullets();
			bullets01.transform.position = new Vector3(base.transform.position.x - 0.1f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets01.gameObject.SetActive(true);
			bullets01.SetDamageAmount(this.currentBulletDamage);
			bullets01.transform.rotation = Quaternion.Euler(0f, 355f, 0f);
			Vector3 positionWithAngle3 = this.GetPositionWithAngle(bullets01.transform.position, 30f, 355);
			positionWithAngle3.y = bullets01.transform.position.y;
			bullets01.transform.DOMove(positionWithAngle3, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets01.gameObject.SetActive(false);
			});
		}
		if (this.amountCharged >= 0.66f || this.autoTarget)
		{
			Bullet bullets04 = base.GetPooledBullets();
			bullets04.transform.position = new Vector3(base.transform.position.x + 0.2f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets04.gameObject.SetActive(true);
			Vector3 positionWithAngle4 = this.GetPositionWithAngle(bullets04.transform.position, 30f, 10);
			positionWithAngle4.y = bullets04.transform.position.y;
			bullets04.transform.rotation = Quaternion.Euler(0f, 10f, 0f);
			bullets04.SetDamageAmount(this.currentBulletDamage);
			bullets04.transform.DOMove(positionWithAngle4, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets04.gameObject.SetActive(false);
			});
			Bullet bullets03 = base.GetPooledBullets();
			bullets03.transform.position = new Vector3(base.transform.position.x - 0.2f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets03.gameObject.SetActive(true);
			bullets03.SetDamageAmount(this.currentBulletDamage);
			bullets03.transform.rotation = Quaternion.Euler(0f, 350f, 0f);
			Vector3 positionWithAngle5 = this.GetPositionWithAngle(bullets03.transform.position, 30f, 350);
			positionWithAngle5.y = bullets03.transform.position.y;
			bullets03.transform.DOMove(positionWithAngle5, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets03.gameObject.SetActive(false);
			});
		}
	}

	private Vector3 GetPositionWithAngle(Vector3 center, float radius, int ang)
	{
		Vector3 result;
		result.x = center.x + radius * Mathf.Sin((float)ang * 0.0174532924f);
		result.y = center.y;
		result.z = center.z + radius * Mathf.Cos((float)ang * 0.0174532924f);
		return result;
	}
}
