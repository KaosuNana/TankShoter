using System;
using UnityEngine;

public class DemoTimeOfDay : MonoBehaviour
{
	public Light lightSource;

	public float minAngle = -15f;

	public float cycleDuration = 10f;

	private float maxAngle = 50f;

	private float yAngle = 60f;

	private float maxIntensity = 1f;

	private void Start()
	{
		if (!this.lightSource)
		{
			base.enabled = false;
		}
		this.maxAngle = base.transform.eulerAngles.x;
		this.yAngle = base.transform.eulerAngles.y;
		this.maxIntensity = this.lightSource.intensity;
	}

	private void Update()
	{
		float num = Time.time / this.cycleDuration;
		float num2 = Mathf.Cos(num * 3.14159274f * 2f) * 0.5f + 0.5f;
		this.lightSource.intensity = num2 * this.maxIntensity;
		float x = this.minAngle + num2 * (this.maxAngle - this.minAngle);
		base.transform.eulerAngles = new Vector3(x, this.yAngle, 0f);
		DynamicGI.UpdateEnvironment();
	}
}
