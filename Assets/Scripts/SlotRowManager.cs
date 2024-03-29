using System;
using UnityEngine;

public class SlotRowManager : MonoBehaviour
{
	public GameObject objectToFollow;

	private Vector3 offsetWithObjectToFollow;

	public float minX;

	public float maxX;

	private float originalMinX;

	private float originalMaxX;

	private int animalInsideRadar;

	private void Awake()
	{
		this.offsetWithObjectToFollow = base.transform.position - this.objectToFollow.transform.position;
		this.originalMinX = this.minX;
		this.originalMaxX = this.maxX;
	}

	private void LateUpdate()
	{
		base.transform.position = new Vector3(Mathf.Lerp(base.transform.position.x, Mathf.Clamp(this.objectToFollow.transform.position.x + this.offsetWithObjectToFollow.x, this.minX, this.maxX), 0.15f), 0f, base.transform.position.z);
	}

	public void AnimalEnterCheckPointRadar(GameObject checkpointRadarObject)
	{
	}

	public void ResetMinMaxToOriginal()
	{
		this.minX = this.originalMinX;
		this.maxX = this.originalMaxX;
	}

	public void AnimalExitCheckPointRadar(GameObject checkpointRadarObject)
	{
	}
}
