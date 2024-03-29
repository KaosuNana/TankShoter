using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class BulletHoming : Bullet
{
	public GameObject enemyTarget;

	public GameObject rocket;

	private bool okToTargetEnemy;

	public ParticleSystem rocketParticleFX;

	private Vector3 lastKnownDirection;

	private Collider myCollider;

	public Collider homingRadarCollider;

	private void WaypointChange(int wayPointIndex)
	{
		if (wayPointIndex == 2)
		{
			this.okToTargetEnemy = true;
			this.homingRadarCollider.enabled = true;
			if (this.enemyTarget != null)
			{
				this.lastKnownDirection = (this.enemyTarget.transform.position - base.transform.position).normalized;
				this.lastKnownDirection.y = 0f;
				base.transform.DOKill(false);
				base.Invoke("DisableAll", 2f);
			}
		}
	}

	private void StartShootingPath()
	{
		this.okToTargetEnemy = false;
		Vector3[] path = new Vector3[]
		{
			new Vector3(UnityEngine.Random.Range(base.transform.position.x - 2f, base.transform.position.x + 2f), base.transform.position.y + 5f, base.transform.position.z + 3f),
			new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 12f),
			new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + 40f)
		};
		base.transform.DOPath(path, 0.75f, PathType.CatmullRom, PathMode.Full3D, 10, null).SetEase(Ease.InQuad).SetLookAt(0.1f, null, null).OnWaypointChange(new TweenCallback<int>(this.WaypointChange)).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
		this.rocket.transform.DOBlendableLocalRotateBy(new Vector3(0f, 0f, 360f), 0.25f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	public override void SetDamageAmount(int amount)
	{
		base.CancelInvoke("DisableAll");
		base.damage = amount;
		base.UpdateDamageValue();
		this.rocket.SetActive(true);
		this.rocketParticleFX.Play();
		this.myCollider.enabled = true;
		this.StartShootingPath();
	}

	public void LockEnemyTarget(GameObject input)
	{
		if (this.enemyTarget == null)
		{
			this.enemyTarget = input;
			if (this.okToTargetEnemy)
			{
				this.lastKnownDirection = (this.enemyTarget.transform.position - base.transform.position).normalized;
				base.transform.DOKill(false);
			}
		}
	}

	private void Update()
	{
		if (this.enemyTarget != null && this.okToTargetEnemy)
		{
			if (this.enemyTarget.gameObject.activeInHierarchy)
			{
				Vector3 position = this.enemyTarget.transform.position;
				position.y = Mathf.Clamp(position.y, 1f, position.y);
				this.lastKnownDirection = (position - base.transform.position).normalized;
				if (this.lastKnownDirection != Vector3.zero)
				{
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.LookRotation(this.lastKnownDirection), 45f * Time.deltaTime);
				}
			}
			base.transform.Translate(this.lastKnownDirection * Time.deltaTime * 37.5f);
		}
	}

	private void OnEnable()
	{
		this.homingRadarCollider.enabled = false;
		this.myCollider = base.GetComponent<Collider>();
		this.myCollider.enabled = true;
	}

	private void OnDisable()
	{
		this.enemyTarget = null;
	}

	public override void AfterHitEnemy()
	{
		this.rocketParticleFX.Stop();
		if (this.impactSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.impactArraySFX[UnityEngine.Random.Range(0, this.impactSFXLength)], base.transform.position, 0.5f);
		}
		this.myCollider.enabled = false;
		this.rocket.SetActive(false);
		base.transform.DOKill(false);
		this.homingRadarCollider.enabled = false;
		this.enemyTarget = null;
		base.Invoke("DisableAll", 2f);
	}

	private void DisableAll()
	{
		base.gameObject.SetActive(false);
	}
}
