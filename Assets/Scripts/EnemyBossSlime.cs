using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBossSlime : EnemyBoss
{
	public int slimeIteration;

	private int _slimeIndex_k__BackingField;

	public int numberOfFinalSlimeNeedToBeDestroyed;

	public float coinDropMultiplicationForEachIteration;

	public float baseHPMultiplicationForEachIteration;

	public Vector3[] slimeOffset;

	public HealthBarController[] healthBarPrefabs;

	public Vector3[] healthBarOffsets;

	public EnemyBossSlime bossSlimePrefab;

	public EnemyBossSlimeWaveSpawner enemyBossWaveSpawnerPrefab;

	public int slimeIndex
	{
		get;
		set;
	}

	protected override void OnEnemyDestroyed()
	{
		if (this.slimeIndex < this.slimeIteration)
		{
			for (int i = 0; i < 2; i++)
			{
				EnemyBossSlime enemyBossSlime = UnityEngine.Object.Instantiate<EnemyBossSlime>(this.bossSlimePrefab);
				enemyBossSlime.shootable = false;
				enemyBossSlime.minCoinDrop = (int)((float)this.minCoinDrop * this.coinDropMultiplicationForEachIteration);
				enemyBossSlime.maxCoinDrop = (int)((float)this.minCoinDrop * this.coinDropMultiplicationForEachIteration);
				enemyBossSlime.transform.position = base.transform.position;
				enemyBossSlime.slimeIndex = this.slimeIndex + 1;
				enemyBossSlime.maxHealth = (int)((float)this.maxHealth * this.baseHPMultiplicationForEachIteration);
				enemyBossSlime.transform.localScale = Vector3.one * base.transform.localScale.x * 0.75f;
				if (i == 0)
				{
					enemyBossSlime.transform.DOJump(new Vector3(base.transform.position.x - this.slimeOffset[this.slimeIndex].x, 0f, base.transform.position.z - this.slimeOffset[this.slimeIndex].z), 3f, 1, 1f, false).OnComplete(delegate
					{
						this.shootable = true;
					});
				}
				else if (i == 1)
				{
					enemyBossSlime.transform.DOJump(new Vector3(base.transform.position.x + this.slimeOffset[this.slimeIndex].x, 0f, base.transform.position.z - this.slimeOffset[this.slimeIndex].z), 3f, 1, 1f, false).OnComplete(delegate
					{
						this.shootable = true;
					});
				}
			}
		}
		else
		{
			GameManager.BossSlimeDestroyed(this.numberOfFinalSlimeNeedToBeDestroyed);
		}
	}

	protected override void ExtraFunctionOnEnable()
	{
		base.transform.DOMoveZ(20f, 1f, false);
		if (this.enemyBossWaveSpawnerPrefab != null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.enemyBossWaveSpawnerPrefab.gameObject);
		}
	}

	protected override void SetupHealthBar()
	{
		this.healthBarInstance = UnityEngine.Object.Instantiate<HealthBarController>(this.healthBarPrefabs[this.slimeIndex]);
		this.healthBarInstance.transform.SetParent(GameManager.WorldCanvas.transform, true);
		this.healthBarOffset = this.healthBarOffsets[this.slimeIndex];
		this.healthBarInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - this.healthBarOffset.z);
	}
}
