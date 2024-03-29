using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
	private Animator animator;

	public int minCoin;

	public int maxCoin;

	private bool _isBouncing_k__BackingField;

	private bool _isOpen_k__BackingField;

	private bool _isClosed_k__BackingField;

	public AudioClip openChest;

	public AudioClip bouncingSFX;

	private AudioSource audioSource;

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
		this.animator = base.GetComponent<Animator>();
		this.audioSource = base.gameObject.AddComponent<AudioSource>();
	}

	public int GetRandomCoin()
	{
		int num = 50;
		int num2 = (this.maxCoin - this.minCoin) / num;
		int num3 = UnityEngine.Random.Range(0, num2 + 1);
		return this.minCoin + num * num3;
	}

	private void PlayOpeningSFX()
	{
		base.GetComponent<AudioSource>().PlayOneShot(this.openChest);
	}

	public void OpenGiftBoxWithSFX(bool shouldPlaySFX)
	{
		this.StopBouncingSFX();
		if (shouldPlaySFX)
		{
			if (SoundManager.IsSFXOn)
			{
				base.Invoke("PlayOpeningSFX", 1.2f);
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
