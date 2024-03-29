using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpSidekickFXController : MonoBehaviour
{
	public Sprite[] tankImages;

	public Color[] backgroundColor;

	public Image tankImage;

	public Image backgroundImage;

	public void ActivateSidekickForVehicle(Vehicle.ID vehicleID)
	{
		base.GetComponent<Animator>().SetTrigger("Action");
		this.tankImage.sprite = this.tankImages[(int)vehicleID];
		this.backgroundImage.color = this.backgroundColor[(int)vehicleID];
	}
}
