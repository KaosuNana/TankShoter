using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunController : MonoBehaviour
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullet;

		internal void __m__0()
		{
			this.bullet.gameObject.SetActive(false);
		}
	}

	public Bullet[] bulletPrefabs;

	public AudioClip mainGunShootSFX;

	public float[] shootRate;

	public int baseBulletDamage;

	protected int currentBulletDamage;

	public bool autoTarget;

	public bool activateShooting;

	private float currentShootCountUp;

	private bool powerUpMode;

	protected float currentShootRate;

	protected Bullet currentBulletPrefab;

	public int secondaryMaxNumberOfBullets = 5;

	private int _currentAttackLevel_k__BackingField;

	private int gunLevel;

	private List<Bullet> bulletPool;

	public GameObject target;

	public float bulletTravelDuration = 1.5f;

	public float bulletTravelDistance = 45f;

	public float bulletTravelDurationAutoShoot = 0.2f;

	public int currentAttackLevel
	{
		get;
		set;
	}

	private void Awake()
	{
		this.currentShootRate = this.shootRate[0];
		this.currentShootCountUp = this.currentShootRate;
		this.currentBulletPrefab = this.bulletPrefabs[0];
		this.bulletPool = new List<Bullet>();
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
			if (this.autoTarget && this.target != null)
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

	public virtual void SetAttackLevel(int attackLevel)
	{
		this.currentAttackLevel = attackLevel;
		this.currentBulletDamage = (int)((float)this.baseBulletDamage * (1f + 0.1f * (float)attackLevel) * GameManager.StrengthMultiplier);
	}

	public void StopReleasingSecondaryCharge()
	{
		base.CancelInvoke("Shoot");
	}

	public virtual void ReleaseSecondaryCharge(float amountCharged)
	{
		int num = (int)Mathf.Ceil(amountCharged * (float)this.secondaryMaxNumberOfBullets);
		this.currentBulletPrefab = this.bulletPrefabs[0];
		for (int i = 0; i < num; i++)
		{
			base.Invoke("Shoot", this.currentShootRate * (float)i);
		}
	}

	public void ActivateRapidMode(float duration)
	{
		base.CancelInvoke();
		this.gunLevel = Mathf.Min(this.gunLevel + 1, 2);
		this.currentShootRate = this.shootRate[this.gunLevel];
		if (duration > 0f)
		{
			base.Invoke("DeactivateRapidMode", duration);
		}
	}

	private void DeactivateRapidMode()
	{
		this.gunLevel = 0;
		this.currentShootRate = this.shootRate[this.gunLevel];
		this.currentBulletPrefab = this.bulletPrefabs[this.gunLevel];
	}

	public virtual string GetBulletDamageWithAttackLevel(int attackLevel)
	{
		this.SetAttackLevel(attackLevel);
		return GameManager.CurrencyToString((float)this.currentBulletDamage / 100f);
	}

	protected virtual void Shoot()
	{
		Bullet bullet = this.GetPooledBullets();
		bullet.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1.2f, base.transform.position.z + 1f);
		bullet.gameObject.SetActive(true);
		bullet.transform.DOKill(false);
		bullet.SetDamageAmount(this.currentBulletDamage);
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		bullet.transform.DOBlendableMoveBy(new Vector3(0f, 0f, this.bulletTravelDistance), this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullet.gameObject.SetActive(false);
		});
	}

	protected Bullet GetPooledBullets()
	{
		for (int i = 0; i < this.bulletPool.Count; i++)
		{
			if (!this.bulletPool[i].gameObject.activeSelf)
			{
				return this.bulletPool[i];
			}
		}
		Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.currentBulletPrefab);
		this.bulletPool.Add(bullet);
		return bullet;
	}
}
