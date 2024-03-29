// using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	public enum GameState
	{
		LoadingState,
		MenuState,
		PlayState,
		PauseState,
		GameOverState,
		UpgradeScreenState,
		ContinueGameSuccessState,
		ResultState,
		TutorialState
	}

	private sealed class _SetGameOver_c__AnonStorey1
	{
		internal float myTimeScale;

		internal float __m__0()
		{
			return this.myTimeScale;
		}

		internal void __m__1(float x)
		{
			this.myTimeScale = x;
		}

		internal void __m__2()
		{
			Time.timeScale = this.myTimeScale;
		}
	}

	private sealed class _ContinueGameSequence_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _ContinueGameSequence_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				GameManager.instance.playerInstance.PrepareToContinueGame();
				GameManager.instance.state = GameManager.GameState.PlayState;
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public static GameManager instance;

	public CanvasManager canvasInstance;

	public CameraController mainCamera;

	public GroundManager groundManagerInstance;

	public MasterPoolManager masterPoolManagerInstance;

	public LevelManager levelManagerInstance;

	public PlayerController playerInstance;

	public SidekickManager sidekickManagerInstance;

	public PowerUpManager powerUpManagerInstance;

	public MissionManager missionManagerInstance;

	public IdleIncomeManager idleIncomeManagerInstance;

	public VehicleManager vehicleManagerInstance;

	public GameObject shopPageInstance;

	public TransitionScreenController transitionScreenInstance;

	public SplashScreenController splashScreenInstance;

	public MasterMenuManager masterMenuManagerInstance;

	public CountdownController countdownInstance;

	// public Purchaser purchaseInstance;

	public PowerUpSidekickFXController powerUpSidekickFXInstance;

	public GameObject[] coinPrefab;

	public float[] coinDropProbability;

	public GameObject[] powerUpPrefab;

	public float[] powerUpDropProbability;

	public GameObject enemyBulletPrefab;

	public GameObject bulletHitParticle;

	public GameObject worldCanvasInstance;

	public ExtraScoreController extraScorePrefab;

	private bool rejectContinue;

	private float strengthMultiplier;

	private int coinMultiplier;

	// private ObscuredInt coin;
	private int coin;
	
	// private ObscuredInt bolt;
	private int bolt;
	
	// private ObscuredInt continueAmount;
	private int continueAmount;
	
	// private ObscuredBool hasPurchasedGoldenGiftBox;
	private bool hasPurchasedGoldenGiftBox;
	
	// private ObscuredBool hasPurchasedFreeContinue;
	private bool hasPurchasedFreeContinue;
	
	// private ObscuredInt thisGameCoin;
	private int thisGameCoin;
	
	// private ObscuredInt thisGameBolt;
	private int thisGameBolt;

	private GameManager.GameState state;

	private int currentDifficultyWaiver;

	private bool hasPurchasedSomething;

	protected GameManager.CallBackFunction callbackFct;

	private int currentScore;

	private int currentCharge;

	public int maxCharge = 50;

	private int numberOfTimesCheckpointSavedUsingBolt;

	private int scoreMultiplier = 1;

	private Tween pauseTween;

	private List<ExtraScoreController> extraScoreList;

	private List<GameObject> coinList;

	private List<GameObject> coinBigList;

	private List<GameObject> boltList;

	private List<GameObject> powerUpList0;

	private List<GameObject> powerUpList1;

	private List<GameObject> powerUpList2;

	private List<GameObject> powerUpList3;

	private List<GameObject> powerUpList4;

	private bool isReleasingCharge;

	// private ObscuredInt numberOfPrestige;
	private int numberOfPrestige;

	private int slimeBossDestroyedCounter;

	public static int CurrentLevel
	{
		get
		{
			return GameManager.instance.levelManagerInstance.currentLevel;
		}
	}

	public static int CurrentDifficultyLevel
	{
		get
		{
			return Mathf.Max(GameManager.instance.levelManagerInstance.currentLevel - GameManager.instance.currentDifficultyWaiver, 1);
		}
	}

	public static Camera MainCamera
	{
		get
		{
			return GameManager.instance.mainCamera.GetComponent<Camera>();
		}
	}

	public static GameObject WorldCanvas
	{
		get
		{
			return GameManager.instance.worldCanvasInstance;
		}
	}

	public static int NumberOfPrestige
	{
		get
		{
			return GameManager.instance.numberOfPrestige;
		}
	}

	public static bool HasPurchasedGoldenGiftBox
	{
		get
		{
			return GameManager.instance.hasPurchasedGoldenGiftBox;
		}
	}

	public static bool HasPlayerPurchasedSomething
	{
		get
		{
			return GameManager.instance.hasPurchasedSomething;
		}
	}

	public static bool HasPurchasedFreeContinue
	{
		get
		{
			return GameManager.instance.hasPurchasedFreeContinue;
		}
	}

	public static GameManager.GameState CurrentState
	{
		get
		{
			return GameManager.instance.state;
		}
		set
		{
			GameManager.instance.state = value;
		}
	}

	public static float StrengthMultiplier
	{
		get
		{
			return GameManager.instance.strengthMultiplier;
		}
	}

	public static int CoinMultiplier
	{
		get
		{
			if (GameManager.instance.coinMultiplier == 0)
			{
				GameManager.instance.UpdateCoinMultiplier();
			}
			return GameManager.instance.coinMultiplier;
		}
	}

	public static int Coin
	{
		get
		{
			return GameManager.instance.coin;
		}
	}

	public static int Bolt
	{
		get
		{
			return GameManager.instance.bolt;
		}
	}

	public static PlayerController Player
	{
		get
		{
			return GameManager.instance.playerInstance;
		}
	}

	public static int ContinueAmount
	{
		get
		{
			return GameManager.instance.continueAmount;
		}
	}

	public static int ThisGameCoin
	{
		get
		{
			return GameManager.instance.thisGameCoin;
		}
	}

	public static int ThisGameGem
	{
		get
		{
			return GameManager.instance.thisGameBolt;
		}
	}

	private void Awake()
	{
		GameManager.instance = this;
		Application.targetFrameRate = 60;
		Time.timeScale = 1f;
		this.currentScore = 0;
		this.coin = PlayerPrefs.GetInt("CoinCollected");
		this.bolt = PlayerPrefs.GetInt("GemCollected");
		this.canvasInstance.hudMenuInstance.OnUpdateCoin();
		this.canvasInstance.hudMenuInstance.OnUpdateBolts();
		this.continueAmount = 0;
		this.state = GameManager.GameState.LoadingState;
		this.missionManagerInstance.EnableMission();
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
		this.hasPurchasedFreeContinue = PlayerPrefs.GetInt("HasPurchasedFreeContinue", 0)==1;
		this.hasPurchasedGoldenGiftBox = PlayerPrefs.GetInt("HasPurchasedGoldenGiftBox", 0)==1;
		this.hasPurchasedSomething = PlayerPrefs.GetInt("HasPurchasedSomething", 0)==1;
		for (int i = 1; i < this.coinDropProbability.Length; i++)
		{
			this.coinDropProbability[i] = this.coinDropProbability[i - 1] + this.coinDropProbability[i];
		}
		for (int j = 1; j < this.powerUpDropProbability.Length; j++)
		{
			this.powerUpDropProbability[j] = this.powerUpDropProbability[j - 1] + this.powerUpDropProbability[j];
		}
		GameManager.instance.canvasInstance.ShowMainMenu(true);
		this.extraScoreList = new List<ExtraScoreController>();
		this.coinList = new List<GameObject>();
		this.coinBigList = new List<GameObject>();
		this.boltList = new List<GameObject>();
		this.powerUpList0 = new List<GameObject>();
		this.powerUpList1 = new List<GameObject>();
		this.powerUpList2 = new List<GameObject>();
		this.powerUpList3 = new List<GameObject>();
		this.powerUpList4 = new List<GameObject>();
		this.StartPoolingPickUpObjects();
		this.groundManagerInstance.StartScrolling();
		GameManager.instance.numberOfPrestige = PlayerPrefs.GetInt("PlayerPrestigeCount");
		GameManager.instance.strengthMultiplier = 1f + (float)this.numberOfPrestige * 0.5f;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= new UnityAction<Scene, LoadSceneMode>(this.OnSceneLoaded);
	}

	private void Start()
	{
		if (!GameSingleton.IsRestarted)
		{
			GameManager.UpdateAllLocalNotification();
			this.splashScreenInstance.gameObject.SetActive(true);
			this.splashScreenInstance.TransitionIn();
		}
		else
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			this.transitionScreenInstance.gameObject.SetActive(true);
			this.transitionScreenInstance.TransitionIn();
		}
		SoundManager.PlayMusic(SoundManager.Music.MainMusic, 0.5f);
		this.UpdateCoinMultiplier();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		GameManager.instance = this;
		if (hasFocus)
		{
			this.CheckIdleIncome();
		}
	}

	private void CheckIdleIncome()
	{
		if (PlayerPrefs.HasKey("QuitTimeStamp"))
		{
			DateTime d = DateTime.Now;
			DateTime d2 = DateTime.FromFileTime(Convert.ToInt64(PlayerPrefs.GetString("QuitTimeStamp")));
			TimeSpan timeSpan = d - d2;
			float num = (float)(timeSpan.Hours * 3600 + timeSpan.Minutes * 60 + timeSpan.Seconds);
			if (num >= 300f)
			{
				GameManager.ShowIdleIncomePage();
				this.idleIncomeManagerInstance.ShowIdleScreenWithAway(num);
			}
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			if (this.state == GameManager.GameState.PlayState)
			{
				GameManager.PauseGame();
			}
			else if (this.state == GameManager.GameState.PauseState)
			{
				GameManager.ResumeGame();
			}
			else if (this.state == GameManager.GameState.MenuState)
			{
				Application.Quit();
			}
			else
			{
				EventManager.TriggerEvent("EventAndroidBackButton");
			}
		}
	}

	private void UpdateCoinMultiplier()
	{
		int numberOfVehicleUpgrades = this.vehicleManagerInstance.GetNumberOfVehicleUpgrades();
		if (numberOfVehicleUpgrades <= 1)
		{
			this.coinMultiplier = 1;
		}
		else if (numberOfVehicleUpgrades <= 3)
		{
			this.coinMultiplier = 2;
		}
		else
		{
			this.coinMultiplier = 2 + (numberOfVehicleUpgrades - 3) / 3;
		}
	}

	public static void WarnBossWave()
	{
		GameManager.instance.canvasInstance.WarnBossWave();
	}

	public static void ActivateScoreMultiplier()
	{
		GameManager.instance.CancelInvoke("DeactivateScoreMultiplier");
		GameManager.instance.scoreMultiplier = 2;
		GameManager.instance.Invoke("DeactivateScoreMultiplier", 5f);
	}

	private void DeactivateScoreMultiplier()
	{
		this.scoreMultiplier = 1;
	}

	public static void BossSlimeDestroyed(int requiredAmountToDestroy)
	{
		GameManager.instance.slimeBossDestroyedCounter++;
		if (GameManager.instance.slimeBossDestroyedCounter >= requiredAmountToDestroy)
		{
			GameManager.instance.slimeBossDestroyedCounter = 0;
			GameManager.BossDestroyed();
			EventManager.TriggerEvent("EventBossSlimeDestroyed");
		}
	}

	public static void BossDestroyed()
	{
		EventManager.TriggerEvent("ShootBoss");
		GameManager.NextLevelShouldHideBanner(false);
		GameManager.instance.mainCamera.ShakeCamera(1f);
		SoundManager.PlayMusic(SoundManager.Music.MainMusic, 1f);
	}

	private void SpawnNextWavePrivate()
	{
		GameManager.SpawnNextWave();
	}

	private void ShowResultStageClear()
	{
		GameManager.ShowResult(true);
	}

	public static int GetCheckpointBoltCost()
	{
		return 10;
	}

	public static void NextLevelShouldHideBanner(bool hideBanner)
	{
		if (hideBanner)
		{
			GameManager.instance.canvasInstance.HideResultScreen();
			GameManager.instance.missionManagerInstance.HideMissionBanner();
			GameManager.instance.playerInstance.PlayerEnter();
		}
		GameManager.instance.levelManagerInstance.NextLevel();
		GameManager.instance.groundManagerInstance.CheckChunks();
		GameManager.instance.currentDifficultyWaiver = 3;
		GameManager.instance.state = GameManager.GameState.PlayState;
		GameManager.instance.Invoke("SpawnNextWavePrivate", 3f);
	}

	private ExtraScoreController GetPooledExtraScoreController()
	{
		int count = this.extraScoreList.Count;
		for (int i = 0; i < count; i++)
		{
			if (!this.extraScoreList[i].gameObject.activeSelf)
			{
				return this.extraScoreList[i];
			}
		}
		ExtraScoreController extraScoreController = UnityEngine.Object.Instantiate<ExtraScoreController>(GameManager.instance.extraScorePrefab);
		this.extraScoreList.Add(extraScoreController);
		return extraScoreController;
	}

	public static void ShowValue(string damageValue, Vector3 pos, ExtraScoreController.ScoreType scoreType)
	{
		ExtraScoreController pooledExtraScoreController = GameManager.instance.GetPooledExtraScoreController();
		pooledExtraScoreController.SetColor(scoreType);
		pooledExtraScoreController.gameObject.SetActive(true);
		pooledExtraScoreController.transform.SetParent(GameManager.instance.worldCanvasInstance.transform, false);
		pooledExtraScoreController.transform.localPosition = new Vector3(UnityEngine.Random.Range(pos.x - 0.5f, pos.x + 0.5f), pos.y, pos.z);
		pooledExtraScoreController.SetText(damageValue);
	}

	public static string CurrencyToString(float valueToConvert)
	{
		if (valueToConvert < 1000f)
		{
			if (valueToConvert >= 100f)
			{
				return valueToConvert.ToString("0");
			}
			if (valueToConvert >= 10f)
			{
				return valueToConvert.ToString("0.#");
			}
			return valueToConvert.ToString("0.##");
		}
		else if (valueToConvert < 1000000f)
		{
			valueToConvert /= 1000f;
			if (valueToConvert >= 100f)
			{
				return valueToConvert.ToString("0") + "K";
			}
			if (valueToConvert >= 10f)
			{
				return valueToConvert.ToString("0.#") + "K";
			}
			return valueToConvert.ToString("0.##") + "K";
		}
		else if (valueToConvert < 1E+09f)
		{
			valueToConvert /= 1000000f;
			if (valueToConvert >= 100f)
			{
				return valueToConvert.ToString("0") + "M";
			}
			if (valueToConvert >= 10f)
			{
				return valueToConvert.ToString("0.#") + "M";
			}
			return valueToConvert.ToString("0.##") + "M";
		}
		else if (valueToConvert < 1E+12f)
		{
			valueToConvert /= 1E+09f;
			if (valueToConvert >= 100f)
			{
				return valueToConvert.ToString("0") + "B";
			}
			if (valueToConvert >= 10f)
			{
				return valueToConvert.ToString("0.#") + "B";
			}
			return valueToConvert.ToString("0.##") + "B";
		}
		else
		{
			if (valueToConvert >= 1E+15f)
			{
				return valueToConvert.ToString();
			}
			valueToConvert /= 1E+12f;
			if (valueToConvert >= 100f)
			{
				return valueToConvert.ToString("0") + "T";
			}
			if (valueToConvert >= 10f)
			{
				return valueToConvert.ToString("0.#") + "T";
			}
			return valueToConvert.ToString("0.##") + "T";
		}
	}

	public static float GetCurrentCharge()
	{
		return (float)GameManager.instance.currentCharge / (float)GameManager.instance.maxCharge;
	}

	public static void ExtraCharge(int extraCharge)
	{
		if (!GameManager.instance.isReleasingCharge)
		{
			GameManager.instance.currentCharge = Mathf.Min(GameManager.instance.currentCharge + extraCharge, GameManager.instance.maxCharge);
			GameManager.instance.canvasInstance.UpdateChargeBar((float)GameManager.instance.currentCharge / (float)GameManager.instance.maxCharge, 0.2f);
		}
	}

	public static void ResetChargeBar()
	{
		GameManager.instance.currentCharge = 0;
		GameManager.instance.canvasInstance.UpdateChargeBar(0f, 0.2f);
	}

	public static void ExtraScore(int extraScore, Vector3 position, string extraText)
	{
		GameManager.instance.currentScore = GameManager.instance.currentScore + extraScore * GameManager.instance.scoreMultiplier;
	}

	public static void ResetCoin()
	{
		GameManager.instance.coin = 0;
	}

	public static void ResetCharge()
	{
		GameManager.instance.isReleasingCharge = true;
		GameManager.instance.currentCharge = 0;
		GameManager.instance.canvasInstance.UpdateChargeBar(0f, 0f);
		GameManager.instance.ReleasingChargeCompleted();
	}

	private void ReleasingChargeCompleted()
	{
		GameManager.instance.isReleasingCharge = false;
	}

	public static GameObject GetEnemyBulletPrefab()
	{
		return GameManager.instance.enemyBulletPrefab;
	}

	public static void SpawnBulletHitParticle(Vector3 hitPos)
	{
		UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.bulletHitParticle, hitPos, Quaternion.identity);
	}

	private void StartPoolingPickUpObjects()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[0]);
		gameObject.SetActive(false);
		this.coinList.Add(gameObject);
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[1]);
		gameObject2.SetActive(false);
		this.coinBigList.Add(gameObject2);
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[2]);
		gameObject3.SetActive(false);
		this.boltList.Add(gameObject3);
		GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[0]);
		gameObject4.SetActive(false);
		this.powerUpList0.Add(gameObject4);
		GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[1]);
		gameObject5.SetActive(false);
		this.powerUpList1.Add(gameObject5);
		GameObject gameObject6 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[2]);
		gameObject6.SetActive(false);
		this.powerUpList2.Add(gameObject6);
		GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[3]);
		gameObject7.SetActive(false);
		this.powerUpList3.Add(gameObject7);
		GameObject gameObject8 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[4]);
		gameObject8.SetActive(false);
		this.powerUpList4.Add(gameObject8);
		for (int i = 0; i < 5; i++)
		{
			ExtraScoreController extraScoreController = UnityEngine.Object.Instantiate<ExtraScoreController>(GameManager.instance.extraScorePrefab);
			extraScoreController.gameObject.SetActive(false);
			this.extraScoreList.Add(extraScoreController);
		}
	}

	private GameObject GetPooledPowerUp(int index)
	{
		if (index == 0)
		{
			for (int i = 0; i < this.powerUpList0.Count; i++)
			{
				if (!this.powerUpList0[i].gameObject.activeSelf)
				{
					return this.powerUpList0[i];
				}
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[0]);
			this.powerUpList0.Add(gameObject);
			return gameObject;
		}
		if (index == 1)
		{
			for (int j = 0; j < this.powerUpList1.Count; j++)
			{
				if (!this.powerUpList1[j].gameObject.activeSelf)
				{
					return this.powerUpList1[j];
				}
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[1]);
			this.powerUpList1.Add(gameObject2);
			return gameObject2;
		}
		if (index == 2)
		{
			for (int k = 0; k < this.powerUpList2.Count; k++)
			{
				if (!this.powerUpList2[k].gameObject.activeSelf)
				{
					return this.powerUpList2[k];
				}
			}
			GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[2]);
			this.powerUpList2.Add(gameObject3);
			return gameObject3;
		}
		if (index == 3)
		{
			for (int l = 0; l < this.powerUpList3.Count; l++)
			{
				if (!this.powerUpList3[l].gameObject.activeSelf)
				{
					return this.powerUpList3[l];
				}
			}
			GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[3]);
			this.powerUpList3.Add(gameObject4);
			return gameObject4;
		}
		for (int m = 0; m < this.powerUpList4.Count; m++)
		{
			if (!this.powerUpList4[m].gameObject.activeSelf)
			{
				return this.powerUpList4[m];
			}
		}
		GameObject gameObject5 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.powerUpPrefab[4]);
		this.powerUpList4.Add(gameObject5);
		return gameObject5;
	}

	private GameObject GetPooledCoin()
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		if (num < GameManager.instance.coinDropProbability[0])
		{
			for (int i = 0; i < this.coinList.Count; i++)
			{
				if (!this.coinList[i].gameObject.activeSelf)
				{
					return this.coinList[i];
				}
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[0]);
			this.coinList.Add(gameObject);
			return gameObject;
		}
		if (num < GameManager.instance.coinDropProbability[1])
		{
			for (int j = 0; j < this.coinBigList.Count; j++)
			{
				if (!this.coinBigList[j].gameObject.activeSelf)
				{
					return this.coinBigList[j];
				}
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[1]);
			this.coinBigList.Add(gameObject2);
			return gameObject2;
		}
		for (int k = 0; k < this.boltList.Count; k++)
		{
			if (!this.boltList[k].gameObject.activeSelf)
			{
				return this.boltList[k];
			}
		}
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(GameManager.instance.coinPrefab[2]);
		this.boltList.Add(gameObject3);
		return gameObject3;
	}

	public static void SpawnCoin(Vector3 coinPos)
	{
		GameObject pooledCoin = GameManager.instance.GetPooledCoin();
		coinPos.z = UnityEngine.Random.Range(coinPos.z, coinPos.z + 2f);
		pooledCoin.transform.position = coinPos;
		pooledCoin.transform.DOKill(false);
		pooledCoin.SetActive(true);
		if (pooledCoin.GetComponent<CoinController>() != null)
		{
			pooledCoin.GetComponent<CoinController>().Spawned();
		}
	}

	public static void SpawnPowerUp(Vector3 powerUpPos)
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		int index;
		if (num < GameManager.instance.powerUpDropProbability[0])
		{
			index = 0;
		}
		else if (num < GameManager.instance.powerUpDropProbability[1])
		{
			index = 1;
		}
		else if (num < GameManager.instance.powerUpDropProbability[2])
		{
			index = 2;
		}
		else if (num < GameManager.instance.powerUpDropProbability[3])
		{
			index = 3;
		}
		else
		{
			index = 4;
		}
		powerUpPos.z = UnityEngine.Random.Range(powerUpPos.z - 5f, powerUpPos.z + 5f);
		GameObject pooledPowerUp = GameManager.instance.GetPooledPowerUp(index);
		pooledPowerUp.SetActive(true);
		pooledPowerUp.transform.position = powerUpPos;
		if (pooledPowerUp.GetComponent<PowerUp>() != null)
		{
			pooledPowerUp.GetComponent<PowerUp>().Spawned();
		}
	}

	private Vector3 RandomCircle(Vector3 center, float radius, int index)
	{
		float num = (float)(45 * index);
		Vector3 result;
		result.x = center.x + radius * Mathf.Sin(num * 0.0174532924f);
		result.y = center.y;
		result.z = center.z + radius * Mathf.Cos(num * 0.0174532924f);
		return result;
	}

	public static int GetIdleIncome()
	{
		return 500;
	}

	public static void HealHP(float amount)
	{
		GameManager.instance.playerInstance.HealHP(amount);
	}

	public static void ActivateMagnetMode(float duration)
	{
		GameManager.instance.playerInstance.ActivateMagnetMode(duration);
	}

	public static void ActivateSidekick(Vehicle.ID vehicleID, float duration)
	{
		GameManager.instance.sidekickManagerInstance.ActivateSidekick(vehicleID, duration);
	}

	public static void PlaySidekickFX(Vehicle.ID vehicleID)
	{
		GameManager.instance.powerUpSidekickFXInstance.gameObject.SetActive(true);
		GameManager.instance.powerUpSidekickFXInstance.ActivateSidekickForVehicle(vehicleID);
	}

	public static void ActivateShieldMode(float duration)
	{
		GameManager.instance.playerInstance.ActivateShieldMode(duration);
	}

	public static void ActivateRapidFire(float duration)
	{
		GameManager.instance.playerInstance.ActivateRapidMode(duration);
	}

	public static void BatterySaverMode()
	{
	}

	public static void StartTutorialMode()
	{
		PlayerPrefs.SetInt("HasCompletedTutorial", 0);
		GameManager.RestartGame();
	}

	public static void TransitionOutThenRestart()
	{
		GameManager.instance.transitionScreenInstance.gameObject.SetActive(true);
		GameManager.instance.transitionScreenInstance.TransitionOutWithRestart(true);
	}

	public static void PlayerPrestige()
	{
		GameManager expr_05 = GameManager.instance;
		expr_05.numberOfPrestige = ++expr_05.numberOfPrestige;
		GameManager.instance.coin = 0;
		PlayerPrefs.SetInt("CoinCollected", GameManager.instance.coin);
		GameManager.instance.vehicleManagerInstance.PlayerPrestige();
		GameManager.instance.powerUpManagerInstance.PlayerPrestige();
		GameManager.instance.levelManagerInstance.PlayerPrestige();
		PlayerPrefs.SetInt("PlayerPrestigeCount", GameManager.instance.numberOfPrestige);
		GameManager.RestartGame();
	}

	public static void RestartGame()
	{
		GameSingleton.RestartLevel();
		SceneManager.LoadScene(0);
	}

	public static void UpdateSensitivity()
	{
		GameManager.instance.playerInstance.UpdateSensitivity();
	}

	public static void HidePowerUpPage()
	{
		if (GameManager.instance.state == GameManager.GameState.UpgradeScreenState)
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			SoundManager.UnMutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.HidePages();
	}

	public static void ShowPowerUpPage()
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.PowerUpPage);
		GameManager.instance.mainCamera.gameObject.SetActive(false);
	}

	public static void HideShopPage()
	{
		if (GameManager.instance.state == GameManager.GameState.UpgradeScreenState)
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			SoundManager.UnMutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.HidePages();
	}

	public static void ShowShopPageWithNotEnoughCoinOrBolt(bool notEnoughCoins, bool notEnoughBolts)
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.ShopPage);
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		if (notEnoughCoins || notEnoughBolts)
		{
			GameManager.instance.shopPageInstance.GetComponent<ShopPageManager>().AnimateNotEnoughCoinsOrBolts(notEnoughCoins, notEnoughBolts);
		}
	}

	public static void PurchaseGoldenGiftBox()
	{
		PlayerPrefs.SetInt("HasPurchasedGoldenGiftBox", 1);
		GameManager.HasPurchasedSomething();
		GameManager.instance.hasPurchasedGoldenGiftBox = true;
	}

	public static void HasPurchasedSomething()
	{
		PlayerPrefs.SetInt("HasPurchasedSomething", 1);
		GameManager.instance.hasPurchasedSomething = true;
	}

	public static void PurchaseFreeContinue()
	{
		PlayerPrefs.SetInt("HasPurchasedFreeContinue", 1);
		GameManager.HasPurchasedSomething();
		GameManager.instance.hasPurchasedFreeContinue = true;
	}

	public static void StartGame()
	{
		SoundManager.FadeMusicTo(1f, 0.1f, 0f);
		GameManager.instance.state = GameManager.GameState.PlayState;
		GameManager.instance.canvasInstance.ShowGamePlayHUD();
		GameManager.instance.mainCamera.StartGameMode();
		GameManager.instance.canvasInstance.ShowMainMenu(false);
		GameManager.instance.missionManagerInstance.ShowMissionBannerIfAnyActiveForDuration(1.5f);
	}

	public static void SpawnNextWave()
	{
		GameManager.instance.currentDifficultyWaiver = Mathf.Max(GameManager.instance.currentDifficultyWaiver--, 0);
		GameManager.instance.levelManagerInstance.SpawnNextWave();
	}

	public static void ExtraCoinExcludeThisGameCoin(int extraCoins)
	{
		GameManager expr_05 = GameManager.instance;
		expr_05.coin += extraCoins;
		PlayerPrefs.SetInt("CoinCollected", GameManager.instance.coin);
		GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateCoin();
	}

	public static void ExtraCoinFromGameplay(int extraCoins, Vector3 position)
	{
		GameManager.ShowValue("+" + (extraCoins * GameManager.instance.coinMultiplier).ToString(), position, ExtraScoreController.ScoreType.Coin);
		GameManager.ExtraCoin(extraCoins);
	}

	public static void ExtraCoin(int extraCoins)
	{
		GameManager expr_05 = GameManager.instance;
		expr_05.coin += extraCoins * GameManager.instance.coinMultiplier;
		GameManager expr_2C = GameManager.instance;
		expr_2C.thisGameCoin += extraCoins * GameManager.instance.coinMultiplier;
		PlayerPrefs.SetInt("CoinCollected", GameManager.instance.coin);
		GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateCoin();
	}

	public static void ExtraBoltExcludeThisGameBolt(int extraGems)
	{
		GameManager expr_05 = GameManager.instance;
		expr_05.bolt += extraGems;
		PlayerPrefs.SetInt("GemCollected", GameManager.instance.bolt);
		GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateBolts();
	}

	public static void ExtraBolt(int extraBolts)
	{
		GameManager expr_05 = GameManager.instance;
		expr_05.bolt += extraBolts;
		GameManager expr_21 = GameManager.instance;
		expr_21.thisGameBolt += extraBolts;
		PlayerPrefs.SetInt("GemCollected", GameManager.instance.bolt);
		GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateBolts();
	}

	public static void SetLayerOnAllRecursive(GameObject obj, int layer)
	{
		obj.layer = layer;
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				GameManager.SetLayerOnAllRecursive(transform.gameObject, layer);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void ShowIdleIncomePage()
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.IdleIncomePage);
	}

	public static void HideIdleIncomePage()
	{
		if (GameManager.instance.state == GameManager.GameState.UpgradeScreenState)
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			SoundManager.UnMutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.HidePages();
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
	}

	public static void ShowPrestigePage()
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.PrestigePage);
	}

	public static void ShowVehiclePage()
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.VehiclePage);
	}

	public static void HideVehiclePage()
	{
		if (GameManager.instance.state == GameManager.GameState.UpgradeScreenState)
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			SoundManager.UnMutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.HidePages();
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
	}

	public void OnPressRestart()
	{
		SoundManager.PlayTapSFX();
		GameManager.instance.transitionScreenInstance.gameObject.SetActive(true);
		GameManager.instance.transitionScreenInstance.TransitionOutWithRestart(true);
	}

	public static void ShowSettingMenu()
	{
		if (GameManager.instance.state == GameManager.GameState.MenuState)
		{
			GameManager.instance.state = GameManager.GameState.UpgradeScreenState;
			SoundManager.MutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.ShowPage(MasterMenuManager.Page.SettingPage);
	}

	public static void HideSettingMenu()
	{
		if (GameManager.instance.state == GameManager.GameState.UpgradeScreenState)
		{
			GameManager.instance.state = GameManager.GameState.MenuState;
			SoundManager.UnMutePlayerLoopSFX();
		}
		GameManager.instance.mainCamera.gameObject.SetActive(true);
		GameManager.instance.masterMenuManagerInstance.gameObject.SetActive(false);
		GameManager.instance.masterMenuManagerInstance.HidePages();
	}

	public static void ResetBadgeNotification()
	{
	}

	private void OnApplicationPause(bool paused)
	{
		if (paused)
		{
			GameManager.UpdateIdleTimeStamp();
		}
		GameManager.PauseGame();
	}

	private void OnApplicationQuit()
	{
		float num = PlayerPrefs.GetFloat("PlayTimeInSeconds", 0f);
		num += Time.realtimeSinceStartup;
		PlayerPrefs.SetFloat("PlayTimeInSeconds", num);
		GameManager.UpdateAllLocalNotification();
		GameManager.UpdateIdleTimeStamp();
	}

	public static void UpdateIdleTimeStamp()
	{
		PlayerPrefs.SetString("QuitTimeStamp", DateTime.Now.ToFileTime().ToString());
		GameManager.UpdateAllLocalNotification();
	}

	public static void UpdateAllLocalNotification()
	{
		GameManager.ResetBadgeNotification();
		if (GameManager.instance.missionManagerInstance.GetTheEarliestTimeCountdown() > 0f)
		{
			GameManager.instance.RegisterForLocalNotification(GameManager.instance.missionManagerInstance.GetTheEarliestTimeCountdown(), "New Missions", ScriptLocalization.Get("New missions available"));
		}
		if (PlayerPrefsX.GetBool("ShouldSendIdleLocalNotification", false))
		{
			float time = 14400f;
			float time2 = 28800f;
			string message = string.Format(ScriptLocalization.Get("You have earned {0} while you were away. Collect now"), GameManager.CurrencyToString(GameManager.instance.idleIncomeManagerInstance.GetEarningEstimateForTime(time)));
			string title = ScriptLocalization.Get("Tank Buddies");
			GameManager.instance.RegisterForLocalNotification(time, title, message);
			GameManager.instance.RegisterForLocalNotification(time2, title, ScriptLocalization.Get("You have maxed your earning. Play now"));
		}
	}

	private void RegisterForLocalNotification(float time, string title, string message)
	{
	}

	public static void ExtraCoinWithCoinBurstMissionReward(int extraCoins)
	{
		GameManager.ExtraCoin(extraCoins);
		GameManager.instance.canvasInstance.hudMenuInstance.ShowCoinBurstParticle(extraCoins);
		GameManager.instance.Invoke("FinishedRewardingMissionWithCoins", 2.5f);
	}

	public static void ExtraBoltWithBoltBurstMissionReward(int extraBolts)
	{
		GameManager.ExtraBolt(extraBolts);
		GameManager.instance.canvasInstance.hudMenuInstance.ShowBoltBurstParticle(extraBolts);
		GameManager.instance.Invoke("FinishedRewardingMissionWithCoins", 2.5f);
	}

	private void FinishedRewardingMissionWithCoins()
	{
		GameManager.ShowResult(false);
	}

	private void CheckMissions()
	{
		if (this.missionManagerInstance.IsAnyMissionCompleted())
		{
			this.missionManagerInstance.StartMissionToReward();
		}
		else
		{
			GameManager.ShowResult(false);
		}
	}

	public static void HurtEffects()
	{
		GameManager.instance.canvasInstance.hudMenuInstance.HurtEffects();
		GameManager.instance.mainCamera.ShakeCameraForHurt();
	}

	public static void SetGameOver()
	{
		if (GameManager.instance.state == GameManager.GameState.GameOverState)
		{
			return;
		}
		GameManager.instance.mainCamera.ShakeCamera(0.5f);
		float myTimeScale = 0f;
		Time.timeScale = 0f;
		GameManager.instance.canvasInstance.hudMenuInstance.HurtEffects();
		DOTween.To(() => myTimeScale, delegate(float x)
		{
			myTimeScale = x;
		}, 1f, 1f).SetDelay(0.1f).SetUpdate(true).OnUpdate(delegate
		{
			Time.timeScale = myTimeScale;
		});
		GameManager.instance.state = GameManager.GameState.GameOverState;
		GameManager.instance.sidekickManagerInstance.StopShooting();
		if (GameManager.instance.continueAmount < 2)
		{
			GameManager expr_D1 = GameManager.instance;
			expr_D1.continueAmount = ++expr_D1.continueAmount;
			GameManager.instance.Invoke("ShowContinue", 1f);
		}
		else
		{
			GameManager.instance.Invoke("CheckMissions", 1f);
		}
		GameManager.instance.canvasInstance.HidePauseButton();
	}

	public static void ShowResult(bool isStageCleared)
	{
		GameManager.instance.Invoke("ShowNextSteps", 1f);
		SoundManager.PlayMusic(SoundManager.Music.MainMusic, 0.5f);
		GameManager.instance.canvasInstance.ShowResultWithStageClear(isStageCleared);
		GameManager.instance.missionManagerInstance.ShowMissionBanner();
        //HeyZapManager.ShouldShowSkippableVideo();
        // AdsControl.Instance.showAds();
	}

	private void ShowNextSteps()
	{
		GameManager.instance.canvasInstance.ShowNextSteps();
	}

	private void RejectContinue()
	{
		if (GameManager.instance.rejectContinue)
		{
			return;
		}
		GameManager.instance.rejectContinue = true;
		GameManager.instance.canvasInstance.HideContinueWithPauseButton(false);
		GameManager.ShowResult(false);
	}

	public static bool ReduceCoinByShouldOfferShop(int amount, bool shouldOfferShop)
	{
		if (GameManager.instance.coin >= amount)
		{
			GameManager expr_1A = GameManager.instance;
			expr_1A.coin -= amount;
			PlayerPrefs.SetInt("CoinCollected", GameManager.instance.coin);
			GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateCoin();
			return true;
		}
		if (shouldOfferShop)
		{
			GameManager.ShowShopPageWithNotEnoughCoinOrBolt(true, false);
		}
		return false;
	}

	public static bool ReduceBoltByShouldOfferShop(int amount, bool shouldOfferShop)
	{
		if (GameManager.instance.bolt >= amount)
		{
			GameManager expr_1A = GameManager.instance;
			expr_1A.bolt -= amount;
			PlayerPrefs.SetInt("GemCollected", GameManager.instance.bolt);
			GameManager.instance.canvasInstance.hudMenuInstance.OnUpdateBolts();
			return true;
		}
		if (shouldOfferShop)
		{
			GameManager.ShowShopPageWithNotEnoughCoinOrBolt(false, true);
		}
		return false;
	}

	public static void RejectOrContinueCountdownFinish()
	{
		GameManager.instance.canvasInstance.HideContinueWithPauseButton(false);
		GameManager.instance.CheckMissions();
	}

	private void ShowContinue()
	{
		GameManager.instance.canvasInstance.ShowContinue();
	}

	public void PlayerRotateToAngle(int angle)
	{
	}

	public static void PauseGame()
	{
		if (GameManager.CurrentState != GameManager.GameState.PlayState)
		{
			return;
		}
		SoundManager.PlayTapSFX();
		SoundManager.PauseLoopSFX();
		GameManager.instance.state = GameManager.GameState.PauseState;
		GameManager.instance.canvasInstance.ShowPauseMenu();
		GameManager.instance.missionManagerInstance.ShowMissionBanner();
		Time.timeScale = 0f;
	}

	public void OnPressPauseGame()
	{
		SoundManager.PlayTapSFX();
		GameManager.PauseGame();
	}

	private void StartCountingDownToResume()
	{
		GameManager.instance.countdownInstance.gameObject.SetActive(true);
		GameManager.instance.countdownInstance.StartCountingDown();
	}

	public static void CountdownResumeCompleted()
	{
		GameManager.instance.playerInstance.ResetJoystickStarted();
		GameManager.instance.state = GameManager.GameState.PlayState;
		Time.timeScale = 1f;
	}

	public static void ResumeGame()
	{
		SoundManager.UnPauseLoopSFX();
		GameManager.instance.StartCountingDownToResume();
		GameManager.instance.canvasInstance.HidePauseMenu();
		GameManager.instance.missionManagerInstance.HideMissionBanner();
	}

	public static void ChangeToPlayState()
	{
		GameManager.instance.state = GameManager.GameState.PlayState;
		GameManager.instance.sidekickManagerInstance.StartShooting();
	}

	public static void ContinueGame()
	{
		GameManager.instance.canvasInstance.HideContinueWithPauseButton(true);
		GameManager.instance.StartCoroutine("ContinueGameSequence");
	}

	private IEnumerator ContinueGameSequence()
	{
		return new GameManager._ContinueGameSequence_c__Iterator0();
	}
}
