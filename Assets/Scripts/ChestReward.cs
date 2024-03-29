using System;
using UnityEngine;

public abstract class ChestReward : MonoBehaviour
{
	public string rewardName;

	protected int outputAmount;

	public float probability;

	public int GetAmount()
	{
		return this.outputAmount;
	}

	public abstract void ActionAfterReward();
}
