using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBossSlimeWaveSpawner : MonoBehaviour
{
	public EnemyWaveManager[] enemyWavePrefab;

	private int enemyWaveIndex;

	public float delayBetweenWaves = 5f;

	private void Awake()
	{
		base.InvokeRepeating("SpawnEnemyWaves", 1f, this.delayBetweenWaves);
		EventManager.StartListening("EventBossSlimeDestroyed", new UnityAction(this.DestroySelf));
	}

	private void DestroySelf()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void SpawnEnemyWaves()
	{
		this.SpawnEnemyWaves(this.enemyWaveIndex % this.enemyWavePrefab.Length);
		this.enemyWaveIndex++;
	}

	private void SpawnEnemyWaves(int index)
	{
		EnemyWaveManager enemyWaveManager = UnityEngine.Object.Instantiate<EnemyWaveManager>(this.enemyWavePrefab[index]);
		enemyWaveManager.transform.position = new Vector3(0f, 0f, 24f);
		enemyWaveManager.SpawnEnemy(GameManager.instance.masterPoolManagerInstance, true);
	}
}
