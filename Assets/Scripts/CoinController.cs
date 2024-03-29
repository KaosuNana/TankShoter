using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoinController : MonoBehaviour
{
	private sealed class _Spawned_c__AnonStorey0
	{
		internal Vector3 endPosition;

		internal CoinController _this;

		internal void __m__0()
		{
			this._this.transform.DOMove(this.endPosition, UnityEngine.Random.Range(2f, 3f), false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this._this.transform.DOKill(false);
				this._this.gameObject.SetActive(false);
			});
		}

		internal void __m__1()
		{
			this._this.transform.DOKill(false);
			this._this.gameObject.SetActive(false);
		}
	}

	public int value;

	private GameObject magnetInstance;

	private bool isTaken;

	private Vector3 originalLocalPosition;

	public AudioClip[] randomCollectSFX;

	private void Awake()
	{
		this.isTaken = false;
		base.GetComponent<Collider>().isTrigger = true;
		this.originalLocalPosition = base.transform.localPosition;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (GameManager.CurrentState != GameManager.GameState.PlayState)
		{
			return;
		}
		if (other.gameObject.layer == 10)
		{
			this.Taken();
		}
		else if (other.gameObject.layer == 11)
		{
			this.magnetInstance = other.gameObject;
			base.transform.DOKill(false);
		}
	}

	private void OnEnable()
	{
		this.isTaken = false;
	}

	protected virtual void DeliverValueToGM()
	{
		GameManager.ExtraCoinFromGameplay(this.value, base.transform.position);
	}

	private void Taken()
	{
		if (this.isTaken)
		{
			return;
		}
		base.transform.DOKill(false);
		SoundManager.PlaySFXInArray(this.randomCollectSFX[UnityEngine.Random.Range(0, this.randomCollectSFX.Length)], base.transform.position, 1f);
		this.isTaken = true;
		if (this.magnetInstance != null)
		{
			EventManager.TriggerEvent("CollectCoinMagnetPowerUp");
			this.magnetInstance = null;
		}
		this.DeliverValueToGM();
		base.gameObject.SetActive(false);
	}

	public virtual void Spawned()
	{
		base.transform.DOKill(false);
		Vector3 endPosition = new Vector3(UnityEngine.Random.Range(-8f, 8f), 0f, (float)UnityEngine.Random.Range(-8, -10));
		float x = base.transform.position.x + (endPosition.x - base.transform.position.x) / 2f;
		base.transform.DOBlendableLocalRotateBy(new Vector3(0f, 720f, 0f), UnityEngine.Random.Range(1f, 3f), RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
		base.transform.DOMove(new Vector3(x, 0f, base.transform.position.z + (float)UnityEngine.Random.Range(2, 4)), 0.5f, false).OnComplete(delegate
		{
			this.transform.DOMove(endPosition, UnityEngine.Random.Range(2f, 3f), false).SetEase(Ease.InQuad).OnComplete(delegate
			{
				this.transform.DOKill(false);
				this.gameObject.SetActive(false);
			});
		});
	}

	public void ShowUpWithDelay(float delay)
	{
		base.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetDelay(delay);
	}

	public void ResetCoin()
	{
		base.transform.localScale = Vector3.one;
		base.transform.localPosition = this.originalLocalPosition;
		this.isTaken = false;
		this.magnetInstance = null;
	}

	private void Update()
	{
		if (this.magnetInstance != null && !this.isTaken)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.magnetInstance.transform.position, Time.deltaTime * 45f);
		}
	}
}
