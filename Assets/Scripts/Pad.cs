using System;
using UnityEngine;

public class Pad : MonoBehaviour
{
	public enum PadType
	{
		Shooting,
		DuckAndShoot
	}

	public Pad.PadType padType;
}
