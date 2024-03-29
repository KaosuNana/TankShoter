using System;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
	public int baseDamage;

	public int damage;

	public float shootRate;

	private void OnEnable()
	{
		this.damage = this.baseDamage + (int)((float)this.baseDamage * 0.75f * Mathf.Floor((float)(GameManager.CurrentLevel / 2)));
	}

	public virtual void SetDamageAmount(int amount)
	{
		this.damage = amount + (int)((float)amount * 0.75f * Mathf.Floor((float)(GameManager.CurrentLevel / 2)));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 26)
		{
			base.gameObject.SetActive(false);
		}
	}

	public virtual void AfterHitPlayer()
	{
		base.gameObject.SetActive(false);
	}
}
