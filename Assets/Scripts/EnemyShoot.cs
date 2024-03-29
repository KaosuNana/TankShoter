using DG.Tweening;
using System;
using UnityEngine;

public class EnemyShoot : EnemyNormal
{
	public EnemyGunController enemyGunPrefab;

	public Transform enemyGunSlot;

	private EnemyGunController enemyGunInstance;

	private float shootRate;

	private Tween redTween;

	protected override void OnStartFunction()
	{
		if (this.enemyGunInstance == null)
		{
			this.enemyGunInstance = UnityEngine.Object.Instantiate<EnemyGunController>(this.enemyGunPrefab);
		}
		else
		{
			this.enemyGunInstance.StopShooting();
		}
		this.enemyGunInstance.transform.SetParent(base.transform, false);
		if (this.enemyGunSlot != null)
		{
			this.enemyGunInstance.transform.position = this.enemyGunSlot.position;
		}
		this.shootRate = this.enemyGunInstance.shootRate;
	}

	protected override void OnEnterEnemyManagerCollider()
	{
		base.InvokeRepeating("ChargingBeforeShoot", 0f, this.shootRate);
		base.Invoke("StartShooting", 0.5f);
	}

	private void StartShooting()
	{
		this.enemyGunInstance.StartShooting();
	}

	private void ChargingBeforeShoot()
	{
		this.redTween = this.myRenderer.material.DOColor(Color.red, 0.5f).OnComplete(delegate
		{
			this.myRenderer.material.DOColor(Color.white, 0.1f);
		});
	}

	protected override void OnExitEnemyManagerCollider()
	{
		this.enemyGunInstance.StopShooting();
	}

	protected override void OnEnemyDestroyed()
	{
		this.enemyGunInstance.StopShooting();
	}

	private void OnDisable()
	{
		base.CancelInvoke();
		if (this.redTween != null)
		{
			this.redTween.Kill(false);
		}
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this.myRenderer.material.color = Color.white;
		this.currentEnemySpeed = this.enemySpeed;
		this.shootable = false;
		this.destroyed = false;
		this.firstShot = false;
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - Time.deltaTime * this.currentEnemySpeed);
		base.UpdateHealthBarPosition();
		if (this.enemyGunInstance.target != null)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(new Vector3(this.enemyGunInstance.target.transform.position.x - base.transform.position.x, 0f, this.enemyGunInstance.target.transform.position.z - base.transform.position.z)), 10f * Time.deltaTime);
		}
		else
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 10f * Time.deltaTime);
		}
	}
}
