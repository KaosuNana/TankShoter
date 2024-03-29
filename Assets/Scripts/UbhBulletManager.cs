using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class UbhBulletManager : UbhSingletonMonoBehavior<UbhBulletManager>
{
	private List<UbhBullet> m_bulletList = new List<UbhBullet>(2048);

	public int activeBulletCount
	{
		get
		{
			return this.m_bulletList.Count;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (UbhSingletonMonoBehavior<UbhTimer>.instance == null)
		{
		}
	}

	public void UpdateBullets()
	{
		for (int i = this.m_bulletList.Count - 1; i >= 0; i--)
		{
			if (this.m_bulletList[i] == null)
			{
				this.m_bulletList.RemoveAt(i);
			}
			else
			{
				this.m_bulletList[i].UpdateMove();
			}
		}
	}

	public void AddBullet(UbhBullet bullet)
	{
		if (this.m_bulletList.Contains(bullet))
		{
			UnityEngine.Debug.LogWarning("This bullet is already added in m_bulletList.");
			return;
		}
		this.m_bulletList.Add(bullet);
	}

	public void RemoveBullet(UbhBullet bullet)
	{
		if (!this.m_bulletList.Contains(bullet))
		{
			UnityEngine.Debug.LogWarning("This bullet is not found in m_bulletList.");
			return;
		}
		this.m_bulletList.Remove(bullet);
	}
}
