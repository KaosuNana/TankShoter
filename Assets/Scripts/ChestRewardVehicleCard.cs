using System;
using UnityEngine;

public class ChestRewardVehicleCard : ChestReward
{
	private Vehicle vehicleInstance;

	public int minAmount;

	public int maxAmount;

	private void Start()
	{
		this.outputAmount = UnityEngine.Random.Range(this.minAmount, this.maxAmount + 1);
	}

	public void AssignVehicle(Vehicle vehicleInput)
	{
		this.vehicleInstance = vehicleInput;
		this.rewardName = this.vehicleInstance.vehicleName;
		this.vehicleInstance.transform.SetParent(base.transform, false);
		vehicleInput.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

	public override void ActionAfterReward()
	{
		this.vehicleInstance.AddCards(this.outputAmount);
	}
}
