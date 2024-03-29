using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static EnemyManager instance;

	public List<GameObject> enemyList;

	private void Awake()
	{
		EnemyManager.instance = this;
	}

	private void Start()
	{
		this.enemyList = new List<GameObject>();
	}

	private void OnTriggerEnter(Collider other)
	{
		this.enemyList.Add(other.gameObject);
	}

	public static void AddEnemy(GameObject input)
	{
		EnemyManager.instance.enemyList.Add(input);
	}

	public static GameObject GetRandomEnemy()
	{
		int i = 0;
		int count = EnemyManager.instance.enemyList.Count;
		while (i < 500)
		{
			if (EnemyManager.instance.enemyList.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, EnemyManager.instance.enemyList.Count);
				if (EnemyManager.instance.enemyList[index].gameObject.activeInHierarchy)
				{
					return EnemyManager.instance.enemyList[index];
				}
			}
			i++;
		}
		return null;
	}

	public static GameObject GetEnemy()
	{
		int i = 0;
		int count = EnemyManager.instance.enemyList.Count;
		while (i < count)
		{
			if (EnemyManager.instance.enemyList[i] == null)
			{
				EnemyManager.instance.enemyList.RemoveAt(i);
				i--;
				count = EnemyManager.instance.enemyList.Count;
			}
			else
			{
				if (EnemyManager.instance.enemyList[i].gameObject.activeInHierarchy)
				{
					return EnemyManager.instance.enemyList[i];
				}
				EnemyManager.instance.enemyList.RemoveAt(i);
				i--;
				count = EnemyManager.instance.enemyList.Count;
			}
			i++;
		}
		return null;
	}
}
