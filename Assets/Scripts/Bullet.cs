using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
	private int _damage_k__BackingField;

	private string _damageValue_k__BackingField;

	private float _shootRate_k__BackingField;

	public AudioClip[] impactArraySFX;

	public ParticleSystem bulletImpactParticleFX;

	public GameObject bulletImpactSlot;

	private ParticleSystem bulletImpactFX;

	public int chargeValue = 1;

	protected int impactSFXLength;

	protected bool damageValueStringUpdated;

	public int damage
	{
		get;
		set;
	}

	public string damageValue
	{
		get;
		set;
	}

	public float shootRate
	{
		get;
		set;
	}

	public abstract void SetDamageAmount(int amount);

	private void Awake()
	{
		this.impactSFXLength = this.impactArraySFX.Length;
	}

	protected void UpdateDamageValue()
	{
		if (this.damageValueStringUpdated)
		{
			return;
		}
		this.damageValue = GameManager.CurrencyToString((float)this.damage / 100f);
		this.damageValueStringUpdated = true;
	}

	public virtual void AfterHitEnemy()
	{
		if (this.impactSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.impactArraySFX[UnityEngine.Random.Range(0, this.impactSFXLength)], base.transform.position, 0.5f);
		}
		GameManager.ExtraCharge(this.chargeValue);
		if (this.bulletImpactParticleFX != null && this.bulletImpactSlot != null)
		{
			if (this.bulletImpactFX == null)
			{
				this.bulletImpactFX = UnityEngine.Object.Instantiate<ParticleSystem>(this.bulletImpactParticleFX, this.bulletImpactSlot.transform.position, this.bulletImpactParticleFX.transform.rotation);
			}
			else
			{
				this.bulletImpactFX.transform.position = this.bulletImpactSlot.transform.position;
				this.bulletImpactFX.Play();
			}
		}
		base.gameObject.SetActive(false);
	}
}
