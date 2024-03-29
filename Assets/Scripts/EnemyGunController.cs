using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal BulletEnemy bullet;

		internal void __m__0()
		{
			this.bullet.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullet.gameObject.SetActive(false);
		}
	}

	public BulletEnemy bulletPrefab;

	public float shootRate = 6f;

	public int bulletDamage;

	public bool autoTargetPlayer;

	public bool activateShooting;

	private float currentShootCountUp;

	protected float currentShootRate;

	protected BulletEnemy currentBulletPrefab;

	private List<BulletEnemy> bulletPool;

	public GameObject target;

	public float bulletTravelDuration = 4f;

	public float bulletTravelDistance = -60f;

	public float bulletTravelDurationAutoShoot = 2f;

	private void Awake()
	{
		this.currentShootRate = this.shootRate;
		this.currentShootCountUp = this.currentShootRate;
		this.currentBulletPrefab = this.bulletPrefab;
		this.bulletPool = new List<BulletEnemy>();
	}

	private void Update()
	{
		this.currentShootCountUp += Time.deltaTime;
		if (this.activateShooting)
		{
			if (this.currentShootCountUp >= this.currentShootRate)
			{
				this.currentShootCountUp = 0f;
				this.Shoot();
			}
			if (this.autoTargetPlayer && this.target != null)
			{
				base.transform.LookAt(this.target.transform);
			}
		}
	}

	public virtual void StartShooting()
	{
		this.activateShooting = true;
	}

	public virtual void StopShooting()
	{
		this.activateShooting = false;
	}

	public void LockInTarget(GameObject inputTarget)
	{
		this.target = inputTarget;
	}

	public void RemoveTarget()
	{
		this.target = null;
	}

	protected virtual void Shoot()
	{
		BulletEnemy bullet = this.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z);
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.SetDamageAmount(this.bulletDamage);
		if (this.target == null)
		{
			bullet.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
		else
		{
			Vector3 vector = (this.target.transform.position - base.transform.position).normalized * 80f;
			bullet.transform.LookAt(vector);
			bullet.transform.DOBlendableMoveBy(vector, this.bulletTravelDurationAutoShoot, false).SetEase(Ease.Linear).OnComplete(delegate
			{
				bullet.gameObject.SetActive(false);
			});
		}
	}

	protected BulletEnemy GetPooledBullets()
	{
		for (int i = 0; i < this.bulletPool.Count; i++)
		{
			if (this.bulletPool[i].gameObject.activeSelf)
			{
				return this.bulletPool[i];
			}
		}
		return UnityEngine.Object.Instantiate<BulletEnemy>(this.currentBulletPrefab);
	}
}
