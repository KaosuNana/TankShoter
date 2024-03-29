using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NextStepManager : MonoBehaviour
{
	public NextStep[] possibleNextSteps;

	public List<NextStep> activeNextSteps;

	private static TweenCallback __f__am_cache0;

	private void Awake()
	{
		this.possibleNextSteps = base.GetComponentsInChildren<NextStep>(true);
	}

	public void DecideNextSteps()
	{
		this.activeNextSteps = new List<NextStep>();
		for (int i = 0; i < this.possibleNextSteps.Length; i++)
		{
			if (this.possibleNextSteps[i].ShouldShow())
			{
				this.possibleNextSteps[i].gameObject.SetActive(true);
				this.activeNextSteps.Add(this.possibleNextSteps[i]);
				if (this.activeNextSteps.Count == 3)
				{
					return;
				}
			}
			else
			{
				this.possibleNextSteps[i].gameObject.SetActive(false);
			}
		}
	}

	public void UpdateNextStepDoubleEarningCoin()
	{
		for (int i = 0; i < this.possibleNextSteps.Length; i++)
		{
			if (this.possibleNextSteps[i].GetComponent<NextStepDoubleEarnings>())
			{
				this.possibleNextSteps[i].GetComponent<NextStepDoubleEarnings>().UpdateCoins();
				return;
			}
		}
	}

	public void ShowNextStepDoubleEarnings()
	{
		int i = 0;
		while (i < this.possibleNextSteps.Length)
		{
			if (this.possibleNextSteps[i].GetComponent<NextStepDoubleEarnings>())
			{
				this.possibleNextSteps[i].GetComponent<NextStepDoubleEarnings>().UpdateCoins();
				if (this.possibleNextSteps[i].gameObject.activeSelf)
				{
					return;
				}
				this.possibleNextSteps[i].gameObject.SetActive(true);
				Vector2 anchoredPosition = this.possibleNextSteps[i].GetComponent<RectTransform>().anchoredPosition;
				anchoredPosition.x = -1000f;
				this.possibleNextSteps[i].GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
				this.possibleNextSteps[i].GetComponent<RectTransform>().DOAnchorPosX(0f, 0.25f, false).SetEase(Ease.OutBack);
				return;
			}
			else
			{
				i++;
			}
		}
	}

	public void HideNextSteps()
	{
		for (int i = 0; i < this.activeNextSteps.Count; i++)
		{
			this.activeNextSteps[i].GetComponent<RectTransform>().DOAnchorPosX(2000f, 0.2f, false).SetEase(Ease.InBack).SetDelay(0.25f + (float)i * 0.1f).OnStart(delegate
			{
				SoundManager.PlaySwooshSFX();
			});
		}
	}

	public void AnimateNextStep()
	{
		for (int i = 0; i < this.activeNextSteps.Count; i++)
		{
			this.activeNextSteps[i].SlidesInWithDelay(0.25f + (float)i * 0.1f);
		}
	}
}
