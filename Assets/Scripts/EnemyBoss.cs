using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
	protected Transform playerTarget;

	protected bool shouldLookAtPlayerTarget;

	public int numberOfGrowAfterDestroyed;

	public int minCoinDrop = 30;

	public int maxCoinDrop = 35;

	protected Tween bossSequence;

	public UbhBaseShot[] m_shotObj;

	protected List<UbhShotCtrl> uniBulletShotControllerList = new List<UbhShotCtrl>();

	public bool shouldLoopShoot;

	public EnemyWaveManager[] enemyWavePrefab;

	protected int enemyWaveIndex;

	protected override void ExtraFunctionOnEnable()
	{
		base.transform.DOMove(new Vector3(0f, 0f, 20f), 1f, false);
	}

	protected void SpawnEnemyWaves()
	{
		this.SpawnEnemyWaves(this.enemyWaveIndex % this.enemyWavePrefab.Length);
		this.enemyWaveIndex++;
	}

	protected override void SpecialEffectOnFirstHit()
	{
	}

	protected override void OnEnemyDestroyed()
	{
		GameManager.BossDestroyed();
	}

	protected override void OnStartFunction()
	{
	}

	protected override void OnAwakeFunction()
	{
		for (int i = 0; i < this.m_shotObj.Length; i++)
		{
			this.uniBulletShotControllerList.Add(base.gameObject.AddComponent<UbhShotCtrl>());
			this.uniBulletShotControllerList[i].ResetToTankBuddiesDefault(this.shouldLoopShoot);
			this.uniBulletShotControllerList[i].SetShotPattern(this.m_shotObj[i], 0f);
		}
	}

	public override void SetInitialPosition(Vector3 localPos, bool shouldFlyDown)
	{
		base.transform.localPosition = localPos;
	}

	protected void SpawnEnemyWaves(int index)
	{
		EnemyWaveManager enemyWaveManager = UnityEngine.Object.Instantiate<EnemyWaveManager>(this.enemyWavePrefab[index]);
		enemyWaveManager.transform.position = new Vector3(0f, 0f, 24f);
		enemyWaveManager.SpawnEnemy(GameManager.instance.masterPoolManagerInstance, true);
	}

	private void Update()
	{
		base.UpdateHealthBarPosition();
		if (this.shouldLookAtPlayerTarget && this.playerTarget != null)
		{
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(this.playerTarget.position - base.transform.position), 50f * Time.deltaTime);
		}
	}

	protected virtual void ExtraBossActionWhenHitByBullet(GameObject other)
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 20 || other.gameObject.layer == 27)
		{
			if (this.shootable)
			{
				base.ActionWhenHitByBullet(other);
				this.ExtraBossActionWhenHitByBullet(other.gameObject);
			}
		}
		else if (other.gameObject.layer == 15)
		{
			this.shootable = true;
		}
	}

	private void GrowEnemy()
	{
		this.shootable = false;
		this.numberOfGrowAfterDestroyed--;
		this.bossSequence.Pause<Tween>();
		base.transform.DOShakeRotation(2f, 30f, 200, 90f, true).OnComplete(delegate
		{
			base.transform.DOBlendableScaleBy(Vector3.one * 2.5f, 1.5f).SetEase(Ease.InOutElastic).OnComplete(delegate
			{
				this.SetUpInitialHP();
				this.shootable = true;
				this.bossSequence.Play<Tween>();
			});
		});
	}

	protected override bool ShouldDestroyEnemy()
	{
		if (this.numberOfGrowAfterDestroyed > 0)
		{
			this.GrowEnemy();
			return false;
		}
		return true;
	}

	public override void DestroyEnemy()
	{
		if (this.destroyed)
		{
			return;
		}
		base.CancelInvoke();
		this.OnEnemyDestroyed();
		if (this.destroyedParticle != null)
		{
			UnityEngine.Object.Instantiate<ParticleSystem>(this.destroyedParticle, base.transform.position, Quaternion.identity);
		}
		if (this.destroyedSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.destroyedArraySFX[UnityEngine.Random.Range(0, this.destroyedSFXLength)], base.transform.position, 1f);
		}
		EventManager.TriggerEvent("ShootTotal");
		this.destroyed = true;
		Vector3 position = base.transform.position;
		position.y = 0.4f;
		int num = UnityEngine.Random.Range(this.minCoinDrop, this.maxCoinDrop);
		for (int i = 0; i < num; i++)
		{
			GameManager.SpawnCoin(position);
		}
		GameManager.SpawnPowerUp(base.transform.position);
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.gameObject.SetActive(false);
		}
		if (base.waveParentInstance != null)
		{
			base.waveParentInstance.EnemyGotDestroyed(this.scoreValue, base.transform.position);
		}
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		base.gameObject.SetActive(false);
	}
}
