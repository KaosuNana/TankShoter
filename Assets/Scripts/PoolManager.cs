using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager instance;

	public List<GameObject> pooledObjectPrefab;

	public int pooledAmount = 20;

	public bool willGrow = true;

	public bool addRandomly;

	public List<GameObject> pooledObjectsList;

	private void Awake()
	{
		PoolManager.instance = this;
		this.pooledObjectsList = new List<GameObject>();
		this.StartPooling();
	}

	protected virtual void StartPooling()
	{
		for (int i = 0; i < this.pooledAmount; i++)
		{
			this.AddNewObject(i);
		}
	}

	protected GameObject AddNewObjectRandomly()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObjectPrefab[UnityEngine.Random.Range(0, this.pooledObjectPrefab.Count)]);
		this.pooledObjectsList.Add(gameObject);
		return gameObject;
	}

	protected GameObject AddNewObject(int i)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObjectPrefab[i % this.pooledObjectPrefab.Count]);
		gameObject.SetActive(false);
		this.pooledObjectsList.Add(gameObject);
		return gameObject;
	}

	private GameObject AddNewObjectFromPoolIndex(int i)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pooledObjectPrefab[i]);
		this.pooledObjectsList.Add(gameObject);
		return gameObject;
	}

	public GameObject GetEnemyBossPooledObjectForWave(int waveCount)
	{
		int num = waveCount / 10 % this.pooledObjectPrefab.Count;
		if (!this.pooledObjectsList[num].activeSelf)
		{
			return this.pooledObjectsList[num];
		}
		return this.AddNewObjectFromPoolIndex(num);
	}

	public GameObject GetRandomEnemyWavePooledObjectForWaveCount(int waveCount)
	{
		if (!this.pooledObjectsList[waveCount % 10].activeSelf)
		{
			return this.pooledObjectsList[waveCount % 10];
		}
		return this.AddNewObjectFromPoolIndex(waveCount % 10);
	}

	public GameObject GetRandomPooledObject()
	{
		if (this.pooledObjectsList.Count == 0 && this.willGrow)
		{
			return this.AddNewObjectRandomly();
		}
		for (int i = 0; i < 1000; i++)
		{
			int index = UnityEngine.Random.Range(0, this.pooledObjectsList.Count);
			if (!this.pooledObjectsList[index].activeSelf)
			{
				this.pooledObjectsList[index].SetActive(true);
				return this.pooledObjectsList[index];
			}
		}
		return this.AddNewObjectRandomly();
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < this.pooledObjectsList.Count; i++)
		{
			if (!this.pooledObjectsList[i].activeSelf)
			{
				this.pooledObjectsList[i].SetActive(true);
				return this.pooledObjectsList[i];
			}
		}
		if (this.willGrow)
		{
			GameObject gameObject = this.AddNewObjectRandomly();
			gameObject.SetActive(true);
			return gameObject;
		}
		return null;
	}
}
