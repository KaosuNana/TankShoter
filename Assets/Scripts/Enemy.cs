using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public enum NormalType
	{
		Enemy01,
		Enemy02,
		Enemy03,
		Enemy04,
		EnemyAngry,
		EnemyBomb,
		EnemyCheeky,
		EnemyDrop,
		EnemyGalaga,
		EnemyJumpy,
		EnemyPortal,
		EnemyShoot,
		EnemySway,
		EnemySweep,
		EnemySweepRound,
		EnemyGalagaBonus,
		EnemyLazerGateLong,
		EnemyLazerGateShort,
		EnemyLazerGateShortRight,
		EnemyEmpty,
		EnemyShootTarget
	}

	public enum BossType
	{
		Boss01,
		Boss02,
		Boss03,
		Boss04,
		Boss05,
		Boss06,
		Boss07,
		Boss08,
		Boss09,
		Boss10,
		Boss11,
		Boss12,
		Boss13,
		Boss14,
		Boss15,
		Boss16,
		Boss17,
		Boss18,
		Boss19,
		Boss20,
		Boss21,
		Boss22,
		Boss23
	}

	public ParticleSystem destroyedParticle;

	public AudioClip[] destroyedArraySFX;

	public HealthBarController healthBarPrefab;

	public int currentHealth;

	private Vector3 _localPos_k__BackingField;

	private bool isMoving;

	public Renderer myRenderer;

	protected float currentShootCountUp;

	public float enemySpeed = 6f;

	public int scoreValue;

	protected bool shootable;

	protected HealthBarController healthBarInstance;

	protected float currentShootRate;

	private WaveManager _waveParentInstance_k__BackingField;

	private CoinController _coinPrefab_k__BackingField;

	public int maxHealth;

	protected int enemyHP;

	public float powerUpDropProbability;

	public Vector3 healthBarOffset;

	protected bool shouldBlinkWhenShot = true;

	protected bool destroyed;

	protected bool firstShot;

	protected float currentEnemySpeed;

	protected int destroyedSFXLength;

	public Vector3 localPos
	{
		get;
		set;
	}

	public WaveManager waveParentInstance
	{
		get;
		set;
	}

	public CoinController coinPrefab
	{
		get;
		set;
	}

	private void OnEnable()
	{
		this.destroyed = false;
		this.firstShot = false;
		this.shootable = false;
		this.SetUpInitialHP();
		this.ExtraFunctionOnEnable();
		this.currentEnemySpeed = this.enemySpeed;
	}

	private void OnDisable()
	{
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.gameObject.SetActive(false);
		}
		base.CancelInvoke();
	}

	private void Awake()
	{
		this.destroyedSFXLength = this.destroyedArraySFX.Length;
		this.OnAwakeFunction();
	}

	private void Start()
	{
		this.OnStartFunction();
	}

	protected void UpdateHealthBarPosition()
	{
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + this.healthBarOffset.y, base.transform.position.z + this.healthBarOffset.z);
		}
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - Time.deltaTime * this.currentEnemySpeed);
		this.UpdateHealthBarPosition();
	}

	public abstract void SetInitialPosition(Vector3 localPos, bool shouldFlyDown);

	protected abstract void ExtraFunctionOnEnable();

	protected abstract void SpecialEffectOnFirstHit();

	protected abstract void OnEnemyDestroyed();

	protected abstract void OnStartFunction();

	protected abstract void OnAwakeFunction();

	protected virtual void OnEnterEnemyManagerCollider()
	{
	}

	protected virtual void OnExitEnemyManagerCollider()
	{
	}

	public virtual void DestroyEnemy()
	{
		if (this.destroyed)
		{
			return;
		}
		if (this.destroyedParticle != null)
		{
			UnityEngine.Object.Instantiate<ParticleSystem>(this.destroyedParticle, base.transform.position, Quaternion.identity);
		}
		if (this.destroyedSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.destroyedArraySFX[UnityEngine.Random.Range(0, this.destroyedSFXLength)], base.transform.position, 1f);
		}
		this.OnEnemyDestroyed();
		EventManager.TriggerEvent("ShootTotal");
		this.destroyed = true;
		Vector3 position = base.transform.position;
		position.y = 0.4f;
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.gameObject.SetActive(false);
		}
		float num = UnityEngine.Random.Range(0f, 1f);
		if (num < 1f - this.powerUpDropProbability)
		{
			GameManager.SpawnCoin(position);
		}
		else
		{
			GameManager.SpawnPowerUp(base.transform.position);
		}
		if (this.waveParentInstance != null)
		{
			this.waveParentInstance.EnemyGotDestroyed(this.scoreValue, base.transform.position);
		}
		base.gameObject.SetActive(false);
	}

	public int GetCurrentEnemyHP()
	{
		return this.currentHealth;
	}

	protected virtual bool ShouldDestroyEnemy()
	{
		return true;
	}

	public void ApplyDamage(int bulletDamage, string damageValue)
	{
		this.currentHealth -= bulletDamage;
		GameManager.ShowValue(damageValue, base.transform.position + this.healthBarOffset, ExtraScoreController.ScoreType.Damage);
		if (this.healthBarInstance != null)
		{
			this.healthBarInstance.SetHealthBarTo((float)this.currentHealth / (float)this.enemyHP);
		}
		if (this.currentHealth <= 0 && this.ShouldDestroyEnemy())
		{
			this.DestroyEnemy();
		}
	}

	protected virtual void SetUpInitialHP()
	{
		this.enemyHP = this.maxHealth + (int)((float)this.maxHealth * 0.75f * Mathf.Floor((float)(GameManager.CurrentLevel / 2)));
		this.currentHealth = this.enemyHP;
	}

	protected virtual void SetupHealthBar()
	{
		this.healthBarInstance = UnityEngine.Object.Instantiate<HealthBarController>(this.healthBarPrefab);
		this.healthBarInstance.transform.SetParent(GameManager.WorldCanvas.transform, true);
		this.healthBarInstance.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z - this.healthBarOffset.z);
		this.healthBarInstance.gameObject.SetActive(false);
	}

	protected void ActionWhenHitByBullet(Collider other)
	{
		if (this.shouldBlinkWhenShot)
		{
			this.myRenderer.material.DOColor(Color.white, "_EmissionColor", 0.05f).OnComplete(delegate
			{
				this.myRenderer.material.DOColor(Color.black, "_EmissionColor", 0.05f);
			});
		}
		other.gameObject.GetComponent<Bullet>().AfterHitEnemy();
		if (!this.firstShot)
		{
			this.firstShot = true;
			this.SetUpInitialHP();
			if (this.healthBarInstance != null)
			{
				this.healthBarInstance.gameObject.SetActive(true);
			}
			else
			{
				this.SetupHealthBar();
				this.healthBarInstance.gameObject.SetActive(true);
			}
			this.healthBarInstance.ResetHealthBar();
			this.SpecialEffectOnFirstHit();
		}
		int damage = other.gameObject.GetComponent<Bullet>().damage;
		this.ApplyDamage(damage, other.gameObject.GetComponent<Bullet>().damageValue);
	}

	private void OnTriggerStay(Collider other)
	{
		int layer = other.gameObject.layer;
		if (layer == 20 || layer == 24 || layer == 27)
		{
			this.currentShootRate += Time.deltaTime;
			if (this.currentShootRate >= other.gameObject.GetComponent<Bullet>().shootRate)
			{
				this.currentShootRate = 0f;
				if (this.shootable)
				{
					this.ActionWhenHitByBullet(other);
				}
			}
		}
	}

	protected virtual void OnTriggerEnemyTriggerDisabler()
	{
		base.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		int layer = other.gameObject.layer;
		if (layer == 20 || layer == 24 || layer == 27)
		{
			if (this.shootable)
			{
				this.ActionWhenHitByBullet(other);
			}
		}
		else if (layer == 12)
		{
			this.OnTriggerEnemyTriggerDisabler();
		}
		else if (layer == 15)
		{
			this.OnEnterEnemyManagerCollider();
			this.shootable = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == 15)
		{
			this.OnExitEnemyManagerCollider();
			this.shootable = false;
		}
	}
}
