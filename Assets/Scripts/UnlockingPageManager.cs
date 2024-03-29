using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UnlockingPageManager : MonoBehaviour
{
	public delegate void CallBackFunction();

	private sealed class _Unlock_c__AnonStorey0
	{
		internal bool isLastOutput;

		internal UnlockingPageManager _this;

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
			this._this.resultObject.GetComponent<ChestReward>().ActionAfterReward();
		}
	}

	public GameObject overlayImage;

	public GameObject confettiEffects;

	public GameObject shines;

	public GameObject researchSlot;

	public GameObject resultSlot;

	public Text currencyText;

	public Text researchText;

	public Text gemText;

	public Text pilotText;

	public GameObject progress;

	public Image progressImage;

	public Image levelUpImage;

	public Text levelText;

	public Text experienceProgressText;

	public GameObject newText;

	public GameObject FX;

	public List<ChestReward> outputReward;

	public Button OKButton;

	private GameObject chestObject;

	private GameObject resultObject;

	private bool finishAnimating;

	private int currentUnlock;

	public AudioClip currencyRevealSFX;

	public AudioClip countingUpGemSFX;

	public AudioClip countingUpSFX;

	protected UnlockingPageManager.CallBackFunction callbackFct;

	private void Start()
	{
	}

	public void ShowChest(GameObject chestToUnlock, UnlockingPageManager.CallBackFunction myCallbackFct)
	{
		this.currentUnlock = 0;
		this.newText.SetActive(false);
		this.pilotText.gameObject.SetActive(false);
		this.gemText.gameObject.SetActive(false);
		this.currencyText.gameObject.SetActive(false);
		this.researchText.gameObject.SetActive(false);
		this.overlayImage.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		this.overlayImage.GetComponent<Image>().DOFade(0.784313738f, 0.5f);
		this.callbackFct = myCallbackFct;
		this.OKButton.gameObject.SetActive(false);
		this.OKButton.transform.localScale = Vector3.zero;
		this.currencyText.gameObject.SetActive(false);
		this.chestObject = UnityEngine.Object.Instantiate<GameObject>(chestToUnlock);
		this.chestObject.transform.SetParent(this.researchSlot.transform, false);
		this.chestObject.transform.localScale = Vector3.zero;
		this.chestObject.transform.localPosition = Vector3.zero;
		this.finishAnimating = false;
		this.UpdateOutputRewardBasedOnProbability();
		UnlockingPageManager.SetLayerOnAllRecursive(this.chestObject, LayerMask.NameToLayer("MenuObject"));
		this.chestObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(0.5f).OnComplete(delegate
		{
			this.chestObject.GetComponent<Chest>().Bouncing(true);
			this.finishAnimating = true;
		});
	}

	private void UpdateOutputRewardBasedOnProbability()
	{
		this.outputReward = new List<ChestReward>();
		for (int i = 0; i < this.chestObject.GetComponent<Chest>().outputPrefab.Length; i++)
		{
			if (UnityEngine.Random.Range(0f, 1f) < this.chestObject.GetComponent<Chest>().outputPrefab[i].probability)
			{
				this.outputReward.Add(this.chestObject.GetComponent<Chest>().outputPrefab[i]);
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
		this.chestObject.transform.DOScale(Vector3.one * 1.1f, 0.3f).SetLoops(2, LoopType.Yoyo);
		this.chestObject.transform.DOJump(this.chestObject.transform.position, 0.5f, 1, 0.3f, false);
		this.Unlock(this.outputReward[this.currentUnlock].gameObject, this.currentUnlock == this.outputReward.Count - 1);
		this.currentUnlock++;
	}

	public void Unlock(GameObject obj, bool isLastOutput)
	{
		UnityEngine.Debug.Log("islas output " + isLastOutput);
		this.currencyText.GetComponent<RectTransform>().localScale = Vector3.zero;
		this.FX.SetActive(false);
		this.progress.SetActive(false);
		this.pilotText.gameObject.SetActive(false);
		this.gemText.gameObject.SetActive(false);
		this.currencyText.gameObject.SetActive(false);
		this.researchText.gameObject.SetActive(false);
		this.newText.SetActive(false);
		if (this.resultObject != null)
		{
			UnityEngine.Object.Destroy(this.resultObject);
		}
		this.resultObject = UnityEngine.Object.Instantiate<GameObject>(obj);
		this.resultObject.transform.SetParent(this.resultSlot.transform, false);
		this.resultObject.transform.localScale = Vector3.zero;
		this.resultObject.transform.localPosition = Vector3.zero;
		UnlockingPageManager.SetLayerOnAllRecursive(this.resultObject, LayerMask.NameToLayer("MenuObject"));
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
				this.resultObject.GetComponent<ChestReward>().ActionAfterReward();
			});
		}).OnComplete(delegate
		{
			UnityEngine.Debug.Log("completed FX Image Fade on up");
			this.FX.GetComponent<Image>().DOFade(0f, 0.9f);
		});
	}

	private void AnimateAmount(int amount, string rewardName)
	{
	}

	private void ResetAll()
	{
		this.confettiEffects.SetActive(false);
		this.shines.transform.DOKill(false);
	}

	public void OnOKButtonPressed()
	{
		this.callbackFct();
		UnityEngine.Object.Destroy(this.resultObject);
		UnityEngine.Object.Destroy(this.chestObject);
		this.ResetAll();
		base.gameObject.SetActive(false);
	}

	private static void SetLayerOnAllRecursive(GameObject obj, int layer)
	{
		obj.layer = layer;
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				UnlockingPageManager.SetLayerOnAllRecursive(transform.gameObject, layer);
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

	private void Update()
	{
		if (Input.GetMouseButton(0) && this.finishAnimating)
		{
			if (this.chestObject.GetComponent<Chest>().isBouncing)
			{
				this.chestObject.GetComponent<Chest>().OpenChestWithSFX(true);
				base.Invoke("UnlockNext", 0f);
			}
			else
			{
				this.UnlockNext();
			}
		}
	}
}
