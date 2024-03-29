using DG.Tweening;
using System;
using UnityEngine;

public class Rotating : MonoBehaviour
{
	public Vector3 rotationAxis;

	private void Start()
	{
		base.transform.DOBlendableRotateBy(this.rotationAxis * 360f, 5f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	private void Update()
	{
	}
}
