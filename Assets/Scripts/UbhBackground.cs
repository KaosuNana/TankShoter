using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhBackground : UbhMonoBehaviour
{
	private const string TEX_OFFSET_PROPERTY = "_MainTex";

	[FormerlySerializedAs("_Speed"), SerializeField]
	private float m_speed = 0.1f;

	private Vector2 m_offset = UbhUtil.VECTOR2_ZERO;

	private void Start()
	{
		UbhGameManager ubhGameManager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		if (ubhGameManager != null && ubhGameManager.m_scaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(UbhUtil.VECTOR2_ONE);
			Vector2 v = a * 2f;
			base.transform.localScale = v;
		}
	}

	private void Update()
	{
		float y = Mathf.Repeat(Time.time * this.m_speed, 1f);
		this.m_offset.x = 0f;
		this.m_offset.y = y;
		base.renderer.sharedMaterial.SetTextureOffset("_MainTex", this.m_offset);
	}
}
