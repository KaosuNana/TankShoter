using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
	public GroundController[] groundList;

	public GameObject[] transitionObject;

	public float speed = 6f;

	private float currentSpeed;

	private int groundLength;

	private bool _shouldScroll_k__BackingField;

	public bool shouldScroll
	{
		get;
		set;
	}

	private void Awake()
	{
		this.groundLength = this.groundList.Length;
		this.UpdateScrollingSpeed();
	}

	public void CheckChunks()
	{
		float num = -1f;
		for (int i = 0; i < this.groundList.Length; i++)
		{
			float zPositionOfTransition = this.groundList[i].GetZPositionOfTransition();
			if (zPositionOfTransition > 35.5f && zPositionOfTransition < 100f && num == -1f)
			{
				this.groundList[i].CreateTransition(this.transitionObject);
				num = zPositionOfTransition;
			}
			else if (this.groundList[i].transform.position.z > num)
			{
				this.groundList[i].UpdateChunks();
			}
		}
	}

	public void StartScrolling()
	{
		this.shouldScroll = true;
	}

	public void StopScrolling()
	{
		this.shouldScroll = false;
	}

	public void UpdateScrollingSpeed()
	{
		this.currentSpeed = this.speed;
	}

	private void Update()
	{
		if (this.shouldScroll)
		{
			for (int i = 0; i < this.groundLength; i++)
			{
				this.groundList[i].transform.position = new Vector3(this.groundList[i].transform.position.x, this.groundList[i].transform.position.y, this.groundList[i].transform.position.z - this.currentSpeed * Time.deltaTime);
				if (this.groundList[i].transform.position.z <= -100f)
				{
					this.groundList[i].transform.position = new Vector3(this.groundList[i].transform.position.x, this.groundList[i].transform.position.y, this.groundList[i].transform.position.z + (float)(50 * this.groundLength));
					this.groundList[i].UpdateChunks();
				}
			}
		}
	}
}
