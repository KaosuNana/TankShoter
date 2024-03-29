using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBossCheeky : EnemyBoss
{
	private int currentHitCount = 1;

	private float currentHitTreshold = 0.9f;

	private int direction = 1;

	public int minCoinWhenHit;

	public int maxCoinWhenHit;

	private int countShootRate;

	protected override void ExtraBossActionWhenHitByBullet(GameObject other)
	{
		this.currentHitCount++;
		this.UpdatePosition();
	}

	protected override void ExtraFunctionOnEnable()
	{
		this.currentHitTreshold = 0.9f;
		base.transform.DOMoveZ(20f, 0.5f, false);
		base.InvokeRepeating("SpawnExtraEnemyWaves", 1f, 5f);
	}

	private void SpawnExtraEnemyWaves()
	{
		EnemyWaveManager enemyWaveManager = UnityEngine.Object.Instantiate<EnemyWaveManager>(this.enemyWavePrefab[0]);
		enemyWaveManager.transform.position = new Vector3(0f, 0f, 20f);
		enemyWaveManager.SpawnEnemy(GameManager.instance.masterPoolManagerInstance, true);
	}

	private void UpdatePosition()
	{
		if (this.healthBarInstance.progress <= this.currentHitTreshold)
		{
			this.currentHitTreshold -= 0.025f;
			this.direction *= -1;
			base.transform.DOMoveX((float)Mathf.Clamp(this.direction * UnityEngine.Random.Range(3, 5), -6, 6), 0.5f, false);
			Vector3 position = base.transform.position;
			position.y = 0.4f;
			int num = UnityEngine.Random.Range(this.minCoinWhenHit, this.maxCoinWhenHit + 1);
			for (int i = 0; i < num; i++)
			{
				GameManager.SpawnCoin(position);
			}
		}
	}
}
