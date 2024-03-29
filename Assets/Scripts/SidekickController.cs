using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SidekickController : MonoBehaviour
{
	private VehicleManager _vehicleManagerInstance_k__BackingField;

	private Vehicle _vehicleInstance_k__BackingField;

	public VehicleManager vehicleManagerInstance
	{
		get;
		set;
	}

	public Vehicle vehicleInstance
	{
		get;
		set;
	}

	private void Awake()
	{
		this.vehicleManagerInstance = GameManager.instance.vehicleManagerInstance;
	}

	private void Update()
	{
		if (this.vehicleInstance != null && this.vehicleInstance.gunInstance.target != null)
		{
			base.transform.position = new Vector3(Mathf.Lerp(base.transform.position.x, this.vehicleInstance.gunInstance.target.transform.position.x, 0.025f), 0f, base.transform.position.z);
		}
	}

	public void ActivateSidekickPowerUp(Vehicle.ID vehicleID, float duration)
	{
		base.CancelInvoke();
		if (this.vehicleInstance != null)
		{
			this.vehicleInstance.transform.DOKill(false);
			this.vehicleInstance.ActivateRapidMode(duration);
			this.vehicleInstance.StartShooting();
		}
		else
		{
			this.vehicleInstance = this.vehicleManagerInstance.GetVehicle(vehicleID);
			GameManager.PlaySidekickFX(vehicleID);
			this.vehicleInstance.UpdatePreferredGunForMainPlayer(false);
			this.vehicleInstance.StartShooting();
			this.vehicleInstance.DisableCollider();
			this.vehicleInstance.transform.SetParent(base.transform, false);
			this.vehicleInstance.transform.localPosition = new Vector3(0f, 0f, -10f);
		}
		this.vehicleInstance.transform.DOLocalMoveZ(0f, 0.5f, false);
		base.Invoke("DeactivateVehicle", duration);
	}

	public void DeactivateVehicle()
	{
		this.vehicleInstance.StopShooting();
		this.vehicleInstance.transform.DOLocalMoveZ(-10f, 0.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(this.vehicleInstance.gameObject);
			base.gameObject.SetActive(false);
		});
	}

	public void StartShooting()
	{
		this.vehicleInstance.StartShooting();
	}

	public void StopShooting()
	{
		this.vehicleInstance.StopShooting();
	}

	public void ActivateRapidMode(float duration)
	{
		this.vehicleInstance.ActivateRapidMode(duration);
	}
}
