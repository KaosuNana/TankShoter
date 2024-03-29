using System;
using UnityEngine;

public class EnemyWaveManager : WaveManager
{
	public Enemy.NormalType[] enemyType;

	public int[] enemyTypeToSpawnAmount;

	private void Awake()
	{
		if (this.delayNextWave == 0f)
		{
			this.delayNextWave = 2f;
		}
		base.enemySpawnerLocation = base.GetComponentsInChildren<EnemySpawnerLocation>(true);
	}

	private void OnEnable()
	{
		this.numberOfChildrenEnemyDestroyed = 0;
	}

	public override float SpawnEnemy(MasterPoolManager masterPoolManager, bool shouldFlyDown)
	{
		int num = UnityEngine.Random.Range(0, base.enemySpawnerLocation.Length);
		for (int i = 0; i < this.enemyTypeToSpawnAmount.Length; i++)
		{
			int num2 = this.enemyTypeToSpawnAmount[i];
			if (i < this.enemyType.Length && num2 > 0)
			{
				for (int j = 0; j < num2; j++)
				{
					while (base.enemySpawnerLocation[num].activeEnemy)
					{
						num = UnityEngine.Random.Range(0, base.enemySpawnerLocation.Length);
					}
					GameObject normalEnemyType = masterPoolManager.GetNormalEnemyType(this.enemyType[i]);
					base.enemySpawnerLocation[num].activeEnemy = normalEnemyType;
					normalEnemyType.transform.SetParent(base.transform, false);
					if (this.enemyType[i] != Enemy.NormalType.EnemyEmpty)
					{
						normalEnemyType.GetComponent<Enemy>().waveParentInstance = this;
						normalEnemyType.GetComponent<Enemy>().SetInitialPosition(base.enemySpawnerLocation[num].transform.localPosition, shouldFlyDown);
						this.numberOfEnemy++;
					}
				}
			}
		}
		for (int k = 0; k < base.enemySpawnerLocation.Length; k++)
		{
			if (!base.enemySpawnerLocation[k].activeEnemy)
			{
				GameObject normalEnemyType2 = masterPoolManager.GetNormalEnemyType(this.enemyType[0]);
				base.enemySpawnerLocation[k].activeEnemy = normalEnemyType2;
				normalEnemyType2.transform.SetParent(base.transform, false);
				if (this.enemyType[0] != Enemy.NormalType.EnemyEmpty)
				{
					normalEnemyType2.GetComponent<Enemy>().waveParentInstance = this;
					normalEnemyType2.GetComponent<Enemy>().SetInitialPosition(base.enemySpawnerLocation[k].transform.localPosition, shouldFlyDown);
					this.numberOfEnemy++;
				}
			}
		}
		return this.delayNextWave;
	}
}
