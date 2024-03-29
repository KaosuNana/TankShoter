// using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	private sealed class _SpawnBossWave_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal WaveManager wavePrefab;

		internal WaveManager _waveTemp___0;

		internal LevelManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _SpawnBossWave_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(0.25f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				SoundManager.PlayMusic(SoundManager.Music.BossMusic, 1f);
				GameManager.WarnBossWave();
				this._current = new WaitForSeconds(2f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._waveTemp___0 = UnityEngine.Object.Instantiate<WaveManager>(this.wavePrefab);
				this._this.delayBeforeSpawningNextWaves = this._waveTemp___0.SpawnEnemy(this._this.masterPoolManager, false);
				this._waveTemp___0.transform.position = new Vector3(0f, 0f, 32f);
				this._this.waveCount++;
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	private HUDMenuController hudMenuInstance;

	private MasterPoolManager masterPoolManager;

	public LevelWaveManager[] levelWaveManagerList;

	private int waveCount;

	private float delayBeforeSpawningNextWaves = 1.5f;

	private float currentDelayBeforeSpawningNextWaves;

	private int _currentLevel_k__BackingField;

	private int currentLevelIndex;

	private ManualWavesManager manualGroupWavesTemp;

	private bool stopAutoSpawning;

	private LevelWaveManager currentLevelWaveManager;

	public int currentLevel
	{
		get;
		set;
	}

	private void Awake()
	{
		// this.currentLevel = ObscuredPrefs.GetInt("AchievedLevel", 1);
		this.currentLevel = PlayerPrefs.GetInt("AchievedLevel", 1);
		this.currentLevelIndex = this.currentLevel - 1;
		this.hudMenuInstance = GameManager.instance.canvasInstance.hudMenuInstance;
		this.masterPoolManager = GameManager.instance.masterPoolManagerInstance;
		this.hudMenuInstance.UpdateLevelTo(this.currentLevel.ToString());
		this.currentLevelWaveManager = this.levelWaveManagerList[Mathf.Min(this.currentLevelIndex, 12)];
		this.currentLevelWaveManager.ResetWaves();
	}

	public void SaveLevel()
	{
		// ObscuredPrefs.SetInt("AchievedLevel", this.currentLevel);
		PlayerPrefs.SetInt("AchievedLevel", this.currentLevel);
	}

	public void PlayerPrestige()
	{
		// ObscuredPrefs.SetInt("AchievedLevel", 1);
		PlayerPrefs.SetInt("AchievedLevel", 1);
	}

	public void NextLevel()
	{
		this.currentLevel++;
		GameSingleton.ReportScoreToLeaderboard(this.currentLevel);
		this.SaveLevel();
		this.currentLevelIndex = this.currentLevel - 1;
		this.waveCount = 0;
		this.hudMenuInstance.UpdateLevelTo(this.currentLevel.ToString());
		this.currentLevelWaveManager = this.levelWaveManagerList[Mathf.Min(this.currentLevelIndex, 12)];
		this.currentLevelWaveManager.ResetWaves();
	}

	public void SpawnNextWave()
	{
		this.currentDelayBeforeSpawningNextWaves = 0f;
		this.stopAutoSpawning = false;
		WaveManager nextWave = this.currentLevelWaveManager.GetNextWave();
		if (nextWave.GetComponent<EnemyBossWaveManager>() != null)
		{
			this.stopAutoSpawning = true;
			base.StartCoroutine(this.SpawnBossWave(nextWave));
		}
		else
		{
			WaveManager waveManager = UnityEngine.Object.Instantiate<WaveManager>(nextWave);
			this.delayBeforeSpawningNextWaves = waveManager.SpawnEnemy(this.masterPoolManager, false);
			waveManager.transform.position = new Vector3(0f, 0f, 32f);
			this.waveCount++;
		}
	}

	private IEnumerator SpawnBossWave(WaveManager wavePrefab)
	{
		LevelManager._SpawnBossWave_c__Iterator0 _SpawnBossWave_c__Iterator = new LevelManager._SpawnBossWave_c__Iterator0();
		_SpawnBossWave_c__Iterator.wavePrefab = wavePrefab;
		_SpawnBossWave_c__Iterator._this = this;
		return _SpawnBossWave_c__Iterator;
	}

	private void Update()
	{
		if (GameManager.CurrentState == GameManager.GameState.PlayState)
		{
			if (!this.stopAutoSpawning)
			{
				this.currentDelayBeforeSpawningNextWaves += Time.deltaTime;
			}
			if (this.currentDelayBeforeSpawningNextWaves >= this.delayBeforeSpawningNextWaves)
			{
				this.SpawnNextWave();
			}
		}
	}
}
