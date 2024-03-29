using System;
using UnityEngine;

public class MasterPoolManager : MonoBehaviour
{
	public PoolManager[] enemyTypePool;

	public PoolManager[] enemyBossTypePool;

	public GameObject GetNormalEnemyType(Enemy.NormalType enemyType)
	{
		return this.enemyTypePool[(int)enemyType].GetPooledObject();
	}

	public GameObject GetBossEnemyType(Enemy.BossType bossType)
	{
		return this.enemyBossTypePool[(int)bossType].GetPooledObject();
	}
}
