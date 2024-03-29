using System;
using UnityEngine;
using UnityEngine.UI;

public class VehiclePageImageSlot : MonoBehaviour
{
	public int teamPositionIndex;

	public Image currentVehicleImage;

	public Vehicle currentVehicle;

	public void RemoveVehicleFromSlot()
	{
		this.currentVehicle = null;
		this.currentVehicleImage = null;
	}

	public void AssignVehicle(Vehicle inputVehicle, Image inputVehicleImage)
	{
		if (this.currentVehicleImage != null)
		{
			this.currentVehicleImage.gameObject.SetActive(false);
		}
		if (this.currentVehicle != null)
		{
			this.currentVehicle.SetVehicleToTeamPosition(-1);
		}
		this.currentVehicle = inputVehicle;
		this.currentVehicle.SetVehicleToTeamPosition(this.teamPositionIndex);
		this.currentVehicleImage = inputVehicleImage;
		this.currentVehicleImage.gameObject.SetActive(true);
		this.currentVehicleImage.transform.SetParent(base.transform, false);
	}
}
