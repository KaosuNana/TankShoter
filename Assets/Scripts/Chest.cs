using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Chest : MonoBehaviour
{
	public enum ChestType
	{
		Wood,
		Bronze,
		Silver,
		Golden
	}

	public ChestReward[] outputPrefab;

	private bool _isBouncing_k__BackingField;

	private bool _isOpen_k__BackingField;

	private bool _isClosed_k__BackingField;

	public AudioClip openChest;

	public AudioClip bouncingSFX;

	public Chest.ChestType chestType;

	public AudioSource audioSource;

	private Animator animator;

	public bool isBouncing
	{
		get;
		set;
	}

	public bool isOpen
	{
		get;
		set;
	}

	public bool isClosed
	{
		get;
		set;
	}

	private void Awake()
	{
		this.audioSource = base.gameObject.AddComponent<AudioSource>();
		this.animator = base.GetComponentInChildren<Animator>();
	}

	public void OpenChestWithSFX(bool shouldPlaySFX)
	{
		if (shouldPlaySFX)
		{
			if (SoundManager.IsSFXOn)
			{
				base.GetComponent<AudioSource>().PlayOneShot(this.openChest);
			}
			this.isOpen = true;
			this.isBouncing = false;
			this.animator.SetBool("IsOpen", this.isOpen);
			this.animator.SetBool("IsBouncing", this.isBouncing);
		}
		else
		{
			this.animator.SetTrigger("Opened");
		}
	}

	public void ResetAnim()
	{
		this.animator.Rebind();
	}

	public void CloseChest()
	{
		this.isOpen = false;
		this.isClosed = true;
		this.animator.SetBool("IsOpen", this.isOpen);
		this.animator.SetBool("IsClosed", this.isClosed);
	}

	public void Bouncing(bool shouldBounce)
	{
		this.isBouncing = shouldBounce;
		this.animator.SetBool("IsBouncing", this.isBouncing);
	}

	public void PlayBouncingSFX()
	{
		if (SoundManager.IsSFXOn)
		{
			this.audioSource.clip = this.bouncingSFX;
			this.audioSource.loop = true;
			this.audioSource.Play();
		}
	}

	public void StopBouncingSFX()
	{
		this.audioSource.Stop();
	}
}
