using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class UbhBaseShot : UbhMonoBehaviour
{
	[Header("===== Common Settings ====="), FormerlySerializedAs("_BulletPrefab")]
	public GameObject m_bulletPrefab;

	[FormerlySerializedAs("_BulletNum")]
	public int m_bulletNum = 10;

	[FormerlySerializedAs("_BulletSpeed")]
	public float m_bulletSpeed = 2f;

	[FormerlySerializedAs("_AccelerationSpeed")]
	public float m_accelerationSpeed;

	[FormerlySerializedAs("_AccelerationTurn")]
	public float m_accelerationTurn;

	[FormerlySerializedAs("_UsePauseAndResume")]
	public bool m_usePauseAndResume;

	[FormerlySerializedAs("_PauseTime")]
	public float m_pauseTime;

	[FormerlySerializedAs("_ResumeTime")]
	public float m_resumeTime;

	[FormerlySerializedAs("_UseAutoRelease")]
	public bool m_useAutoRelease;

	[FormerlySerializedAs("_AutoReleaseTime")]
	public float m_autoReleaseTime = 10f;

	[SerializeField, Space(10f)]
	private UnityEvent m_shotFiredCallbackEvents = new UnityEvent();

	[SerializeField]
	private UnityEvent m_shotFinishedCallbackEvents = new UnityEvent();

	protected bool m_shooting;

	private UbhShotCtrl m_shotCtrl;

	public UbhShotCtrl shotCtrl
	{
		get
		{
			if (this.m_shotCtrl == null)
			{
				this.m_shotCtrl = base.transform.GetComponentInParent<UbhShotCtrl>();
			}
			return this.m_shotCtrl;
		}
	}

	public bool shooting
	{
		get
		{
			return this.m_shooting;
		}
	}

	public virtual bool lockOnShot
	{
		get
		{
			return false;
		}
	}

	protected virtual void OnDisable()
	{
		this.m_shooting = false;
	}

	public abstract void Shot();

	public void SetShotCtrl(UbhShotCtrl shotCtrl)
	{
		this.m_shotCtrl = shotCtrl;
	}

	protected void FiredShot()
	{
		this.m_shotFiredCallbackEvents.Invoke();
	}

	public void StopShotCoroutine()
	{
		base.StopAllCoroutines();
		this.m_shooting = false;
	}

	protected void FinishedShot()
	{
		this.m_shotFinishedCallbackEvents.Invoke();
		this.m_shooting = false;
	}

	protected UbhBullet GetBullet(Vector3 position, bool forceInstantiate = false)
	{
		if (this.m_bulletPrefab == null)
		{
			return null;
		}
		UbhBullet bullet = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.m_bulletPrefab, position, forceInstantiate);
		if (bullet == null)
		{
			return null;
		}
		return bullet;
	}

	protected void ShotBullet(UbhBullet bullet, float speed, float angle, bool homing = false, Transform homingTarget = null, float homingAngleSpeed = 0f, bool wave = false, float waveSpeed = 0f, float waveRangeSize = 0f)
	{
		if (bullet == null)
		{
			return;
		}
		bullet.Shot(this, speed, angle, this.m_accelerationSpeed, this.m_accelerationTurn, homing, homingTarget, homingAngleSpeed, wave, waveSpeed, waveRangeSize, this.m_usePauseAndResume, this.m_pauseTime, this.m_resumeTime, this.m_useAutoRelease, this.m_autoReleaseTime, this.m_shotCtrl.m_axisMove, this.m_shotCtrl.m_inheritAngle);
	}
}
