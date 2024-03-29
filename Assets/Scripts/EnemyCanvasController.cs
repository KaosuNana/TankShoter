using System;
using UnityEngine;

public class EnemyCanvasController : MonoBehaviour
{
	private void Awake()
	{
		base.GetComponent<Canvas>().worldCamera = Camera.main;
	}
}
