using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
	public enum PowerUpType
	{
		RapidFire,
		Magnet,
		Heal,
		Sidekick,
		Shield,
		XPIncrease,
		TwinBee
	}

	private sealed class _Spawned_c__AnonStorey0
	{
		internal Vector3 endPosition;

		internal PowerUp _this;

		internal void __m__0()
		{
			this._this.transform.DOMove(this.endPosition, 2f, false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this._this.gameObject.SetActive(false);
			});
		}

		internal void __m__1()
		{
			this._this.gameObject.SetActive(false);
		}
	}

	public PowerUp.PowerUpType powerUpType;

	public AudioClip[] powerUpCollectSFX;

	private GameObject magnetInstance;

	public abstract string GetPowerUpInfoForLevel(int level);

	public abstract void ActivatePowerUp();

	public virtual void PowerUpCollected()
	{
		if (this.powerUpCollectSFX.Length > 0)
		{
			SoundManager.PlaySFXInArray(this.powerUpCollectSFX[UnityEngine.Random.Range(0, this.powerUpCollectSFX.Length)], base.transform.position, 1f);
		}
		if (this.powerUpType != PowerUp.PowerUpType.TwinBee)
		{
			EventManager.TriggerEventPowerUp("CollectPowerUp", this.powerUpType);
		}
		base.CancelInvoke();
		base.gameObject.SetActive(false);
	}

	public virtual void Spawned()
	{
		base.transform.DOKill(false);
		Vector3 endPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0f, -10f);
		float x = base.transform.position.x + (endPosition.x - base.transform.position.x) / 2f;
		base.transform.DOMove(new Vector3(x, 0f, base.transform.position.z + 2f), 0.5f, false).OnComplete(delegate
		{
			this.transform.DOMove(endPosition, 2f, false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this.gameObject.SetActive(false);
			});
		});
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 11)
		{
			this.magnetInstance = other.gameObject;
			base.transform.DOKill(false);
		}
	}

	private void Update()
	{
		if (this.magnetInstance != null)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.magnetInstance.transform.position, Time.deltaTime * 30f);
		}
	}
}
