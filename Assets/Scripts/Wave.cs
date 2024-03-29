using System;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
	public enum WaveLevel
	{
		Level1,
		Level2,
		Level3,
		Level4,
		Level5
	}

	public abstract void ActionWhenBroken();
}
