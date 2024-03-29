using System;
using UnityEngine;

public class BulletParent : Bullet
{
	public Bullet[] bulletChildren;

	public TrailRenderer trailRenderer;

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
		for (int i = 0; i < this.bulletChildren.Length; i++)
		{
			this.bulletChildren[i].SetDamageAmount(amount);
		}
	}

	private void OnEnable()
	{
		if (this.trailRenderer != null)
		{
			this.trailRenderer.Clear();
		}
		for (int i = 0; i < this.bulletChildren.Length; i++)
		{
			this.bulletChildren[i].GetComponent<Collider>().enabled = true;
			this.bulletChildren[i].gameObject.SetActive(true);
		}
	}

	private void Disable()
	{
		if (this.trailRenderer != null)
		{
			this.trailRenderer.Clear();
		}
	}
}
