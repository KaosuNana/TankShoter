using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using I2.Loc;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HUDMenuController : MonoBehaviour
{
	private sealed class _UpdateChargeBar_c__AnonStorey0
	{
		private sealed class _UpdateChargeBar_c__AnonStorey1
		{
			internal float myTimeScale;

			internal HUDMenuController._UpdateChargeBar_c__AnonStorey0 __f__ref_0;

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

		internal float progress;

		internal HUDMenuController _this;

		private static TweenCallback __f__am_cache0;

		internal void __m__0()
		{
			if (this.progress >= 1f)
			{
				SoundManager.PlaySFXInArray(this._this.chargeBarFullSFX, this._this.transform.position, 1f);
				this._this.chargeBarGlowImage.gameObject.SetActive(true);
				this._this.chargeBarGlowImage.DOKill(false);
				this._this.chargeBarGlowImage.DOFade(0f, 0f).SetUpdate(!this._this.hasCompletedTutorial).OnComplete(delegate
				{
					this._this.chargeBarGlowImage.DOFade(0.5f, 0.2f).SetUpdate(!this._this.hasCompletedTutorial).SetLoops(-1, LoopType.Yoyo);
				});
				if (!this._this.hasCompletedTutorial)
				{
					EventManager.StartListening("ReleaseFingerToReleaseSPC", new UnityAction(this._this.ReleaseFingerTriggered));
					float myTimeScale = 1f;
					this._this.tutorialOverlay.DOFade(0.5f, 0.2f).SetUpdate(true);
					this._this.tutorialOverlay.gameObject.SetActive(true);
					this._this.slowDownTween = DOTween.To(() => myTimeScale, delegate(float x)
					{
						myTimeScale = x;
					}, 0f, 0.2f).SetUpdate(true).OnUpdate(delegate
					{
						Time.timeScale = myTimeScale;
					}).OnComplete(delegate
					{
						GameManager.CurrentState = GameManager.GameState.TutorialState;
						SoundManager.PauseLoopSFX();
					});
					this._this.tutorialObject.SetActive(true);
				}
			}
		}

		internal void __m__1()
		{
			this._this.chargeBarGlowImage.DOFade(0.5f, 0.2f).SetUpdate(!this._this.hasCompletedTutorial).SetLoops(-1, LoopType.Yoyo);
		}

		private static void __m__2()
		{
			GameManager.CurrentState = GameManager.GameState.TutorialState;
			SoundManager.PauseLoopSFX();
		}
	}

	public Text coinText;

	public Text boltText;

	public Text levelText;

	public GameObject[] coinAndPauseHolder;

	public Text[] coinTextList;

	public GameObject[] boltHolder;

	public Text[] boltTextList;

	public GameObject extraPaddingForHealthBar;

	public Image playerHUDImage;

	public Button pauseButton;

	public Image chargeBarImage;

	public Image chargeBarGlowImage;

	private Color originalChargeBarColor;

	public Image freezeImage;

	public Image hurtImage;

	public GameObject coinBurstParticle;

	public Text coinBurstText;

	public GameObject coinBurstObject;

	public GameObject boltBurstParticle;

	public Text boltBurstText;

	public GameObject boltBurstObject;

	public AudioClip coinBurstSFX;

	private bool chargeBarFull;

	private int currentCoin;

	private int currentBolt;

	public AudioClip chargeBarFullSFX;

	public GameObject tutorialObject;

	public Image tutorialOverlay;

	private bool hasCompletedTutorial;

	private Tween slowDownTween;

	public void OnShowPauseHUD()
	{
		this.HidePauseButton();
	}

	private void Awake()
	{
		this.tutorialObject.SetActive(false);
		this.tutorialOverlay.DOFade(0f, 0f).SetUpdate(true);
		this.tutorialOverlay.gameObject.SetActive(false);
		this.originalChargeBarColor = this.chargeBarImage.color;
		this.chargeBarImage.fillAmount = 0f;
		this.chargeBarGlowImage.gameObject.SetActive(false);
		this.currentCoin = GameManager.Coin;
		this.currentBolt = GameManager.Bolt;
		this.hasCompletedTutorial = PlayerPrefsX.GetBool("HasCompletedTutorial", false);
	}

	private void Start()
	{
		base.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 200f), 0.5f, false);
		if (GameSingleton.IsiPhoneX)
		{
			this.coinText = this.coinTextList[1];
			this.boltText = this.boltTextList[1];
			this.coinAndPauseHolder[0].SetActive(false);
			this.coinAndPauseHolder[1].SetActive(true);
			this.boltHolder[0].SetActive(false);
			this.boltHolder[1].SetActive(true);
			this.extraPaddingForHealthBar.SetActive(true);
		}
		else
		{
			this.coinText = this.coinTextList[0];
			this.boltText = this.boltTextList[0];
			this.coinAndPauseHolder[0].SetActive(true);
			this.coinAndPauseHolder[1].SetActive(false);
			this.boltHolder[0].SetActive(true);
			this.boltHolder[1].SetActive(false);
			this.extraPaddingForHealthBar.SetActive(false);
		}
	}

	public void OnShowGamePlayHUD()
	{
		this.pauseButton.gameObject.SetActive(true);
		base.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 0f), 0.5f, false);
		this.ShowPauseButton();
	}

	public void UpdateChargeBar(float progress, float duration)
	{
		if (progress == 0f)
		{
			this.chargeBarGlowImage.DOKill(false);
			this.chargeBarGlowImage.gameObject.SetActive(false);
			this.chargeBarFull = false;
		}
		else if (progress >= 1f)
		{
			if (this.chargeBarFull)
			{
				return;
			}
			this.chargeBarFull = true;
		}
		this.chargeBarImage.DOKill(false);
		this.chargeBarImage.color = this.originalChargeBarColor;
		this.chargeBarImage.DOFillAmount(Mathf.Min(progress, 1f), duration).OnComplete(delegate
		{
			if (progress >= 1f)
			{
				SoundManager.PlaySFXInArray(this.chargeBarFullSFX, this.transform.position, 1f);
				this.chargeBarGlowImage.gameObject.SetActive(true);
				this.chargeBarGlowImage.DOKill(false);
				this.chargeBarGlowImage.DOFade(0f, 0f).SetUpdate(!this.hasCompletedTutorial).OnComplete(delegate
				{
					this.chargeBarGlowImage.DOFade(0.5f, 0.2f).SetUpdate(!this.hasCompletedTutorial).SetLoops(-1, LoopType.Yoyo);
				});
				if (!this.hasCompletedTutorial)
				{
					EventManager.StartListening("ReleaseFingerToReleaseSPC", new UnityAction(this.ReleaseFingerTriggered));
					float myTimeScale = 1f;
					this.tutorialOverlay.DOFade(0.5f, 0.2f).SetUpdate(true);
					this.tutorialOverlay.gameObject.SetActive(true);
					this.slowDownTween = DOTween.To(() => myTimeScale, delegate(float x)
					{
						myTimeScale = x;
					}, 0f, 0.2f).SetUpdate(true).OnUpdate(delegate
					{
						Time.timeScale = myTimeScale;
					}).OnComplete(delegate
					{
						GameManager.CurrentState = GameManager.GameState.TutorialState;
						SoundManager.PauseLoopSFX();
					});
					this.tutorialObject.SetActive(true);
				}
			}
		});
	}

	private void ReleaseFingerTriggered()
	{
		if (this.slowDownTween != null)
		{
			this.slowDownTween.Kill(false);
		}
		GameManager.CurrentState = GameManager.GameState.PlayState;
		Time.timeScale = 1f;
		this.tutorialObject.SetActive(false);
		this.tutorialOverlay.DOFade(0f, 0f).SetUpdate(true);
		this.tutorialOverlay.gameObject.SetActive(false);
		this.hasCompletedTutorial = true;
		PlayerPrefsX.SetBool("HasCompletedTutorial", this.hasCompletedTutorial);
	}

	public void FreezeEffects()
	{
		this.freezeImage.gameObject.SetActive(true);
		this.freezeImage.DOFade(0.25f, 0.5f);
	}

	public void HurtEffects()
	{
		this.hurtImage.gameObject.SetActive(true);
		this.hurtImage.DOFade(0.5f, 0.1f).SetUpdate(true).OnComplete(delegate
		{
			this.hurtImage.DOFade(0f, 0.1f).SetDelay(0.1f).SetUpdate(true).OnComplete(delegate
			{
				this.hurtImage.gameObject.SetActive(false);
			});
		});
	}

	public void NormalEffects()
	{
		this.freezeImage.DOFade(0f, 0.5f).OnComplete(delegate
		{
			this.freezeImage.gameObject.SetActive(false);
		});
	}

	public void ShowBoltBurstParticle(int amount)
	{
		SoundManager.PlaySFXInArray(this.coinBurstSFX, base.transform.position, 1f);
		this.boltBurstParticle.SetActive(true);
		short num = (short)Mathf.Min(100, amount);
		ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, num, num, 1, 0.01f);
		this.boltBurstParticle.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[]
		{
			burst
		});
		this.boltBurstText.text = GameManager.CurrencyToString((float)amount);
		this.boltBurstObject.SetActive(true);
		this.boltBurstObject.transform.localScale = Vector3.zero;
		this.boltBurstObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce).OnComplete(delegate
		{
			this.boltBurstObject.transform.DOScale(Vector3.zero, 0.2f).SetDelay(0.8f).SetEase(Ease.InBounce);
		});
	}

	public void ShowCoinBurstParticle(int amount)
	{
		SoundManager.PlaySFXInArray(this.coinBurstSFX, base.transform.position, 1f);
		this.coinBurstParticle.SetActive(true);
		short num = (short)Mathf.Min(100, amount);
		ParticleSystem.Burst burst = new ParticleSystem.Burst(0f, num, num, 1, 0.01f);
		this.coinBurstParticle.GetComponent<ParticleSystem>().emission.SetBursts(new ParticleSystem.Burst[]
		{
			burst
		});
		this.coinBurstText.text = GameManager.CurrencyToString((float)amount);
		this.coinBurstObject.SetActive(true);
		this.coinBurstObject.transform.localScale = Vector3.zero;
		this.coinBurstObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce).OnComplete(delegate
		{
			this.coinBurstObject.transform.DOScale(Vector3.zero, 0.2f).SetDelay(0.8f).SetEase(Ease.InBounce);
		});
	}

	public void OnShowMainMenuHUD()
	{
	}

	public void OnUpdateBolts()
	{
		DOTween.To(() => this.currentBolt, delegate(int x)
		{
			this.currentBolt = x;
		}, GameManager.Bolt, 0.2f).OnUpdate(delegate
		{
			this.boltText.text = GameManager.CurrencyToString((float)this.currentBolt);
		});
	}

	public void OnUpdateCoin()
	{
		DOTween.To(() => this.currentCoin, delegate(int x)
		{
			this.currentCoin = x;
		}, GameManager.Coin, 0.2f).OnUpdate(delegate
		{
			this.coinText.text = GameManager.CurrencyToString((float)this.currentCoin);
		});
	}

	public void ShowPauseButton()
	{
		this.pauseButton.GetComponent<Image>().DOKill(false);
		this.pauseButton.GetComponent<Image>().DOFade(1f, 0.2f);
	}

	public void HidePauseButton()
	{
		this.pauseButton.GetComponent<Image>().DOKill(false);
		this.pauseButton.GetComponent<Image>().DOFade(0f, 0.2f).SetUpdate(true);
	}

	public void UpdateLevelTo(string levelInfo)
	{
		this.levelText.text = ScriptLocalization.Get("LEVEL").ToUpper() + " " + levelInfo;
	}
}
