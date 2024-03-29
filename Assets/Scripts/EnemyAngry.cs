using DG.Tweening;
using System;
using UnityEngine;

public class EnemyAngry : EnemyNormal
{
	private bool isChargingForward;

	private Tween shakingTween;

	private Tween redTween;

	public ParticleSystem dustTrail;

	public EnemyAngryChargeController enemyAngryChargePrefab;

	private EnemyAngryChargeController enemyAngryChargeInstance;

	protected override void ExtraFunctionOnEnable()
	{
		this.dustTrail.gameObject.SetActive(false);
	}

	protected override void OnStartFunction()
	{
		this.currentEnemySpeed = this.enemySpeed;
	}

	private void ChargeForward()
	{
		if (this.enemyAngryChargeInstance == null)
		{
			this.enemyAngryChargeInstance = UnityEngine.Object.Instantiate<EnemyAngryChargeController>(this.enemyAngryChargePrefab);
			this.enemyAngryChargeInstance.transform.SetParent(GameManager.WorldCanvas.transform, true);
		}
		else
		{
			this.enemyAngryChargeInstance.gameObject.SetActive(true);
		}
		this.enemyAngryChargeInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + this.healthBarOffset.z);
		this.isChargingForward = true;
		this.shakingTween = base.transform.DOShakeRotation(2f, 30f, 200, 90f, true);
		this.enemyAngryChargeInstance.AnimatingChargeImage();
		this.redTween = this.myRenderer.material.DOColor(Color.red, 2f).OnComplete(delegate
		{
			this.currentEnemySpeed = 50f;
			this.dustTrail.gameObject.SetActive(true);
			this.isChargingForward = false;
		});
	}

	private void OnDisable()
	{
		base.CancelInvoke();
		if (this.shakingTween != null)
		{
			this.shakingTween.Kill(false);
		}
		if (this.redTween != null)
		{
			this.redTween.Kill(false);
		}
		base.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		this.myRenderer.material.color = Color.white;
		this.currentEnemySpeed = this.enemySpeed;
		this.isChargingForward = false;
		this.shootable = false;
		this.destroyed = false;
		this.firstShot = false;
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.gameObject.SetActive(false);
		}
		if (this.enemyAngryChargeInstance != null)
		{
			this.enemyAngryChargeInstance.TurnOff();
		}
	}

	protected override void SpecialEffectOnFirstHit()
	{
		this.shouldBlinkWhenShot = false;
		this.ChargeForward();
	}

	private void Update()
	{
		if (!this.isChargingForward)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - Time.deltaTime * this.currentEnemySpeed);
		}
		base.UpdateHealthBarPosition();
	}
}
