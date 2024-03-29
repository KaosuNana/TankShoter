using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class UbhTimer : UbhSingletonMonoBehavior<UbhTimer>
{
	private const float FPS_60_DELTA = 0.0166666675f;

	private float m_deltaTime;

	private float m_deltaFrameCount;

	private float m_frameCount;

	private bool m_pausing;

	public float deltaTime
	{
		get
		{
			return (!this.m_pausing) ? this.m_deltaTime : 0f;
		}
	}

	public float deltaFrameCount
	{
		get
		{
			return (!this.m_pausing) ? this.m_deltaFrameCount : 0f;
		}
	}

	public float frameCount
	{
		get
		{
			return this.m_frameCount;
		}
	}

	public bool pausing
	{
		get
		{
			return this.m_pausing;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		this.UpdateTimes();
	}

	private void Update()
	{
		this.UpdateTimes();
		UbhSingletonMonoBehavior<UbhBulletManager>.instance.UpdateBullets();
	}

	private void UpdateTimes()
	{
		this.m_deltaTime = Time.deltaTime;
		this.m_deltaFrameCount = this.m_deltaTime / 0.0166666675f;
		if (!this.m_pausing)
		{
			this.m_frameCount += this.m_deltaFrameCount;
		}
	}

	public void Pause()
	{
		this.m_pausing = true;
	}

	public void Resume()
	{
		this.m_pausing = false;
	}
}
