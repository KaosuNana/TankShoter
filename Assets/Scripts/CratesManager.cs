using System;
using UnityEngine;

public class CratesManager : MonoBehaviour
{
	public GameObject[] rewardList;

	private GameObject currentReward;

	private void Awake()
	{
		this.currentReward = UnityEngine.Object.Instantiate<GameObject>(this.rewardList[UnityEngine.Random.Range(0, this.rewardList.Length)]);
		this.currentReward.transform.SetParent(base.transform, false);
		this.currentReward.transform.localPosition = new Vector3(0f, 0.5f, 0f);
	}

	public void StompedByPlayer()
	{
		this.currentReward.transform.localPosition = new Vector3(0f, 2.5f, 0f);
		this.currentReward.transform.SetParent(null, true);
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
