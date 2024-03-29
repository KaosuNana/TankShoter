using System;
using UnityEngine;

public class CampFireController : MonoBehaviour
{
	public ParticleSystem campFireParticle;

	public void StartFireParticle()
	{
		this.campFireParticle.Play();
	}

	public void StopFireParticle()
	{
		this.campFireParticle.Stop();
	}
}
