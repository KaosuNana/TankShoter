using DG.Tweening;
using I2.Loc;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BossIncomingOverlayController : MonoBehaviour
{
	public Text[] blinkingText;

	public RectTransform topLines;

	public RectTransform bottomLines;

	public Image overlayImage;

	private CanvasGroup canvasGroup;

	public AudioClip warningSFX;

	public Text levelText;

	private static TweenCallback __f__am_cache0;

	private void Awake()
	{
		this.canvasGroup = base.GetComponent<CanvasGroup>();
	}

	private void OnEnable()
	{
		this.levelText.text = ScriptLocalization.Get("LEVEL") + " " + GameManager.CurrentLevel;
	}

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			Vector2 anchoredPosition = this.topLines.anchoredPosition;
			anchoredPosition.y = -80f;
			this.topLines.anchoredPosition = anchoredPosition;
		}
	}

	public void StartBlinkingTexts()
	{
		this.canvasGroup.alpha = 0f;
		this.topLines.DOAnchorPosX(0f, 0.01f, false);
		this.bottomLines.DOAnchorPosX(0f, 0.01f, false);
		DOTween.Sequence().SetLoops(3, LoopType.Restart).AppendCallback(delegate
		{
			SoundManager.PlaySFXInArray(this.warningSFX, base.transform.position, 1f);
		}).AppendInterval(0.8f);
		this.canvasGroup.DOFade(1f, 0.5f).OnComplete(delegate
		{
			this.topLines.DOAnchorPosX(1800f, 30f, false);
			this.bottomLines.DOAnchorPosX(-1800f, 30f, false);
			this.overlayImage.DOFade(0.15f, 0.5f).SetLoops(5, LoopType.Yoyo);
			for (int i = 0; i < this.blinkingText.Length; i++)
			{
				this.blinkingText[i].DOFade(0f, 0.3f).SetLoops(6, LoopType.Yoyo).OnStepComplete(delegate
				{
				}).OnComplete(delegate
				{
					this.canvasGroup.DOFade(0f, 0.25f).OnComplete(delegate
					{
						base.gameObject.SetActive(false);
					});
				});
			}
		});
	}

	private void PlayWarningSFX()
	{
	}
}
