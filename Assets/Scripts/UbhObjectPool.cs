using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UniBulletHell/Manager/Object Pool"), DisallowMultipleComponent]
public sealed class UbhObjectPool : UbhSingletonMonoBehavior<UbhObjectPool>
{
	[Serializable]
	private class InitializePool
	{
		public GameObject m_bulletPrefab;

		public int m_initialPoolNum;
	}

	private class PoolingParam
	{
		public List<UbhBullet> m_bulletList = new List<UbhBullet>(1024);

		public int m_searchStartIndex;
	}

	[SerializeField]
	private List<UbhObjectPool.InitializePool> m_initializePoolList;

	private Dictionary<int, UbhObjectPool.PoolingParam> m_pooledBulletDic = new Dictionary<int, UbhObjectPool.PoolingParam>(256);

	protected override void Awake()
	{
		base.Awake();
		base.transform.hierarchyCapacity = 2048;
		if (this.m_initializePoolList != null && this.m_initializePoolList.Count > 0)
		{
			for (int i = 0; i < this.m_initializePoolList.Count; i++)
			{
				this.CreatePool(this.m_initializePoolList[i].m_bulletPrefab, this.m_initializePoolList[i].m_initialPoolNum);
			}
		}
	}

	public void CreatePool(GameObject goPrefab, int createNum)
	{
		for (int i = 0; i < createNum; i++)
		{
			UbhBullet bullet = this.GetBullet(goPrefab, UbhUtil.VECTOR3_ZERO, true);
			if (bullet == null)
			{
				break;
			}
			this.ReleaseBullet(bullet, false);
		}
	}

	public void RemovePool(GameObject goPrefab)
	{
		int instanceID = goPrefab.GetInstanceID();
		if (!this.m_pooledBulletDic.ContainsKey(instanceID))
		{
			return;
		}
		UbhObjectPool.PoolingParam poolingParam = this.m_pooledBulletDic[instanceID];
		poolingParam.m_searchStartIndex = 0;
		for (int i = 0; i < poolingParam.m_bulletList.Count; i++)
		{
			UnityEngine.Object.Destroy(poolingParam.m_bulletList[i].gameObject);
			UnityEngine.Object.Destroy(poolingParam.m_bulletList[i]);
			poolingParam.m_bulletList[i] = null;
		}
		poolingParam.m_bulletList.Clear();
	}

	public UbhBullet GetBullet(GameObject goPrefab, Vector3 position, bool forceInstantiate = false)
	{
		if (goPrefab == null)
		{
			return null;
		}
		UbhBullet ubhBullet = null;
		int instanceID = goPrefab.GetInstanceID();
		if (!this.m_pooledBulletDic.ContainsKey(instanceID))
		{
			this.m_pooledBulletDic.Add(instanceID, new UbhObjectPool.PoolingParam());
		}
		UbhObjectPool.PoolingParam poolingParam = this.m_pooledBulletDic[instanceID];
		if (!forceInstantiate && poolingParam.m_bulletList.Count > 0)
		{
			if (poolingParam.m_searchStartIndex < 0 || poolingParam.m_searchStartIndex >= poolingParam.m_bulletList.Count)
			{
				poolingParam.m_searchStartIndex = poolingParam.m_bulletList.Count - 1;
			}
			for (int i = poolingParam.m_searchStartIndex; i >= 0; i--)
			{
				if (poolingParam.m_bulletList[i] == null || poolingParam.m_bulletList[i].gameObject == null)
				{
					poolingParam.m_bulletList.RemoveAt(i);
				}
				else if (!poolingParam.m_bulletList[i].gameObject.activeSelf)
				{
					poolingParam.m_searchStartIndex = i - 1;
					ubhBullet = poolingParam.m_bulletList[i];
					ubhBullet.gameObject.SetActive(true);
					break;
				}
			}
			if (ubhBullet == null)
			{
				for (int j = poolingParam.m_bulletList.Count - 1; j > poolingParam.m_searchStartIndex; j--)
				{
					if (poolingParam.m_bulletList[j] == null || poolingParam.m_bulletList[j].gameObject == null)
					{
						poolingParam.m_bulletList.RemoveAt(j);
					}
					else if (j < poolingParam.m_bulletList.Count && !poolingParam.m_bulletList[j].gameObject.activeSelf)
					{
						poolingParam.m_searchStartIndex = j - 1;
						ubhBullet = poolingParam.m_bulletList[j];
						ubhBullet.gameObject.SetActive(true);
						break;
					}
				}
			}
		}
		if (ubhBullet == null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(goPrefab, base.transform);
			ubhBullet = gameObject.GetComponent<UbhBullet>();
			if (ubhBullet == null)
			{
				ubhBullet = gameObject.AddComponent<UbhBullet>();
			}
			poolingParam.m_bulletList.Add(ubhBullet);
			poolingParam.m_searchStartIndex = poolingParam.m_bulletList.Count - 1;
		}
		ubhBullet.transform.position = position;
		ubhBullet.transform.rotation = UbhUtil.QUATERNION_IDENTITY;
		UbhSingletonMonoBehavior<UbhBulletManager>.instance.AddBullet(ubhBullet);
		return ubhBullet;
	}

	public void ReleaseBullet(UbhBullet bullet, bool destroy = false)
	{
		if (bullet == null || bullet.gameObject == null)
		{
			return;
		}
		UbhSingletonMonoBehavior<UbhBulletManager>.instance.RemoveBullet(bullet);
		if (destroy)
		{
			UnityEngine.Object.Destroy(bullet.gameObject);
			UnityEngine.Object.Destroy(bullet);
			bullet = null;
			return;
		}
		bullet.gameObject.SetActive(false);
	}

	public List<UbhBullet> GetActiveBulletsList(GameObject goPrefab)
	{
		int instanceID = goPrefab.GetInstanceID();
		List<UbhBullet> list = null;
		if (this.m_pooledBulletDic.ContainsKey(instanceID))
		{
			UbhObjectPool.PoolingParam poolingParam = this.m_pooledBulletDic[instanceID];
			for (int i = 0; i < poolingParam.m_bulletList.Count; i++)
			{
				UbhBullet ubhBullet = poolingParam.m_bulletList[i];
				if (ubhBullet != null && ubhBullet.gameObject != null && ubhBullet.gameObject.activeSelf)
				{
					if (list == null)
					{
						list = new List<UbhBullet>(1024);
					}
					list.Add(poolingParam.m_bulletList[i]);
				}
			}
		}
		return list;
	}
}
