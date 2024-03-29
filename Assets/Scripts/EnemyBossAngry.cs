using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBossAngry : EnemyBoss
{
	public EnemyAngryChargeController enemyAngryChargePrefab;

	private EnemyAngryChargeController enemyAngryChargeInstance;

	protected override void OnEnemyDestroyed()
	{
		GameManager.BossDestroyed();
		if (this.enemyAngryChargeInstance != null)
		{
			this.enemyAngryChargeInstance.TurnOff();
		}
	}

	protected override void ExtraFunctionOnEnable()
	{
		if (this.enemyAngryChargeInstance == null)
		{
			this.enemyAngryChargeInstance = UnityEngine.Object.Instantiate<EnemyAngryChargeController>(this.enemyAngryChargePrefab);
			this.enemyAngryChargeInstance.gameObject.SetActive(false);
			this.enemyAngryChargeInstance.transform.SetParent(GameManager.WorldCanvas.transform, true);
			this.enemyAngryChargeInstance.transform.localScale = Vector3.one * 1.5f;
		}
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		this.bossSequence = DOTween.Sequence().OnComplete(delegate
		{
			this.ExtraFunctionOnEnable();
		}).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(0f, base.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.Fast)).AppendInterval(0.5f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(1.5f).Append(base.transform.DOShakeRotation(1f, 30f, 200, 90f, true)).Insert(3f, this.myRenderer.material.DOColor(Color.red, 1f)).InsertCallback(3f, delegate
		{
			this.enemyAngryChargeInstance.gameObject.SetActive(true);
			this.enemyAngryChargeInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + this.healthBarOffset.z);
			this.enemyAngryChargeInstance.AnimatingChargeImage();
		}).Append(base.transform.DOMoveZ(-2f, 0.8f, false)).InsertCallback(4f, delegate
		{
			this.enemyAngryChargeInstance.TurnOff();
		}).AppendInterval(1f).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(6f, this.myRenderer.material.DOColor(Color.white, 0.5f)).AppendInterval(1f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).Append(base.transform.DORotate(new Vector3(0f, -360f, 0f), 2.5f, RotateMode.WorldAxisAdd)).Append(base.transform.DOShakeRotation(1f, 30f, 200, 90f, true)).Insert(10f, this.myRenderer.material.DOColor(Color.red, 1f)).InsertCallback(10f, delegate
		{
			this.enemyAngryChargeInstance.gameObject.SetActive(true);
			this.enemyAngryChargeInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + this.healthBarOffset.z);
			this.enemyAngryChargeInstance.AnimatingChargeImage();
		}).Append(base.transform.DOMoveZ(-2f, 0.8f, false)).AppendInterval(1f).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(13f, this.myRenderer.material.DOColor(Color.white, 0.5f));
	}
}
