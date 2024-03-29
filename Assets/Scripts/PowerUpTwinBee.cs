using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpTwinBee : PowerUp
{
	private sealed class _Spawned_c__AnonStorey0
	{
		internal Vector3 endPosition;

		internal PowerUpTwinBee _this;

		internal void __m__0()
		{
			this._this.transform.DOMove(this.endPosition, 2f, false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this._this.gameObject.SetActive(false);
			});
			this._this.Invoke("MakeItPermanent", 0.75f);
		}

		internal void __m__1()
		{
			this._this.gameObject.SetActive(false);
		}
	}

	public List<GameObject> powerUpList;

	public GameObject transparentCube;

	private bool shootable;

	private int index;

	public int shotCountBeforeChange;

	private int currentShotCount;

	private Collider myCollider;

	private void Awake()
	{
		this.shootable = true;
		this.myCollider = base.GetComponent<Collider>();
		int preferredVehicleIndex = GameManager.instance.vehicleManagerInstance.GetPreferredVehicleIndex();
		UnityEngine.Object.Destroy(this.powerUpList[preferredVehicleIndex].gameObject);
		this.powerUpList.RemoveAt(preferredVehicleIndex);
		this.index = UnityEngine.Random.Range(0, this.powerUpList.Count);
		this.UpdateSidekickPowerUp();
	}

	private void OnEnable()
	{
		base.CancelInvoke();
		this.transparentCube.transform.localScale = Vector3.one * 2f;
		this.shootable = true;
		this.myCollider.enabled = true;
		this.index = UnityEngine.Random.Range(0, this.powerUpList.Count);
		this.UpdateSidekickPowerUp();
		for (int i = 0; i < this.powerUpList.Count; i++)
		{
			this.powerUpList[i].GetComponent<PowerUpSidekick>().DisableCollider();
		}
	}

	private void UpdateSidekickPowerUp()
	{
		for (int i = 0; i < this.powerUpList.Count; i++)
		{
			this.powerUpList[i].SetActive(this.index % this.powerUpList.Count == i);
		}
	}

	public override void ActivatePowerUp()
	{
		for (int i = 0; i < this.powerUpList.Count; i++)
		{
			if (this.powerUpList[i].activeSelf)
			{
				this.powerUpList[i].GetComponent<PowerUp>().ActivatePowerUp();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 20 && this.shootable)
		{
			this.currentShotCount++;
			this.Spawned();
		}
	}

	public override void Spawned()
	{
		if (this.currentShotCount % this.shotCountBeforeChange == 0)
		{
			this.index++;
			this.UpdateSidekickPowerUp();
		}
		base.transform.DOKill(false);
		base.CancelInvoke();
		this.transparentCube.transform.localScale = Vector3.one * 2f;
		this.shootable = true;
		this.myCollider.enabled = true;
		Vector3 endPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, -10f);
		float x = base.transform.position.x + (endPosition.x - base.transform.position.x) / 2f;
		base.transform.DOMove(new Vector3(x, 0f, Mathf.Min(base.transform.position.z + 0.1f, 12f)), 0.5f, false).OnComplete(delegate
		{
			this.transform.DOMove(endPosition, 2f, false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this.gameObject.SetActive(false);
			});
			this.Invoke("MakeItPermanent", 0.75f);
		});
		base.transform.DOBlendableLocalRotateBy(new Vector3(0f, 1440f, 0f), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuart);
	}

	private void MakeItPermanent()
	{
		this.transparentCube.transform.DOScale(Vector3.zero, 0.5f);
		this.shootable = false;
		this.myCollider.enabled = false;
		for (int i = 0; i < this.powerUpList.Count; i++)
		{
			this.powerUpList[i].GetComponent<PowerUpSidekick>().EnableCollider();
		}
	}

	public override string GetPowerUpInfoForLevel(int level)
	{
		return string.Empty;
	}
}
