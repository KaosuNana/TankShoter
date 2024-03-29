using System;
using UnityEngine;

public class EnemyBomb : EnemyNormal
{
	public GameObject explodeRadarPrefab;

	protected override void OnEnemyDestroyed()
	{
		UnityEngine.Object.Instantiate<GameObject>(this.explodeRadarPrefab, base.transform.position, Quaternion.identity);
	}
}
