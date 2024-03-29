using DG.Tweening;
using System;
using UnityEngine;

public class BulletEnemyLaserGridChildren : BulletEnemy
{
	public ParticleSystem glow;

	private MeshRenderer gridMesh;

	private Collider gridCollider;

	private Tween gridTween;

	private void OnEnable()
	{
		if (this.gridMesh == null)
		{
			this.gridMesh = base.GetComponent<MeshRenderer>();
		}
		if (this.gridCollider == null)
		{
			this.gridCollider = base.GetComponent<Collider>();
		}
		this.glow.Stop();
		base.GetComponent<Collider>().enabled = false;
		Color color = base.GetComponent<MeshRenderer>().material.color;
		color.a = 0f;
		base.GetComponent<MeshRenderer>().material.color = color;
	}

	public override void SetDamageAmount(int amount)
	{
		this.damage = amount + (int)((float)amount * 0.75f * Mathf.Floor((float)(GameManager.CurrentLevel / 2)));
	}

	public override void AfterHitPlayer()
	{
	}

	public void StartAppearing()
	{
		this.gridMesh.material.SetTextureOffset("_MainTex", new Vector2(0f, 0f));
		this.gridCollider.enabled = false;
		this.gridMesh.material.SetVector("_V_CW_MainTex_Scroll", new Vector4(0f, 0f, 1f, 1f));
		this.gridMesh.material.DOFade(1f, 0.5f);
		this.gridMesh.material.DOFade(0f, 0.45f).SetDelay(0.5f);
		this.gridMesh.material.DOFade(1f, 0.4f).SetDelay(0.95f);
		this.gridMesh.material.DOFade(0f, 0.35f).SetDelay(1.3f);
		this.gridMesh.material.DOFade(1f, 0.3f).SetDelay(1.6f);
		this.gridMesh.material.DOFade(0f, 0.25f).SetDelay(1.85f);
		this.gridMesh.material.DOFade(1f, 0.2f).SetDelay(2.05f);
		this.gridMesh.material.DOFade(0f, 0.15f).SetDelay(2.2f);
		this.gridMesh.material.DOFade(1f, 0.1f).SetDelay(2.3f);
		this.gridMesh.material.DOFade(0f, 0.1f).SetDelay(2.4f);
		this.gridMesh.material.DOFade(1f, 0.1f).SetDelay(2.5f).OnComplete(delegate
		{
			this.gridMesh.material.SetTextureOffset("_MainTex", new Vector2(0f, 10.39f));
			this.gridCollider.enabled = true;
			this.glow.Play();
		});
		this.gridMesh.material.DOFade(0f, 0.5f).SetDelay(3.5f).OnComplete(delegate
		{
			this.gridCollider.enabled = false;
			this.glow.Stop();
		});
	}
}
