using System;
using UnityEngine;

public class HomingRadar : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9 || other.gameObject.layer == 25)
		{
			base.transform.parent.GetComponent<BulletHoming>().LockEnemyTarget(other.gameObject);
		}
	}
}
