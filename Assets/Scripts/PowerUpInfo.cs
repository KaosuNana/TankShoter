using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpInfo : MonoBehaviour
{
	private int _level_k__BackingField;

	private float _fileVersion_k__BackingField;

	public PowerUp.PowerUpType powerUpType;

	public int[] powerUpUpgradeCost;

	private string savedFileName;

	public int level
	{
		get;
		set;
	}

	public float fileVersion
	{
		get;
		set;
	}

	private void Awake()
	{
		this.LoadProgress();
	}

	public int GetUpgradeCost()
	{
		return this.powerUpUpgradeCost[this.level];
	}

	public void LoadProgress()
	{
		this.savedFileName = this.powerUpType.ToString();
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Load<PowerUpInfo>(this.savedFileName, this);
		}
		else
		{
			this.fileVersion = 1f;
			this.level = 0;
			ES2.Save<PowerUpInfo>(this, this.savedFileName);
		}
	}

	public void SaveProgress()
	{
		ES2.Save<PowerUpInfo>(this, this.savedFileName);
	}

	public void ResetProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Delete(this.savedFileName);
		}
	}
}
