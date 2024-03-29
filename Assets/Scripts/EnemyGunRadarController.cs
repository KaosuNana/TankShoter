using System;
using UnityEngine;

public class EnemyGunRadarController : MonoBehaviour
{
	public EnemyGunController enemyGunInstance;

	private Quaternion rotation;

	private void Awake()
	{
		this.rotation = base.transform.rotation;
	}

	private void OnTriggerEnter(Collider other)
	{
		this.enemyGunInstance.LockInTarget(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		this.enemyGunInstance.RemoveTarget();
	}

	private void LateUpdate()
	{
		base.transform.rotation = this.rotation;
	}
}
