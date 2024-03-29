using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using UnityEngine.UI;

public class IdleIncomeManager : MonoBehaviour
{
	private sealed class _ShowIdleScreenWithAway_c__AnonStorey3
	{
		internal int tempCoinAmount;

		internal int tempBoltAmount;

		internal IdleIncomeManager _this;

		internal void __m__0()
		{
			DOTween.To(() => this.tempCoinAmount, delegate(int x)
			{
				this.tempCoinAmount = x;
			}, this._this.idleCoin, 1.25f).SetDelay(0.35f).OnUpdate(delegate
			{
				this._this.earningText[0].text = GameManager.CurrencyToString((float)this.tempCoinAmount);
			}).SetUpdate(true);
		}

		internal void __m__1()
		{
			DOTween.To(() => this.tempBoltAmount, delegate(int x)
			{
				this.tempBoltAmount = x;
			}, this._this.idleBolt, 1.25f).SetDelay(0.35f).OnUpdate(delegate
			{
				this._this.earningText[1].text = GameManager.CurrencyToString((float)this.tempBoltAmount);
			}).SetUpdate(true);
		}

		internal int __m__2()
		{
			return this.tempCoinAmount;
		}

		internal void __m__3(int x)
		{
			this.tempCoinAmount = x;
		}

		internal void __m__4()
		{
			this._this.earningText[0].text = GameManager.CurrencyToString((float)this.tempCoinAmount);
		}

		internal int __m__5()
		{
			return this.tempBoltAmount;
		}

		internal void __m__6(int x)
		{
			this.tempBoltAmount = x;
		}

		internal void __m__7()
		{
			this._this.earningText[1].text = GameManager.CurrencyToString((float)this.tempBoltAmount);
		}
	}

	private sealed class _AnimateBoltBurstParticle_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal IdleIncomeManager _this;

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

		public _AnimateBoltBurstParticle_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Object.Instantiate<GameObject>(this._this.boltBurstParticle);
				this._current = new WaitForSecondsRealtime(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				EventManager.TriggerEvent("EventBoltChanges");
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

	private sealed class _AnimateCoinBurstParticle_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal IdleIncomeManager _this;

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

		public _AnimateCoinBurstParticle_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Object.Instantiate<GameObject>(this._this.coinBurstParticle);
				this._current = new WaitForSecondsRealtime(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				EventManager.TriggerEvent("EventCoinChanges");
				this._current = new WaitForSecondsRealtime(1.8f);
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				GameManager.HideIdleIncomePage();
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

	private sealed class _AnimateBoltToBoltHUD_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		private sealed class _AnimateBoltToBoltHUD_c__AnonStorey4
		{
			internal int tempAmount;

			internal IdleIncomeManager._AnimateBoltToBoltHUD_c__Iterator2 __f__ref_2;

			internal int __m__0()
			{
				return this.tempAmount;
			}

			internal void __m__1(int x)
			{
				this.tempAmount = x;
			}

			internal void __m__2()
			{
				this.__f__ref_2._this.earningText[1].text = this.tempAmount.ToString();
			}
		}

		internal int _i___1;

		internal GameObject _boltTemp___2;

		internal Vector3 _spawnPos___2;

		internal Vector3[] _pathArray___2;

		internal IdleIncomeManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private IdleIncomeManager._AnimateBoltToBoltHUD_c__Iterator2._AnimateBoltToBoltHUD_c__AnonStorey4 _locvar0;

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

		public _AnimateBoltToBoltHUD_c__Iterator2()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._locvar0 = new IdleIncomeManager._AnimateBoltToBoltHUD_c__Iterator2._AnimateBoltToBoltHUD_c__AnonStorey4();
				this._locvar0.__f__ref_2 = this;
				this._locvar0.tempAmount = this._this.idleBolt;
				DOTween.To(new DOGetter<int>(this._locvar0.__m__0), new DOSetter<int>(this._locvar0.__m__1), 0, 1.25f * Time.timeScale).SetDelay(0.35f).OnUpdate(new TweenCallback(this._locvar0.__m__2));
				this._i___1 = 0;
				break;
			case 1u:
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < 10)
			{
				this._boltTemp___2 = UnityEngine.Object.Instantiate<GameObject>(this._this.boltPrefab, this._this.boltSpawnTransform.position, Quaternion.identity);
				this._boltTemp___2.transform.localScale = Vector3.zero;
				this._spawnPos___2 = this._this.boltSpawnTransform.position;
				this._spawnPos___2.y = this._spawnPos___2.y + 1f;
				this._pathArray___2 = new Vector3[]
				{
					this._spawnPos___2,
					this._this.boltImageTransform.position
				};
				this._this.AnimateObjectViaPath(this._boltTemp___2, this._spawnPos___2, this._pathArray___2, -1, this._i___1 == 0, false, "EventBoltChangesNoScaling");
				this._current = new WaitForSecondsRealtime(0.1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			}
			this._PC = -1;
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

	private sealed class _AnimateObjectViaPath_c__AnonStorey5
	{
		internal GameObject obj;

		internal Vector3[] pathArray;

		internal bool shouldAnimateHUD;

		internal string eventTrigger;

		internal bool shouldHidePage;

		internal IdleIncomeManager _this;

		internal void __m__0()
		{
			this.obj.transform.DOPath(this.pathArray, 0.35f, PathType.CatmullRom, PathMode.Full3D, 10, null).SetEase(Ease.InQuad).SetDelay(0.15f).OnComplete(delegate
			{
				this._this.coinImageTransform.transform.DOKill(false);
				this._this.coinImageTransform.transform.DOScale(Vector3.one * 1.35f, 0.05f).OnComplete(delegate
				{
					this._this.coinImageTransform.transform.DOScale(Vector3.one, 0.05f);
				});
				if (this.shouldAnimateHUD)
				{
					EventManager.TriggerEvent(this.eventTrigger);
				}
			}).OnWaypointChange(delegate(int waypoint)
			{
				if (waypoint == 1)
				{
					this.obj.transform.DOScale(Vector3.zero, 0.8f).OnComplete(delegate
					{
						if (this.shouldHidePage)
						{
							this._this.Invoke("HidePage", 0.5f);
						}
						UnityEngine.Object.Destroy(this.obj);
					});
				}
			});
		}

		internal void __m__1()
		{
			this._this.coinImageTransform.transform.DOKill(false);
			this._this.coinImageTransform.transform.DOScale(Vector3.one * 1.35f, 0.05f).OnComplete(delegate
			{
				this._this.coinImageTransform.transform.DOScale(Vector3.one, 0.05f);
			});
			if (this.shouldAnimateHUD)
			{
				EventManager.TriggerEvent(this.eventTrigger);
			}
		}

		internal void __m__2(int waypoint)
		{
			if (waypoint == 1)
			{
				this.obj.transform.DOScale(Vector3.zero, 0.8f).OnComplete(delegate
				{
					if (this.shouldHidePage)
					{
						this._this.Invoke("HidePage", 0.5f);
					}
					UnityEngine.Object.Destroy(this.obj);
				});
			}
		}

		internal void __m__3()
		{
			this._this.coinImageTransform.transform.DOScale(Vector3.one, 0.05f);
		}

		internal void __m__4()
		{
			if (this.shouldHidePage)
			{
				this._this.Invoke("HidePage", 0.5f);
			}
			UnityEngine.Object.Destroy(this.obj);
		}
	}

	public Text titleText;

	public Text feedbackText;

	public Text[] earningText;

	public GameObject[] earningObj;

	public Text awayMessageText;

	public Text awayTimeText;

	public float baseCoinEarningPerSecond;

	public float baseBoltEarningPerMinute;

	public int maxAwayHour;

	public GameObject doubleEarningObj;

	public GameObject okButtonObj;

	public GameObject coinPrefab;

	public GameObject boltPrefab;

	public Transform coinImageTransform;

	public Transform coinSpawnTransform;

	public Transform boltImageTransform;

	public Transform boltSpawnTransform;

	public GameObject coinBurstParticle;

	public GameObject boltBurstParticle;

	public AudioClip coinBurstSFX;

	private int idleCoin;

	private int idleBolt;

	private bool collected;

	private bool maxed;

	public float GetEarningEstimateForTime(float time)
	{
		return time * this.baseCoinEarningPerSecond * (float)GameManager.CoinMultiplier;
	}

	public void ShowIdleScreenWithAway(float inputTimeAway)
	{
		this.feedbackText.gameObject.SetActive(false);
		this.titleText.transform.localScale = Vector3.zero;
		this.titleText.transform.DOKill(false);
		this.titleText.transform.DOScale(Vector3.one * 1f, 0.25f).SetDelay(0.5f).SetEase(Ease.OutBack).SetUpdate(true);
		if (inputTimeAway >= (float)(3600 * this.maxAwayHour))
		{
			this.maxed = true;
			inputTimeAway = (float)(3600 * this.maxAwayHour);
		}
		this.idleCoin = (int)(inputTimeAway * this.baseCoinEarningPerSecond * (float)GameManager.CoinMultiplier);
		this.idleBolt = (int)Mathf.Floor(inputTimeAway / 60f * this.baseBoltEarningPerMinute);
		int tempBoltAmount = 0;
		int tempCoinAmount = 0;
		this.earningText[0].text = tempCoinAmount.ToString();
		this.earningText[1].text = tempBoltAmount.ToString();
		this.earningObj[0].transform.localScale = Vector3.zero;
		this.earningObj[1].transform.localScale = Vector3.zero;
		this.awayMessageText.transform.localScale = Vector3.zero;
		this.awayTimeText.transform.localScale = Vector3.zero;
		this.okButtonObj.transform.localScale = Vector3.zero;
		this.doubleEarningObj.transform.localScale = Vector3.zero;
		this.okButtonObj.gameObject.SetActive(true);
		this.doubleEarningObj.gameObject.SetActive(true);
		this.awayMessageText.transform.DOScale(Vector3.one * 1.05f, 0.25f).SetDelay(0.55f).SetEase(Ease.OutBack).SetUpdate(true);
		this.awayTimeText.transform.DOScale(Vector3.one * 1.05f, 0.25f).SetDelay(0.6f).SetEase(Ease.OutBack).SetUpdate(true);
		this.earningObj[0].transform.DOScale(Vector3.one * 1f, 0.25f).SetDelay(0.65f).SetEase(Ease.OutBack).SetUpdate(true).OnStart(delegate
		{
			DOTween.To(() => tempCoinAmount, delegate(int x)
			{
				tempCoinAmount = x;
			}, this.idleCoin, 1.25f).SetDelay(0.35f).OnUpdate(delegate
			{
				this.earningText[0].text = GameManager.CurrencyToString((float)tempCoinAmount);
			}).SetUpdate(true);
		});
		this.earningObj[1].transform.DOScale(Vector3.one * 1f, 0.25f).SetDelay(0.65f).SetEase(Ease.OutBack).SetUpdate(true).OnStart(delegate
		{
			DOTween.To(() => tempBoltAmount, delegate(int x)
			{
				tempBoltAmount = x;
			}, this.idleBolt, 1.25f).SetDelay(0.35f).OnUpdate(delegate
			{
				this.earningText[1].text = GameManager.CurrencyToString((float)tempBoltAmount);
			}).SetUpdate(true);
		});
		this.okButtonObj.transform.DOScale(Vector3.one * 1f, 0.25f).SetDelay(0.85f).SetEase(Ease.OutBack).SetUpdate(true);
		this.doubleEarningObj.transform.DOScale(Vector3.one * 1f, 0.25f).SetDelay(0.95f).SetEase(Ease.OutBack).SetUpdate(true);
		this.awayMessageText.text = ScriptLocalization.Get("You were away for");
		if (this.maxed)
		{
			this.awayTimeText.text = this.ConvertFloatToTime(inputTimeAway) + " (" + ScriptLocalization.Get("Max").ToUpper() + ")";
		}
		else
		{
			this.awayTimeText.text = this.ConvertFloatToTime(inputTimeAway);
		}
	}

	private void OnDisable()
	{
		this.collected = false;
		this.maxed = false;
		EventManager.StopListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	public void OnPressWatchAdsToDouble()
	{
		SoundManager.PlayTapSFX();
		Analytics.CustomEvent("WatchAdsToDoubleIdleIncome", new Dictionary<string, object>
		{
			{
				"idleCoin",
				this.idleCoin
			},
			{
				"idleBolt",
				this.idleBolt
			}
		});
        /*
		if (HeyZapManager.IsAdReady())
		{
			HeyZapManager.ShowRewardedVideoWithCallBack(new HeyZapManager.CallBackFunction(this.SuccessToDouble), null);
		}
		*/
        SuccessToDouble();
  //       if (AdsControl.Instance.GetRewardAvailable())
  //       {
  //           AdsControl.Instance.PlayDelegateRewardVideo(delegate
  //           {
  //               SuccessToDouble();
  //           });
  //       }
  //       else
		// {
		// 	this.AnimateFeedback();
		// }
	}

	private void AnimateFeedback()
	{
		this.feedbackText.gameObject.SetActive(true);
		this.feedbackText.DOKill(false);
		this.feedbackText.DOFade(0f, 0f);
		this.feedbackText.DOFade(1f, 0.2f).OnComplete(delegate
		{
			this.feedbackText.DOFade(0f, 0.5f).SetDelay(1f).OnComplete(delegate
			{
				this.feedbackText.gameObject.SetActive(false);
			});
		});
	}

	private void SuccessToDouble()
	{
		this.doubleEarningObj.SetActive(false);
		this.okButtonObj.SetActive(false);
		this.idleCoin *= 2;
		this.idleBolt *= 2;
		this.earningText[0].text = this.earningText[0].text + " x2";
		if (this.idleBolt > 0)
		{
			this.earningText[1].text = this.earningText[1].text + " x2";
		}
		this.RewardEarning();
	}

	private void RewardEarning()
	{
		PlayerPrefsX.SetBool("ShouldSendIdleLocalNotification", true);
		GameManager.UpdateIdleTimeStamp();
		this.collected = true;
		GameManager.ExtraCoinExcludeThisGameCoin(this.idleCoin);
		GameManager.ExtraBoltExcludeThisGameBolt(this.idleBolt);
		SoundManager.PlaySFXInArray(this.coinBurstSFX, base.transform.position, 1f);
		base.StartCoroutine("AnimateCoinBurstParticle");
		if (this.idleBolt > 0)
		{
			base.StartCoroutine("AnimateBoltBurstParticle");
		}
	}

	public void OnPressOkButton()
	{
		SoundManager.PlayTapSFX();
		if (this.collected)
		{
			return;
		}
		this.doubleEarningObj.SetActive(false);
		this.okButtonObj.SetActive(false);
		this.RewardEarning();
	}

	private IEnumerator AnimateBoltBurstParticle()
	{
		IdleIncomeManager._AnimateBoltBurstParticle_c__Iterator0 _AnimateBoltBurstParticle_c__Iterator = new IdleIncomeManager._AnimateBoltBurstParticle_c__Iterator0();
		_AnimateBoltBurstParticle_c__Iterator._this = this;
		return _AnimateBoltBurstParticle_c__Iterator;
	}

	private IEnumerator AnimateCoinBurstParticle()
	{
		IdleIncomeManager._AnimateCoinBurstParticle_c__Iterator1 _AnimateCoinBurstParticle_c__Iterator = new IdleIncomeManager._AnimateCoinBurstParticle_c__Iterator1();
		_AnimateCoinBurstParticle_c__Iterator._this = this;
		return _AnimateCoinBurstParticle_c__Iterator;
	}

	private IEnumerator AnimateBoltToBoltHUD()
	{
		IdleIncomeManager._AnimateBoltToBoltHUD_c__Iterator2 _AnimateBoltToBoltHUD_c__Iterator = new IdleIncomeManager._AnimateBoltToBoltHUD_c__Iterator2();
		_AnimateBoltToBoltHUD_c__Iterator._this = this;
		return _AnimateBoltToBoltHUD_c__Iterator;
	}

	private void AnimateObjectViaPath(GameObject obj, Vector3 spawnPos, Vector3[] pathArray, int rotateDirection, bool shouldAnimateHUD, bool shouldHidePage, string eventTrigger)
	{
		obj.transform.localScale = Vector3.zero;
		obj.transform.DOBlendableLocalRotateBy(new Vector3(0f, (float)(720 * rotateDirection), 0f), UnityEngine.Random.Range(1f, 3f), RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
		obj.transform.DOScale(Vector3.one * 1.5f, 0.2f).OnComplete(delegate
		{
			obj.transform.DOPath(pathArray, 0.35f, PathType.CatmullRom, PathMode.Full3D, 10, null).SetEase(Ease.InQuad).SetDelay(0.15f).OnComplete(delegate
			{
				this.coinImageTransform.transform.DOKill(false);
				this.coinImageTransform.transform.DOScale(Vector3.one * 1.35f, 0.05f).OnComplete(delegate
				{
					this.coinImageTransform.transform.DOScale(Vector3.one, 0.05f);
				});
				if (shouldAnimateHUD)
				{
					EventManager.TriggerEvent(eventTrigger);
				}
			}).OnWaypointChange(delegate(int waypoint)
			{
				if (waypoint == 1)
				{
					obj.transform.DOScale(Vector3.zero, 0.8f).OnComplete(delegate
					{
						if (shouldHidePage)
						{
							this.Invoke("HidePage", 0.5f);
						}
						UnityEngine.Object.Destroy(obj);
					});
				}
			});
		});
	}

	private void HidePage()
	{
		GameManager.HideIdleIncomePage();
	}

	private string ConvertFloatToTime(float timer)
	{
		if (timer >= 86400f)
		{
			return string.Format("{0}:{1}:{2:00}:{3:00}", new object[]
			{
				(int)timer / 86400,
				(int)timer / 3600,
				(int)(timer % 3600f) / 60,
				(int)timer % 60
			});
		}
		if (timer >= 3600f)
		{
			return string.Format("{0}:{1:00}:{2:00}", (int)timer / 3600, (int)(timer % 3600f) / 60, (int)timer % 60);
		}
		return string.Format("{0:00}:{1:00}", (int)timer / 60, (int)timer % 60);
	}

	private void OnEnable()
	{
		EventManager.StartListening("EventAndroidBackButton", new UnityAction(this.OnPressBackButtonAndroid));
	}

	private void OnPressBackButtonAndroid()
	{
		this.OnPressOkButton();
	}
}
