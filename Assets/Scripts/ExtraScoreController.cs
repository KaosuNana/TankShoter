using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ExtraScoreController : MonoBehaviour
{
	public enum ScoreType
	{
		Damage,
		Coin
	}

	private Text scoreText;

	public Color[] colorList;

	private void Awake()
	{
		this.scoreText = base.GetComponent<Text>();
	}

	private void OnEnable()
	{
		base.transform.localScale = Vector3.zero;
		base.transform.DOScale(new Vector3(0.015f, 0.015f, 0.015f), 0.1f).OnStart(delegate
		{
			base.transform.DOBlendableLocalMoveBy(new Vector3(0f, 3f, 0f), 1f, false);
		}).OnComplete(delegate
		{
			base.transform.DOScale(Vector3.zero, 0.1f).SetDelay(0.5f).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		});
	}

	public void SetText(string extraScoreText)
	{
		this.scoreText.text = extraScoreText;
	}

	public void SetColor(ExtraScoreController.ScoreType scoreType)
	{
		this.scoreText.color = this.colorList[(int)scoreType];
	}
}
