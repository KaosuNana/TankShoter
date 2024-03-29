using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class WaveManager : MonoBehaviour
{
	private EnemySpawnerLocation[] _enemySpawnerLocation_k__BackingField;

	public Wave.WaveLevel waveLevel;

	public int extraScoreWhenWaveCleared;

	protected int numberOfEnemy;

	protected int numberOfChildrenEnemyDestroyed;

	public float delayNextWave;

	public EnemySpawnerLocation[] enemySpawnerLocation
	{
		get;
		set;
	}

	public abstract float SpawnEnemy(MasterPoolManager masterPoolManager, bool shouldFlyDown);

	public void EnemyGotDestroyed(int extraScore, Vector3 position)
	{
		this.numberOfChildrenEnemyDestroyed++;
		GameManager.ExtraScore(extraScore, position, "+");
		if (this.numberOfChildrenEnemyDestroyed == this.numberOfEnemy)
		{
			EventManager.TriggerEvent("WaveCleared");
			GameManager.ExtraScore(this.extraScoreWhenWaveCleared, new Vector3(position.x, position.y + 2f, position.z), "wave cleared +");
			GameManager.ExtraCharge(10);
		}
	}
}
