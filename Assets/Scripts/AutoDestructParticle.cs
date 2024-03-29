using System;
using UnityEngine;

public class AutoDestructParticle : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, 3f);
	}
}
