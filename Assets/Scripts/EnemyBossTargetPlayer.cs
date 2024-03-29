using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBossTargetPlayer : EnemyBoss
{
	private sealed class _ExtraFunctionOnEnable_c__AnonStorey0
	{
		internal Vector3 playerPos;

		internal EnemyBossTargetPlayer _this;

		internal void __m__0()
		{
			this._this.ExtraFunctionOnEnable();
		}

		internal void __m__1()
		{
			this._this.SpawnEnemyWaves(0);
		}

		internal void __m__2()
		{
			this._this.shouldLookAtPlayerTarget = true;
		}

		internal void __m__3()
		{
			this._this.uniBulletShotControllerList[0].StartShotRoutine();
		}

		internal void __m__4()
		{
			this._this.uniBulletShotControllerList[1].StartShotRoutine();
		}

		internal void __m__5()
		{
			this._this.shouldLookAtPlayerTarget = false;
			this.playerPos = this._this.playerTarget.position;
			this._this.enemyAngryChargeInstance.gameObject.SetActive(true);
			this._this.enemyAngryChargeInstance.transform.position = new Vector3(this._this.transform.position.x, this._this.transform.position.y, this._this.transform.position.z + this._this.healthBarOffset.z);
			this._this.enemyAngryChargeInstance.transform.rotation = this._this.transform.rotation;
			this._this.enemyAngryChargeInstance.AnimatingChargeImage();
		}

		internal void __m__6()
		{
			this._this.MoveTo(this.playerPos);
		}
	}

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
		this.playerTarget = GameManager.Player.transform;
		Vector3 playerPos = this.playerTarget.position;
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		this.bossSequence = DOTween.Sequence().OnComplete(delegate
		{
			this.ExtraFunctionOnEnable();
		}).Append(base.transform.DOMove(new Vector3(0f, 0f, 20f), 1f, false)).Insert(0f, base.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.Fast)).AppendCallback(delegate
		{
			this.SpawnEnemyWaves(0);
		}).AppendInterval(2f).InsertCallback(1f, delegate
		{
			this.shouldLookAtPlayerTarget = true;
		}).AppendInterval(0.5f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(2f).Append(base.transform.DOShakeRotation(1f, 30f, 200, 90f, true)).Insert(5f, this.myRenderer.material.DOColor(Color.red, 1f)).InsertCallback(5f, delegate
		{
			this.shouldLookAtPlayerTarget = false;
			playerPos = this.playerTarget.position;
			this.enemyAngryChargeInstance.gameObject.SetActive(true);
			this.enemyAngryChargeInstance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + this.healthBarOffset.z);
			this.enemyAngryChargeInstance.transform.rotation = this.transform.rotation;
			this.enemyAngryChargeInstance.AnimatingChargeImage();
		}).AppendCallback(delegate
		{
			this.MoveTo(playerPos);
		}).AppendInterval(1f).Append(base.transform.DOMove(new Vector3(0f, 0f, 20f), 1f, false)).Insert(6f, this.myRenderer.material.DOColor(Color.white, 0.5f));
	}

	private void MoveTo(Vector3 pos)
	{
		base.transform.DOMove(pos, 0.5f, false);
	}
}
