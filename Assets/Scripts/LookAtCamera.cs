using System;
using UnityEngine;

[Serializable]
public class LookAtCamera : MonoBehaviour
{
	public Camera lookAtCamera;

	public bool lookOnlyOnAwake;

	public void Start()
	{
		if (!this.lookAtCamera)
		{
			this.lookAtCamera = Camera.main;
		}
		if (this.lookOnlyOnAwake)
		{
			this._LookAtCamera();
		}
	}

	public void Update()
	{
		if (!this.lookOnlyOnAwake)
		{
			this._LookAtCamera();
		}
	}

	public void _LookAtCamera()
	{
		this.transform.LookAt(this.lookAtCamera.transform);
	}

	public void Main()
	{
	}
}
