using DG.Tweening;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	public enum PlayerMode
	{
		Normal,
		Giant,
		Rocket,
		Magnet
	}

	public enum InputMode
	{
		Relative,
		Track
	}

	public VehicleManager vehicleManagerInstance;

	private Vehicle vehicleInstance;

	public int shieldDamage;

	public float shieldShootRate;

	public HealthBarController healthBarInstance;

	public GameObject runningParticle;

	public GameObject magnetParticle;

	public ParticleSystem chargingParticle;

	public ParticleSystem hpPickUpParticle;

	public ParticleSystem xpPickUpParticle;

	public ParticleSystem rapidPickUpParticle;

	public GameObject shieldObject;

	public Renderer[] shieldRenderer;

	public GameObject magnetRadar;

	public float movementRange;

	public float horizontalSpeed;

	public float verticalSpeed;

	public float minX;

	public float maxX;

	public float minZ;

	public float maxZ = 25f;

	private float forwardSpeed;

	private Vector2 initPos;

	private Vector2 finalPos;

	private Vector2 startJoystick;

	private float initialPlayerXPos;

	private float initialPlayerZPos;

	private bool isJoystickStarted;

	private bool isTouchedButton;

	public AudioSource playerAudioSource;

	public AudioSource powerUpAudioSource;

	private int _currentForwardSpeed_k__BackingField;

	private bool isInvincible;

	private bool _isMagnetMode_k__BackingField;

	private bool _isOnPowerUp_k__BackingField;

	public AudioClip hitSFX;

	public AudioClip burnSFX;

	public AudioClip[] magnetSFX;

	private bool playerArrived;

	private bool shouldContinueInMagnetMode;

	private float latestMagnetModeDuration;

	public bool shouldAutoShoot;

	private int gunFlag;

	private Tween speedTween;

	private float secondaryGunCharge;

	private float currentEnemyBulletShootRate;

	private int ground_layer_mask;

	public int currentForwardSpeed
	{
		get;
		set;
	}

	public bool isMagnetMode
	{
		get;
		set;
	}

	public bool isOnPowerUp
	{
		get;
		set;
	}

	private void Awake()
	{
		this.forwardSpeed = (float)this.currentForwardSpeed;
		this.isJoystickStarted = false;
		this.isOnPowerUp = false;
		SoundManager.SetPlayerAudioSource(this.playerAudioSource);
		SoundManager.SetPowerUpAudioSource(this.powerUpAudioSource);
		this.UpdateSensitivity();
		this.UpdateVehicle();
		this.shieldDamage = (int)((float)this.shieldDamage * GameManager.StrengthMultiplier);
		this.ground_layer_mask = LayerMask.GetMask(new string[]
		{
			"Ground"
		});
	}

	private void Start()
	{
		base.transform.position = new Vector3(0f, 0f, -20f);
		float delay = 0.5f;
		if (!GameSingleton.IsRestarted)
		{
			delay = 2f;
		}
		this.playerArrived = true;
		base.transform.DOMoveZ(0f, 1f, false).SetDelay(delay);
	}

	public void ResetPlayerPosition()
	{
		base.transform.DOMove(Vector3.zero, 0.5f, false);
	}

	public float GetHPPercentage()
	{
		return this.healthBarInstance.healthBarImage.fillAmount;
	}

	private void UpdateVehicle()
	{
		this.vehicleInstance = this.vehicleManagerInstance.GetMainVehicle();
		this.vehicleInstance.transform.SetParent(base.transform, false);
		this.vehicleInstance.transform.position = Vector3.zero;
		this.vehicleInstance.UpdatePreferredGunForMainPlayer(true);
		this.vehicleInstance.SetHealthBar(this.healthBarInstance);
	}

	public void PlayerExitArenaWithDelay(float delay)
	{
		this.StopShooting();
		base.transform.DOMoveZ(40f, 1f, false).SetDelay(delay).OnComplete(delegate
		{
			base.transform.position = new Vector3(0f, 0f, -20f);
		});
	}

	public void PlayerEnter()
	{
		base.transform.DOMoveZ(0f, 1f, false);
	}

	public void PlayXPPickUp()
	{
		this.xpPickUpParticle.gameObject.SetActive(true);
		this.xpPickUpParticle.Play();
	}

	public void HealHP(float amount)
	{
		this.vehicleInstance.HealHP(amount);
		this.hpPickUpParticle.gameObject.SetActive(true);
		this.hpPickUpParticle.Play();
	}

	private void OnTriggerStay(Collider other)
	{
		if (GameManager.CurrentState != GameManager.GameState.PlayState)
		{
			return;
		}
		if (other.gameObject.layer == 17 && !this.isInvincible)
		{
			this.currentEnemyBulletShootRate += Time.deltaTime;
			if (this.currentEnemyBulletShootRate >= other.gameObject.GetComponent<BulletEnemy>().shootRate)
			{
				GameManager.HurtEffects();
				this.currentEnemyBulletShootRate = 0f;
				this.vehicleInstance.ApplyDamage(other.gameObject.GetComponent<BulletEnemy>().damage);
				if (this.vehicleInstance.currentHP <= 0)
				{
					this.PlayerDie();
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (GameManager.CurrentState != GameManager.GameState.PlayState)
		{
			return;
		}
		int layer = other.gameObject.layer;
		if (layer == 9 || layer == 25)
		{
			if (!this.isInvincible)
			{
				GameManager.HurtEffects();
				if (other.gameObject.GetComponent<Enemy>() != null)
				{
					int currentHP = this.vehicleInstance.currentHP;
					int currentEnemyHP = other.gameObject.GetComponent<Enemy>().GetCurrentEnemyHP();
					if (currentEnemyHP > 0)
					{
						this.vehicleInstance.ApplyDamage(currentEnemyHP);
					}
					other.gameObject.GetComponent<Enemy>().ApplyDamage(currentHP, GameManager.CurrencyToString((float)currentHP / 100f));
				}
				if (this.vehicleInstance.currentHP <= 0)
				{
					this.PlayerDie();
				}
			}
		}
		else if (layer == 17)
		{
			if (!this.isInvincible)
			{
				GameManager.HurtEffects();
				this.vehicleInstance.ApplyDamage(other.gameObject.GetComponent<BulletEnemy>().damage);
				other.gameObject.GetComponent<BulletEnemy>().AfterHitPlayer();
				if (this.vehicleInstance.currentHP <= 0)
				{
					this.PlayerDie();
				}
			}
		}
		else if (layer == 19)
		{
			other.gameObject.GetComponent<PowerUp>().ActivatePowerUp();
			other.gameObject.GetComponent<PowerUp>().PowerUpCollected();
		}
	}

	private void DeactivateInvicible()
	{
		this.vehicleInstance.StopBlinking();
		this.isInvincible = false;
	}

	private void SetInvincibleFor(float duration)
	{
		base.CancelInvoke("DeactivateInvicible");
		this.isInvincible = true;
		this.vehicleInstance.StopBlinking();
		this.vehicleInstance.BlinkVehicle();
		base.Invoke("DeactivateInvicible", duration);
	}

	public void ActivateShieldMode(float duration)
	{
		base.CancelInvoke("DeactivateShieldMode");
		this.vehicleInstance.StopBlinking();
		base.CancelInvoke("BlinkVehicle");
		this.isInvincible = true;
		this.shieldObject.SetActive(true);
		this.shieldObject.transform.localScale = Vector3.zero;
		this.shieldObject.GetComponent<BulletShieldBall>().SetDamageAmount((int)((float)this.shieldDamage * (1f + 0.1f * (float)this.vehicleInstance.vehicleInfoInstance.attackLevel)));
		this.shieldObject.GetComponent<BulletShieldBall>().shootRate = this.shieldShootRate;
		this.shieldObject.transform.DOScale(Vector3.one, 0.5f);
		base.Invoke("BlinkVehicle", duration - 2f);
		base.Invoke("DeactivateShieldMode", duration - 0.5f);
	}

	private void BlinkVehicle()
	{
		this.vehicleInstance.BlinkVehicle();
	}

	public void TurnOffInvincible()
	{
		this.isInvincible = false;
	}

	private void DeactivateShieldMode()
	{
		this.shieldObject.transform.DOKill(false);
		this.shieldObject.transform.DOScale(Vector3.zero, 0.5f).OnComplete(delegate
		{
			this.shieldObject.SetActive(false);
		});
	}

	public void ActivateRapidMode(float duration)
	{
		this.vehicleInstance.ActivateRapidMode(duration);
	}

	public void ActivateMagnetMode(float duration)
	{
		base.CancelInvoke("DeactivateMagnetMode");
		SoundManager.PlaySFXInArray(this.magnetSFX[0], base.transform.position, 0.5f);
		SoundManager.PlayPowerUpLoopSFX(this.magnetSFX[1], 0.5f);
		this.isMagnetMode = true;
		this.isOnPowerUp = true;
		this.magnetRadar.SetActive(true);
		this.magnetParticle.GetComponent<ParticleSystem>().Play();
		base.Invoke("DeactivateMagnetMode", duration);
	}

	private void DeactivateMagnetParticleFX()
	{
		this.magnetParticle.GetComponent<ParticleSystem>().Stop();
	}

	public void DeactivateMagnetMode()
	{
		this.isOnPowerUp = false;
		this.isMagnetMode = false;
		this.magnetRadar.SetActive(false);
		this.DeactivateMagnetParticleFX();
		SoundManager.StopPowerUpLoopSFX();
		SoundManager.PlaySFXInArray(this.magnetSFX[2], base.transform.position, 0.5f);
	}

	private void StartRunning()
	{
		this.isInvincible = false;
		this.isOnPowerUp = false;
		this.forwardSpeed = (float)this.currentForwardSpeed;
	}

	public void RespawnAtPosition(Vector3 pos)
	{
		base.transform.position = pos;
	}

	public void PrepareToContinueGame()
	{
		base.transform.position = Vector3.zero;
		this.vehicleInstance.RessurectVehicle();
		EventManager.TriggerEvent("ContinueTotal");
		if (this.shouldContinueInMagnetMode)
		{
			this.shouldContinueInMagnetMode = false;
		}
		this.SetInvincibleFor(3f);
		this.forwardSpeed = (float)this.currentForwardSpeed;
	}

	public void ContinueGame()
	{
	}

	public void UpdateSensitivity()
	{
		this.movementRange = 100f / PlayerPrefs.GetFloat("JoystickSensitivity", 0.8f);
	}

	private void PlayerDie()
	{
		SoundManager.StopPlayerLoopSFX();
		this.StopShooting();
		this.vehicleInstance.StopReleasingSecondaryCharge();
		SoundManager.PlaySFXInArray(this.hitSFX, base.transform.position, 1f);
		UnityEngine.Object.Instantiate<GameObject>(this.vehicleInstance.explodeParticlePrefab.gameObject, base.transform.position, Quaternion.identity);
		this.vehicleInstance.DestroyVehicle();
		GameManager.SetGameOver();
		if (this.isMagnetMode)
		{
			base.CancelInvoke();
			this.DeactivateMagnetParticleFX();
			this.DeactivateMagnetMode();
			this.shouldContinueInMagnetMode = true;
		}
		this.runningParticle.GetComponent<ParticleSystem>().Stop();
	}

	private void Update()
	{
		if (GameManager.CurrentState == GameManager.GameState.MenuState)
		{
#if !UNITY_EDITOR
            for (int i = 0; i < UnityEngine.Input.touchCount; i++)
			{
				Touch touch = Input.touches[i];
				if (touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended && EventSystem.current.IsPointerOverGameObject(Input.touches[i].fingerId))
				{
					return;
				}
			}
			if (Input.GetMouseButton(0) && this.playerArrived)
			{
				base.transform.DOKill(false);
				GameManager.StartGame();
				base.transform.DORotate(Vector3.zero, 0.1f, RotateMode.Fast);
				this.forwardSpeed = (float)this.currentForwardSpeed;
			}
#endif

#if UNITY_EDITOR

                
            if (Input.GetMouseButton(0) && this.playerArrived)
            {
                base.transform.DOKill(false);
                GameManager.StartGame();
                base.transform.DORotate(Vector3.zero, 0.1f, RotateMode.Fast);
                this.forwardSpeed = (float)this.currentForwardSpeed;
            }
#endif

        }
        if (GameManager.CurrentState == GameManager.GameState.ContinueGameSuccessState && Input.GetMouseButton(0))
		{
			this.ContinueGame();
		}
		if (GameManager.CurrentState == GameManager.GameState.PlayState)
		{
			this.DetectTap();
			this.DetectTrackpad();
			return;
		}
		if (GameManager.CurrentState == GameManager.GameState.TutorialState)
		{
			this.DetectTap();
			return;
		}
	}

	private void StartShooting()
	{
		this.vehicleInstance.StartShooting();
	}

	private void StopShooting()
	{
		this.vehicleInstance.StopShooting();
	}

	private void StartCharging()
	{
		this.chargingParticle.Play();
	}

	private void StopCharging()
	{
		this.chargingParticle.Stop();
	}

	private void DetectTap()
	{
		if (UnityEngine.Input.touchCount > 0)
		{
			this.StartShooting();
		}
		else if (UnityEngine.Input.touchCount == 0)
		{
			this.StopShooting();
			if (GameManager.GetCurrentCharge() > 0.02f)
			{
				EventManager.TriggerEvent("ReleaseFingerToReleaseSPC");
				this.vehicleInstance.ReleaseSecondaryCharge(GameManager.GetCurrentCharge());
				GameManager.ResetCharge();
			}
		}

#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            this.StartShooting();
        }
        else if (!Input.GetMouseButton(0))
        {
            this.StopShooting();
            if (GameManager.GetCurrentCharge() > 0.02f)
            {
                EventManager.TriggerEvent("ReleaseFingerToReleaseSPC");
                this.vehicleInstance.ReleaseSecondaryCharge(GameManager.GetCurrentCharge());
                GameManager.ResetCharge();
            }
        }
#endif
    }

    private void LerpSpeedTo(float speed)
	{
		if (this.speedTween != null)
		{
			this.speedTween.Kill(false);
		}
		this.speedTween = DOTween.To(() => this.forwardSpeed, delegate(float x)
		{
			this.forwardSpeed = x;
		}, speed, 1f);
	}

	public void ResetJoystickStarted()
	{
		this.isJoystickStarted = false;
	}

	private void DetectTrackpad()
	{
#if !UNITY_EDITOR
        if (UnityEngine.Input.touchCount == 0)
		{
			this.isJoystickStarted = false;
		}
		if (UnityEngine.Input.touchCount > 0)
		{
			Vector2 position = Input.touches[0].position;
			Vector2 vector = new Vector2(position.x - this.startJoystick.x, position.y - this.startJoystick.y);
			float num = vector.x / this.movementRange;
			float num2 = vector.y / this.movementRange;
			float num3 = num * this.horizontalSpeed;
			float num4 = num2 * this.verticalSpeed;
			if (!this.isJoystickStarted)
			{
				this.startJoystick = Input.touches[0].position;
				this.isJoystickStarted = true;
				this.initialPlayerXPos = base.transform.position.x;
				this.initialPlayerZPos = base.transform.position.z;
				num3 = 0f;
				num4 = 0f;
			}
			if (this.isJoystickStarted)
			{
				base.transform.position = new Vector3(Mathf.Lerp(base.transform.position.x, Mathf.Clamp(this.initialPlayerXPos + num3, this.minX, this.maxX), 0.85f), base.transform.position.y, Mathf.Lerp(base.transform.position.z, Mathf.Clamp(this.initialPlayerZPos + num4, this.minZ, this.maxZ), 0.15f));
			}
		}
#endif

#if UNITY_EDITOR

        if (!Input.GetMouseButton(0))
        {
            this.isJoystickStarted = false;
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 position = Input.mousePosition;
            Vector2 vector = new Vector2(position.x - this.startJoystick.x, position.y - this.startJoystick.y);
            float num = vector.x / this.movementRange;
            float num2 = vector.y / this.movementRange;
            float num3 = num * this.horizontalSpeed;
            float num4 = num2 * this.verticalSpeed;
            if (!this.isJoystickStarted)
            {
                this.startJoystick = Input.mousePosition;
                this.isJoystickStarted = true;
                this.initialPlayerXPos = base.transform.position.x;
                this.initialPlayerZPos = base.transform.position.z;
                num3 = 0f;
                num4 = 0f;
            }
            if (this.isJoystickStarted)
            {
                base.transform.position = new Vector3(Mathf.Lerp(base.transform.position.x, Mathf.Clamp(this.initialPlayerXPos + num3, this.minX, this.maxX), 0.85f), base.transform.position.y, Mathf.Lerp(base.transform.position.z, Mathf.Clamp(this.initialPlayerZPos + num4, this.minZ, this.maxZ), 0.15f));
            }
        }
#endif
    }
}
