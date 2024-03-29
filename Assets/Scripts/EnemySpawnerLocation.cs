using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawnerLocation : SpawnerLocation
{
	private GameObject _activeEnemy_k__BackingField;

	public GameObject activeEnemy
	{
		get;
		set;
	}
}
