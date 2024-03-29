using System;
using UnityEngine;

public class GroundController : MonoBehaviour
{
	public GameObject[] chunks;

	public GameObject[] chunkToSwap;

	private int activeChunk;

	private GameObject transitionObject;

	private void Start()
	{
		this.UpdateChunks();
	}

	public float GetZPositionOfTransition()
	{
		return this.chunkToSwap[this.activeChunk].transform.position.z;
	}

	public void CreateTransition(GameObject[] input)
	{
		this.transitionObject = input[this.activeChunk];
		this.transitionObject.SetActive(true);
		this.transitionObject.transform.SetParent(base.transform, false);
		this.transitionObject.transform.localScale = Vector3.one;
		this.transitionObject.transform.localPosition = this.chunkToSwap[this.activeChunk].transform.localPosition;
		this.chunkToSwap[this.activeChunk].SetActive(false);
	}

	public void UpdateChunks()
	{
		this.chunkToSwap[0].SetActive(true);
		this.chunkToSwap[1].SetActive(true);
		if (this.transitionObject != null)
		{
			this.transitionObject.SetActive(false);
			this.transitionObject = null;
		}
		if (GameManager.CurrentLevel % 2 == 0)
		{
			this.activeChunk = 1;
			this.chunks[0].SetActive(false);
			this.chunks[1].SetActive(true);
		}
		else
		{
			this.activeChunk = 0;
			this.chunks[0].SetActive(true);
			this.chunks[1].SetActive(false);
		}
	}
}
