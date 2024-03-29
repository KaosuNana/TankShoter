using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pillar : MonoBehaviour
{
	public enum PillarType
	{
		pillarNormal,
		pillarFast,
		pillarSuperFast,
		pillarDontTouch
	}

	private PillarController pillarController;

	private Pillar.PillarType _pillarType_k__BackingField;

	public int PillarPosition;

	public Pillar.PillarType pillarType
	{
		get;
		set;
	}

	public void SetPillarType(Pillar.PillarType inputPillarType, Material inputMaterial)
	{
		this.pillarType = inputPillarType;
		base.GetComponent<Renderer>().sharedMaterial = inputMaterial;
	}

	public void SetPillarController(PillarController pController)
	{
		this.pillarController = pController;
	}

	private void OnCollisionEnter(Collision other)
	{
		this.pillarController.GoDown(this);
	}
}
