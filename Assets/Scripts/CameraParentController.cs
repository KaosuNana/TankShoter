using DG.Tweening;
using System;
using UnityEngine;

public class CameraParentController : MonoBehaviour
{
	public void ShakeCamera()
	{
		base.transform.DOShakePosition(2f, new Vector3(10.8f, -10.8f, 0f), 100, 0f, false, true).SetUpdate(true);
	}
}
