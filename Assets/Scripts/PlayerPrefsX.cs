using System;
using UnityEngine;

public class PlayerPrefsX
{
	public static void SetBool(string name, bool booleanValue)
	{
		PlayerPrefs.SetInt(name, (!booleanValue) ? 0 : 1);
	}

	public static bool GetBool(string name)
	{
		return PlayerPrefs.GetInt(name) == 1;
	}

	public static bool GetBool(string name, bool defaultValue)
	{
		if (PlayerPrefs.HasKey(name))
		{
			return PlayerPrefsX.GetBool(name);
		}
		return defaultValue;
	}
}
