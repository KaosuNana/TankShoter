using System;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
	public GameObject playerInstance;

	private void Update()
	{
		base.transform.position = this.playerInstance.transform.position;
	}
}
