using System;
using UnityEngine;

public class EnemyBossWaveManager : WaveManager
{
	public Enemy.BossType bossType;

	private void Awake()
	{
		if (this.delayNextWave == 0f)
		{
			this.delayNextWave = 1000000f;
		}
		base.enemySpawnerLocation = base.GetComponentsInChildren<EnemySpawnerLocation>(true);
	}

	public override float SpawnEnemy(MasterPoolManager masterPoolManager, bool shouldFlyDown)
	{
		GameObject bossEnemyType = masterPoolManager.GetBossEnemyType(this.bossType);
		base.enemySpawnerLocation[0].activeEnemy = bossEnemyType;
		bossEnemyType.GetComponent<Enemy>().waveParentInstance = this;
		bossEnemyType.transform.SetParent(base.transform, false);
		bossEnemyType.transform.localPosition = base.enemySpawnerLocation[0].transform.localPosition;
		bossEnemyType.SetActive(true);
		return this.delayNextWave;
	}
}
