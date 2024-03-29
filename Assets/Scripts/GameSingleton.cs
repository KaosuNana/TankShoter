using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameSingleton : MonoBehaviour
{
	private static GameSingleton instance;

//	public HeyZapManager heyzapInstance;

	private int numberOfGame;

	private bool isRestarted;

	private bool shadowPreference;

	private bool isIphoneX;

	private static Action<bool> __f__am_cache0;

	private static Action<bool> __f__am_cache1;

	private static Action<bool> __f__am_cache2;

	public static bool IsiPhoneX
	{
		get
		{
			return GameSingleton.instance.isIphoneX;
		}
	}

	public static bool ShadowPreference
	{
		get
		{
			return GameSingleton.instance.shadowPreference;
		}
	}

	public static int NumberOfGame
	{
		get
		{
			return GameSingleton.instance.numberOfGame;
		}
	}

	public static bool IsRestarted
	{
		get
		{
			return GameSingleton.instance.isRestarted;
		}
	}

	private void Awake()
	{
		if (GameSingleton.instance != null)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(this);
		GameSingleton.instance = this;
		this.isRestarted = false;
		this.numberOfGame = 0;
		this.shadowPreference = PlayerPrefsX.GetBool("ShadowPreference", false);
		this.SetupQualitySetting();
		this.UpdateIsiPhoneX();
	}

	private void UpdateIsiPhoneX()
	{
		int width = Screen.width;
		int height = Screen.height;
		float num = (float)width / (float)height;
		if (num < 0.5f)
		{
			this.isIphoneX = true;
		}
		else
		{
			this.isIphoneX = false;
		}
	}

	private void AndroidGPGInitialise()
	{
	
	}

	private void Start()
	{
		
		PlayerPrefs.SetFloat("LatestVersionPlayed", 1.1f);
	}

	public static void UpdateShadowPreference()
	{
		GameSingleton.instance.shadowPreference = !GameSingleton.instance.shadowPreference;
		PlayerPrefsX.SetBool("ShadowPreference", GameSingleton.instance.shadowPreference);
		GameSingleton.instance.SetupQualitySetting();
	}

	private void SetupQualitySetting()
	{
		if (this.shadowPreference)
		{
			QualitySettings.SetQualityLevel(3, true);
		}
		else
		{
			QualitySettings.SetQualityLevel(0, true);
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		GameSingleton.instance = this;
	}

	public static void RestartLevel()
	{
		GameSingleton.instance.numberOfGame++;
		GameSingleton.instance.isRestarted = true;
	}

	private void ProcessAuthentication(bool success)
	{
		
	}

	public static void ReportScoreToLeaderboard(int scoreToReport)
	{
		
	}

	public static void ShowLeaderboard()
	{
	
	}
}
