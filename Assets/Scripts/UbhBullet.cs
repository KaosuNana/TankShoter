using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent]
public class UbhBullet : UbhMonoBehaviour
{
	private Transform m_transformCache;

	private UbhBaseShot m_parentBaseShot;

	private float m_speed;

	private float m_angle;

	private float m_accelSpeed;

	private float m_accelTurn;

	private bool m_homing;

	private Transform m_homingTarget;

	private float m_homingAngleSpeed;

	private bool m_wave;

	private float m_waveSpeed;

	private float m_waveRangeSize;

	private bool m_pauseAndResume;

	private float m_pauseTime;

	private float m_resumeTime;

	private bool m_useAutoRelease;

	private float m_autoReleaseTime;

	private UbhUtil.AXIS m_axisMove;

	private float m_baseAngle;

	private float m_selfFrameCnt;

	private float m_selfTimeCount;

	private UbhTentacleBullet m_tentacleBullet;

	private bool _shooting_k__BackingField;

	public bool shooting
	{
		get;
		private set;
	}

	public UbhBaseShot parentShot
	{
		get
		{
			return this.m_parentBaseShot;
		}
	}

	private void Awake()
	{
		this.m_transformCache = base.transform;
		this.m_tentacleBullet = base.GetComponent<UbhTentacleBullet>();
	}

	private void OnDisable()
	{
		this.m_parentBaseShot = null;
		this.m_homingTarget = null;
		this.m_transformCache.ResetPosition(false);
		this.m_transformCache.ResetRotation(false);
		this.shooting = false;
	}

	public void Shot(UbhBaseShot parentBaseShot, float speed, float angle, float accelSpeed, float accelTurn, bool homing, Transform homingTarget, float homingAngleSpeed, bool wave, float waveSpeed, float waveRangeSize, bool pauseAndResume, float pauseTime, float resumeTime, bool useAutoRelease, float autoReleaseTime, UbhUtil.AXIS axisMove, bool inheritAngle)
	{
		if (this.shooting)
		{
			return;
		}
		this.shooting = true;
		this.m_parentBaseShot = parentBaseShot;
		this.m_speed = speed;
		this.m_angle = angle;
		this.m_accelSpeed = accelSpeed;
		this.m_accelTurn = accelTurn;
		this.m_homing = homing;
		this.m_homingTarget = homingTarget;
		this.m_homingAngleSpeed = homingAngleSpeed;
		this.m_wave = wave;
		this.m_waveSpeed = waveSpeed;
		this.m_waveRangeSize = waveRangeSize;
		this.m_pauseAndResume = pauseAndResume;
		this.m_pauseTime = pauseTime;
		this.m_resumeTime = resumeTime;
		this.m_useAutoRelease = useAutoRelease;
		this.m_autoReleaseTime = autoReleaseTime;
		this.m_axisMove = axisMove;
		this.m_baseAngle = 0f;
		if (inheritAngle && !this.m_parentBaseShot.lockOnShot)
		{
			if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				this.m_baseAngle = this.m_parentBaseShot.shotCtrl.transform.eulerAngles.y;
			}
			else
			{
				this.m_baseAngle = this.m_parentBaseShot.shotCtrl.transform.eulerAngles.z;
			}
		}
		if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
		{
			this.m_transformCache.SetEulerAnglesY(this.m_baseAngle - this.m_angle);
		}
		else
		{
			this.m_transformCache.SetEulerAnglesZ(this.m_baseAngle + this.m_angle);
		}
		this.m_selfFrameCnt = 0f;
		this.m_selfTimeCount = 0f;
	}

	public void UpdateMove()
	{
		if (!this.shooting)
		{
			return;
		}
		float deltaTime = UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
		this.m_selfTimeCount += deltaTime;
		if (this.m_useAutoRelease && this.m_autoReleaseTime > 0f && this.m_selfTimeCount >= this.m_autoReleaseTime)
		{
			UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this, false);
			return;
		}
		if (this.m_pauseAndResume && this.m_pauseTime >= 0f && this.m_resumeTime > this.m_pauseTime && this.m_pauseTime <= this.m_selfTimeCount && this.m_selfTimeCount < this.m_resumeTime)
		{
			return;
		}
		if (this.m_homing)
		{
			if (this.m_homingTarget != null && 0f < this.m_homingAngleSpeed)
			{
				float angleFromTwoPosition = UbhUtil.GetAngleFromTwoPosition(this.m_transformCache, this.m_homingTarget, this.m_axisMove);
				float current;
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					current = -this.m_transformCache.eulerAngles.y;
				}
				else
				{
					current = this.m_transformCache.eulerAngles.z;
				}
				float num = Mathf.MoveTowardsAngle(current, angleFromTwoPosition, deltaTime * this.m_homingAngleSpeed);
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					this.m_transformCache.SetEulerAnglesY(-num);
				}
				else
				{
					this.m_transformCache.SetEulerAnglesZ(num);
				}
			}
		}
		else if (this.m_wave)
		{
			this.m_angle += this.m_accelTurn * deltaTime;
			if (0f < this.m_waveSpeed && 0f < this.m_waveRangeSize)
			{
				float num2 = this.m_angle + this.m_waveRangeSize / 2f * Mathf.Sin(this.m_selfFrameCnt * this.m_waveSpeed / 100f);
				if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
				{
					this.m_transformCache.SetEulerAnglesY(this.m_baseAngle - num2);
				}
				else
				{
					this.m_transformCache.SetEulerAnglesZ(this.m_baseAngle + num2);
				}
			}
			this.m_selfFrameCnt += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaFrameCount;
		}
		else
		{
			float num3 = this.m_accelTurn * deltaTime;
			if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
			{
				this.m_transformCache.Rotate(0f, -num3, 0f);
			}
			else
			{
				this.m_transformCache.Rotate(0f, 0f, num3);
			}
		}
		this.m_speed += this.m_accelSpeed * deltaTime;
		if (this.m_axisMove == UbhUtil.AXIS.X_AND_Z)
		{
			this.m_transformCache.position += this.m_transformCache.forward * this.m_speed * deltaTime;
		}
		else
		{
			this.m_transformCache.position += this.m_transformCache.up * this.m_speed * deltaTime;
		}
		if (this.m_tentacleBullet != null)
		{
			this.m_tentacleBullet.UpdateRotate();
		}
	}
}
