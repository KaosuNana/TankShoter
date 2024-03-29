using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhDestroyArea : UbhMonoBehaviour
{
	[FormerlySerializedAs("_UseCenterCollider"), SerializeField]
	private bool m_useCenterCollider;

	[FormerlySerializedAs("_ColCenter"), SerializeField]
	private BoxCollider2D m_colCenter;

	[FormerlySerializedAs("_ColTop"), SerializeField]
	private BoxCollider2D m_colTop;

	[FormerlySerializedAs("_ColBottom"), SerializeField]
	private BoxCollider2D m_colBottom;

	[FormerlySerializedAs("_ColRight"), SerializeField]
	private BoxCollider2D m_colRight;

	[FormerlySerializedAs("_ColLeft"), SerializeField]
	private BoxCollider2D m_colLeft;

	private void Start()
	{
		if (this.m_colCenter == null || this.m_colTop == null || this.m_colBottom == null || this.m_colRight == null || this.m_colLeft == null)
		{
			return;
		}
		UbhGameManager ubhGameManager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		if (ubhGameManager != null && ubhGameManager.m_scaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(UbhUtil.VECTOR2_ONE);
			Vector2 size = a * 2f;
			size.x += 0.5f;
			size.y += 0.5f;
			Vector2 vECTOR2_ZERO = UbhUtil.VECTOR2_ZERO;
			this.m_colCenter.size = size;
			this.m_colTop.size = size;
			vECTOR2_ZERO.x = this.m_colTop.offset.x;
			vECTOR2_ZERO.y = size.y;
			this.m_colTop.offset = vECTOR2_ZERO;
			this.m_colBottom.size = size;
			vECTOR2_ZERO.x = this.m_colBottom.offset.x;
			vECTOR2_ZERO.y = -size.y;
			this.m_colBottom.offset = vECTOR2_ZERO;
			Vector2 vECTOR2_ZERO2 = UbhUtil.VECTOR2_ZERO;
			vECTOR2_ZERO2.x = size.y;
			vECTOR2_ZERO2.y = size.x;
			this.m_colRight.size = vECTOR2_ZERO2;
			vECTOR2_ZERO.x = size.x / 2f + vECTOR2_ZERO2.x / 2f;
			vECTOR2_ZERO.y = this.m_colRight.offset.y;
			this.m_colRight.offset = vECTOR2_ZERO;
			this.m_colLeft.size = vECTOR2_ZERO2;
			vECTOR2_ZERO.x = -(size.x / 2f) - vECTOR2_ZERO2.x / 2f;
			vECTOR2_ZERO.y = this.m_colLeft.offset.y;
			this.m_colLeft.offset = vECTOR2_ZERO;
		}
		this.m_colCenter.enabled = this.m_useCenterCollider;
		this.m_colTop.enabled = !this.m_useCenterCollider;
		this.m_colBottom.enabled = !this.m_useCenterCollider;
		this.m_colRight.enabled = !this.m_useCenterCollider;
		this.m_colLeft.enabled = !this.m_useCenterCollider;
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (!this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerEnter(Collider c)
	{
		if (this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit(Collider c)
	{
		if (!this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void HitCheck(Transform colTrans)
	{
		string name = colTrans.name;
		if (name.Contains("EnemyBullet") || name.Contains("PlayerBullet"))
		{
			UbhBullet componentInParent = colTrans.parent.GetComponentInParent<UbhBullet>();
			if (componentInParent != null)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(componentInParent, false);
			}
		}
		else if (!name.Contains("Player"))
		{
			UnityEngine.Object.Destroy(colTrans.gameObject);
		}
	}
}
