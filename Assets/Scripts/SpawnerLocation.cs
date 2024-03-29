using System;
using UnityEngine;

public class SpawnerLocation : MonoBehaviour
{
	private void Start()
	{
		if (base.GetComponent<MeshRenderer>() != null)
		{
			base.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
