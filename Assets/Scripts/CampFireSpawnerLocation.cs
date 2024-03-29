using System;
using UnityEngine;

public class CampFireSpawnerLocation : SpawnerLocation
{
	private bool spawnedCampFire;

	public void SpawnCampFire(GameObject campFirePrefab)
	{
		if (this.spawnedCampFire)
		{
			return;
		}
		this.spawnedCampFire = true;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(campFirePrefab);
		gameObject.transform.SetParent(base.transform, false);
		gameObject.transform.localPosition = Vector3.zero;
	}
}
