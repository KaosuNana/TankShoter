using System;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + -15f * Time.deltaTime);
	}
}
