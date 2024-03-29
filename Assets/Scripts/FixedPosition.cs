using System;
using UnityEngine;

public class FixedPosition : MonoBehaviour
{
	private Quaternion rotation;

	private void LateUpdate()
	{
		base.transform.rotation = Quaternion.Euler(Vector3.zero);
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, 0f, base.transform.localPosition.z);
	}
}
