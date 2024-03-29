using System;
using UnityEngine;

public class EnemyLaserGate : Enemy
{
	public BulletEnemyLazer lazerGateBullet;

	private EnemyLaserGateChildren[] laserGateChildrens;

	protected override void ExtraFunctionOnEnable()
	{
		for (int i = 0; i < this.laserGateChildrens.Length; i++)
		{
			this.laserGateChildrens[i].gameObject.SetActive(true);
			this.lazerGateBullet.gameObject.SetActive(true);
		}
	}

	protected override void SpecialEffectOnFirstHit()
	{
	}

	protected override void OnEnemyDestroyed()
	{
	}

	protected override void OnStartFunction()
	{
	}

	protected override void OnAwakeFunction()
	{
		this.laserGateChildrens = base.GetComponentsInChildren<EnemyLaserGateChildren>();
	}

	public override void SetInitialPosition(Vector3 localPos, bool shouldFlyDown)
	{
		base.transform.localPosition = localPos;
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - Time.deltaTime * this.enemySpeed);
	}

	public void LaserGateChildDestroyed()
	{
		this.lazerGateBullet.gameObject.SetActive(false);
	}
}
