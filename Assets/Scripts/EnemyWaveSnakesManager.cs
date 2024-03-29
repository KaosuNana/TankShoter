using System;
using UnityEngine;

public class EnemyWaveSnakesManager : EnemyWaveManager
{
	public override float SpawnEnemy(MasterPoolManager masterPoolManager, bool shouldFlyDown)
	{
		UnityEngine.Debug.Log("spawning galaga ");
		for (int i = 0; i < base.enemySpawnerLocation.Length; i++)
		{
			if (!base.enemySpawnerLocation[i].activeEnemy)
			{
				GameObject normalEnemyType = masterPoolManager.GetNormalEnemyType(this.enemyType[0]);
				base.enemySpawnerLocation[i].activeEnemy = normalEnemyType;
				normalEnemyType.GetComponent<Enemy>().waveParentInstance = this;
				normalEnemyType.transform.SetParent(base.transform, false);
				normalEnemyType.transform.localPosition = base.enemySpawnerLocation[i].transform.localPosition;
				normalEnemyType.SetActive(true);
				normalEnemyType.GetComponent<EnemyGalaga>().StartAnimatingWithDelay((float)i * 0.2f);
			}
		}
		return this.delayNextWave;
	}
}
