using System;
using UnityEngine;

public class ChestRewardCoin : ChestReward
{
	public int minAmount;

	public int maxAmount;

	private void Start()
	{
		this.outputAmount = UnityEngine.Random.Range(this.minAmount, this.maxAmount + 1);
	}

	public override void ActionAfterReward()
	{
		EventManager.TriggerEvent("EventCoinChanges");
		GameManager.ExtraCoin(this.outputAmount);
	}
}
