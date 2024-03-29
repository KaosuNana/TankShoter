using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ChestUnlockPageManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	private sealed class _Unlock_c__AnonStorey0
	{
		internal bool isLastOutput;

		internal ChestUnlockPageManager _this;

		internal void __m__0()
		{
			this._this.confettiEffects.SetActive(true);
			this._this.confettiEffects.GetComponent<ParticleSystem>().Play();
			if (this.isLastOutput)
			{
				this._this.OKButton.gameObject.SetActive(true);
				this._this.OKButton.transform.DOScale(Vector3.one, 0.2f).SetDelay(1.5f);
			}
			this._this.resultObject.transform.DOBlendableLocalRotateBy(new Vector3(0f, 1800f, 0f), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
			this._this.resultObject.transform.DOLocalJump(new Vector3(this._this.resultObject.transform.localPosition.x, 1.5f, this._this.resultObject.transform.localPosition.z), 1.75f, 1, 0.7f, false);
			this._this.resultObject.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBack).OnComplete(delegate
			{
				UnityEngine.Debug.Log("completed result object scale up");
				this._this.resultObject.transform.DOKill(false);
				this._this.resultObject.transform.DOBlendableLocalRotateBy(new Vector3(0f, 360f, 0f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
				if (this._this.resultObject.GetComponent<ChestRewardVehicleCard>() != null)
				{
					this._this.AnimateAmount(this._this.vehicleCardText, this._this.resultObject.GetComponent<ChestReward>().GetAmount(), this._this.resultObject.GetComponent<ChestReward>().rewardName);
				}
				else
				{
					this._this.AnimateAmount(this._this.researchText, this._this.resultObject.GetComponent<ChestReward>().GetAmount(), this._this.resultObject.GetComponent<ChestReward>().rewardName);
				}
				this._this.resultObject.GetComponent<ChestReward>().ActionAfterReward();
			});
		}

		internal void __m__1()
		{
			UnityEngine.Debug.Log("completed FX Image Fade on up");
			this._this.FX.GetComponent<Image>().DOFade(0f, 0.9f);
		}

		internal void __m__2()
		{
			UnityEngine.Debug.Log("completed result object scale up");
			this._this.resultObject.transform.DOKill(false);
			this._this.resultObject.transform.DOBlendableLocalRotateBy(new Vector3(0f, 360f, 0f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
			if (this._this.resultObject.GetComponent<ChestRewardVehicleCard>() != null)
			{
				this._this.AnimateAmount(this._this.vehicleCardText, this._this.resultObject.GetComponent<ChestReward>().GetAmount(), this._this.resultObject.GetComponent<ChestReward>().rewardName);
			}
			else
			{
				this._this.AnimateAmount(this._this.researchText, this._this.resultObject.GetComponent<ChestReward>().GetAmount(), this._this.resultObject.GetComponent<ChestReward>().rewardName);
			}
			this._this.resultObject.GetComponent<ChestReward>().ActionAfterReward();
		}
	}

	private sealed class _AnimateAmount_c__AnonStorey1
	{
		internal int amount;

		internal int tempAmount;

		internal string rewardName;

		internal Text tempText;

		internal ChestUnlockPageManager _this;

		internal void __m__0()
		{
			DOTween.To(() => this.tempAmount, delegate(int x)
			{
				this.tempAmount = x;
			}, this.amount, 0.25f * Time.timeScale).SetDelay(0.2f).OnStart(delegate
			{
				SoundManager.PlaySFXInArray(this._this.countingUpGemSFX, this._this.transform.position, 1f);
			}).OnComplete(delegate
			{
				this._this.finishAnimating = true;
			}).OnUpdate(delegate
			{
				this.tempText.text = this.rewardName + " x " + this.tempAmount.ToString();
			});
		}

		internal int __m__1()
		{
			return this.tempAmount;
		}

		internal void __m__2(int x)
		{
			this.tempAmount = x;
		}

		internal void __m__3()
		{
			SoundManager.PlaySFXInArray(this._this.countingUpGemSFX, this._this.transform.position, 1f);
		}

		internal void __m__4()
		{
			this._this.finishAnimating = true;
		}

		internal void __m__5()
		{
			this.tempText.text = this.rewardName + " x " + this.tempAmount.ToString();
		}
	}

	private VehicleManager vehicleManagerInstance;

	public GameObject overlayImage;

	public GameObject confettiEffects;

	public GameObject shines;

	public Chest[] chestList;

	public GameObject resultSlot;

	public Text currencyText;

	public Text researchText;

	public Text gemText;

	public Text vehicleCardText;

	public GameObject progress;

	public Image progressImage;

	public Image levelUpImage;

	public Text levelText;

	public Text experienceProgressText;

	public GameObject newText;

	public GameObject FX;

	public List<ChestReward> outputReward;

	public Button OKButton;

	private Chest chestInstance;

	private GameObject resultObject;

	private bool finishAnimating;

	private int currentUnlock;

	public AudioClip currencyRevealSFX;

	public AudioClip countingUpGemSFX;

	public AudioClip countingUpSFX;

	protected ChestUnlockPageManager.CallBackFunction callbackFct;

	public Chest.ChestType chestTypeTestingOnly;

	private void Awake()
	{
		this.vehicleManagerInstance = GameManager.instance.vehicleManagerInstance;
	}

	private void PlayCurrencyRevealSFX()
	{
		SoundManager.PlaySFXInArray(this.currencyRevealSFX, base.transform.position, 1f);
	}

	private void Reset()
	{
		this.currentUnlock = 0;
		this.newText.SetActive(false);
		this.vehicleCardText.gameObject.SetActive(false);
		this.gemText.gameObject.SetActive(false);
		this.currencyText.gameObject.SetActive(false);
		this.researchText.gameObject.SetActive(false);
		this.overlayImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		this.OKButton.gameObject.SetActive(false);
		this.OKButton.transform.localScale = Vector3.zero;
		this.finishAnimating = false;
	}

	public void ShowChest(Chest.ChestType chestType, ChestUnlockPageManager.CallBackFunction myCallbackFct)
	{
		this.Reset();
		this.overlayImage.GetComponent<Image>().DOFade(0.784313738f, 0.5f);
		this.callbackFct = myCallbackFct;
		this.chestInstance = this.chestList[(int)chestType];
		this.chestInstance.gameObject.SetActive(true);
		this.chestInstance.transform.localScale = Vector3.zero;
		this.chestInstance.transform.localPosition = Vector3.zero;
		this.UpdateOutputRewardBasedOnProbability();
		this.chestInstance.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f).OnComplete(delegate
		{
			this.chestInstance.Bouncing(true);
			this.finishAnimating = true;
		});
	}

	private void UpdateOutputRewardBasedOnProbability()
	{
		this.outputReward = new List<ChestReward>();
		for (int i = 0; i < this.chestInstance.outputPrefab.Length; i++)
		{
			if (UnityEngine.Random.Range(0f, 1f) < this.chestInstance.outputPrefab[i].probability)
			{
				this.outputReward.Add(this.chestInstance.outputPrefab[i]);
			}
		}
	}

	private void UnlockNext()
	{
		if (this.currentUnlock >= this.outputReward.Count)
		{
			return;
		}
		UnityEngine.Debug.Log("current unlock is " + this.currentUnlock);
		this.finishAnimating = false;
		this.chestInstance.transform.DOScale(Vector3.one * 1.1f, 0.3f).SetLoops(2, LoopType.Yoyo);
		this.chestInstance.transform.DOJump(this.chestInstance.transform.position, 0.5f, 1, 0.3f, false);
		this.Unlock(this.outputReward[this.currentUnlock].gameObject, this.currentUnlock == this.outputReward.Count - 1);
		this.currentUnlock++;
	}

	public void Unlock(GameObject obj, bool isLastOutput)
	{
		UnityEngine.Debug.Log("islas output " + isLastOutput);
		this.currencyText.GetComponent<RectTransform>().localScale = Vector3.zero;
		this.FX.SetActive(false);
		this.progress.SetActive(false);
		this.vehicleCardText.gameObject.SetActive(false);
		this.gemText.gameObject.SetActive(false);
		this.currencyText.gameObject.SetActive(false);
		this.researchText.gameObject.SetActive(false);
		this.newText.SetActive(false);
		if (this.resultObject != null)
		{
			UnityEngine.Object.Destroy(this.resultObject);
		}
		this.resultObject = UnityEngine.Object.Instantiate<GameObject>(obj);
		if (this.resultObject.GetComponent<ChestRewardVehicleCard>() != null)
		{
		}
		this.resultObject.transform.SetParent(this.resultSlot.transform, false);
		this.resultObject.transform.localScale = Vector3.zero;
		this.resultObject.transform.localPosition = Vector3.zero;
		base.Invoke("PlayCurrencyRevealSFX", 0.3f);
		this.FX.SetActive(true);
		this.FX.GetComponent<Image>().DOFade(1f, 0.3f).SetDelay(0.2f).OnStart(delegate
		{
			this.confettiEffects.SetActive(true);
			this.confettiEffects.GetComponent<ParticleSystem>().Play();
			if (isLastOutput)
			{
				this.OKButton.gameObject.SetActive(true);
				this.OKButton.transform.DOScale(Vector3.one, 0.2f).SetDelay(1.5f);
			}
			this.resultObject.transform.DOBlendableLocalRotateBy(new Vector3(0f, 1800f, 0f), 4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
			this.resultObject.transform.DOLocalJump(new Vector3(this.resultObject.transform.localPosition.x, 1.5f, this.resultObject.transform.localPosition.z), 1.75f, 1, 0.7f, false);
			this.resultObject.transform.DOScale(Vector3.one, 0.7f).SetEase(Ease.OutBack).OnComplete(delegate
			{
				UnityEngine.Debug.Log("completed result object scale up");
				this.resultObject.transform.DOKill(false);
				this.resultObject.transform.DOBlendableLocalRotateBy(new Vector3(0f, 360f, 0f), 2f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
				if (this.resultObject.GetComponent<ChestRewardVehicleCard>() != null)
				{
					this.AnimateAmount(this.vehicleCardText, this.resultObject.GetComponent<ChestReward>().GetAmount(), this.resultObject.GetComponent<ChestReward>().rewardName);
				}
				else
				{
					this.AnimateAmount(this.researchText, this.resultObject.GetComponent<ChestReward>().GetAmount(), this.resultObject.GetComponent<ChestReward>().rewardName);
				}
				this.resultObject.GetComponent<ChestReward>().ActionAfterReward();
			});
		}).OnComplete(delegate
		{
			UnityEngine.Debug.Log("completed FX Image Fade on up");
			this.FX.GetComponent<Image>().DOFade(0f, 0.9f);
		});
	}

	private void AnimateAmount(Text tempText, int amount, string rewardName)
	{
		tempText.gameObject.SetActive(true);
		tempText.text = rewardName + " x 0";
		int tempAmount = 0;
		tempText.GetComponent<RectTransform>().DOScale(1f, 0.25f).OnComplete(delegate
		{
			DOTween.To(() => tempAmount, delegate(int x)
			{
				tempAmount = x;
			}, amount, 0.25f * Time.timeScale).SetDelay(0.2f).OnStart(delegate
			{
				SoundManager.PlaySFXInArray(this.countingUpGemSFX, this.transform.position, 1f);
			}).OnComplete(delegate
			{
				this.finishAnimating = true;
			}).OnUpdate(delegate
			{
				tempText.text = rewardName + " x " + tempAmount.ToString();
			});
		});
	}

	private void ResetAll()
	{
		this.confettiEffects.SetActive(false);
		this.shines.transform.DOKill(false);
	}

	public void OnOKButtonPressed()
	{
		SoundManager.PlayTapSFX();
		UnityEngine.Debug.Log("pressed ok button");
		UnityEngine.Object.Destroy(this.resultObject);
		this.chestInstance.ResetAnim();
		this.chestInstance.gameObject.SetActive(false);
		this.ResetAll();
		base.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetMouseButton(0) && this.finishAnimating)
		{
			if (this.chestInstance.isBouncing)
			{
				this.chestInstance.OpenChestWithSFX(true);
				base.Invoke("UnlockNext", 0f);
			}
			else
			{
				this.UnlockNext();
			}
		}
	}
}
