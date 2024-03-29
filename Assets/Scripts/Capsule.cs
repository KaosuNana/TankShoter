using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Capsule : MonoBehaviour
{
	public Animator animator;

	private bool _isBouncing_k__BackingField;

	private bool _isOpen_k__BackingField;

	private bool _isClosed_k__BackingField;

	private bool _readyToOpen_k__BackingField;

	public AudioClip openCapsule;

	public AudioClip bouncingCapsule;

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

	public bool readyToOpen
	{
		get;
		set;
	}

	public void OpenCapsule()
	{
		if (SoundManager.IsSFXOn)
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.openCapsule);
		}
		this.readyToOpen = false;
		this.isOpen = true;
		this.isBouncing = false;
		this.animator.SetBool("IsOpen", this.isOpen);
		this.animator.SetBool("IsBouncing", this.isBouncing);
	}

	public void CloseCapsule()
	{
		this.isOpen = false;
		this.isClosed = true;
		this.animator.SetBool("IsOpen", this.isOpen);
		this.animator.SetBool("IsClosed", this.isClosed);
	}

	public void PlayBouncingSFX()
	{
		if (SoundManager.IsSFXOn)
		{
			base.GetComponent<AudioSource>().clip = this.bouncingCapsule;
			base.GetComponent<AudioSource>().loop = true;
			base.GetComponent<AudioSource>().Play();
		}
	}

	public void StopBouncingSFX()
	{
		base.GetComponent<AudioSource>().Stop();
	}
}
