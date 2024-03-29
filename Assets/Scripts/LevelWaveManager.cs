using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelWaveManager : MonoBehaviour
{
	public int numberOfWavesBeforeBoss = 10;

	public ManualWavesManager[] wavesList;

	public ManualWavesManager[] bossWaveList;

	private WaveManager _checkpointWave_k__BackingField;

	private int currentNumberOfWaves;

	private int lineCount;

	private ManualWavesManager _currentManualWaves_k__BackingField;

	private int previousIndex;

	public WaveManager checkpointWave
	{
		get;
		set;
	}

	public ManualWavesManager currentManualWaves
	{
		get;
		set;
	}

	private void Awake()
	{
		this.ResetWaves();
	}

	public void ResetWaves()
	{
		this.currentNumberOfWaves = 0;
		this.lineCount = 0;
		this.previousIndex = -1;
	}

	public ManualWavesManager GetNextGroupWave()
	{
		this.currentNumberOfWaves++;
		if (this.currentNumberOfWaves < this.numberOfWavesBeforeBoss)
		{
			return this.wavesList[UnityEngine.Random.Range(0, this.wavesList.Length)];
		}
		this.currentNumberOfWaves = 0;
		return this.bossWaveList[UnityEngine.Random.Range(0, this.bossWaveList.Length)];
	}

	public WaveManager GetNextWave()
	{
		if (this.currentManualWaves == null)
		{
			this.UpdateRandomWave();
		}
		if (this.lineCount >= this.currentManualWaves.manualWaves.Length)
		{
			this.UpdateRandomWave();
			this.lineCount = 0;
			this.currentNumberOfWaves++;
		}
		if (this.currentNumberOfWaves == this.numberOfWavesBeforeBoss)
		{
			this.currentNumberOfWaves++;
			return this.bossWaveList[UnityEngine.Random.Range(0, this.bossWaveList.Length)].manualWaves[0];
		}
		WaveManager result = this.currentManualWaves.manualWaves[this.lineCount];
		this.lineCount++;
		return result;
	}

	private void UpdateRandomWave()
	{
		int num = UnityEngine.Random.Range(0, this.wavesList.Length);
		if (this.wavesList.Length > 1)
		{
			while (num == this.previousIndex)
			{
				num = UnityEngine.Random.Range(0, this.wavesList.Length);
			}
		}
		this.previousIndex = num;
		this.currentManualWaves = this.wavesList[num];
	}
}
