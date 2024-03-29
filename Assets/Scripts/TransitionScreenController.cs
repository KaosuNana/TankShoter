using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TransitionScreenController : MonoBehaviour
{
	private sealed class _TransitionOutWithRestart_c__AnonStorey0
	{
		internal bool shouldRestart;

		internal void __m__0()
		{
			if (this.shouldRestart)
			{
				GameManager.RestartGame();
			}
		}
	}

	private void Awake()
	{
	}

	public void TransitionOutWithRestart(bool shouldRestart)
	{
		base.GetComponent<RectTransform>().localScale = Vector3.one * 0f;
		base.GetComponent<RectTransform>().DOScale(Vector3.one * 4f, 0.5f).SetEase(Ease.InOutQuart).OnComplete(delegate
		{
			if (shouldRestart)
			{
				GameManager.RestartGame();
			}
		});
	}

	public void TransitionIn()
	{
		base.GetComponent<RectTransform>().localScale = Vector3.one * 4f;
		base.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuart).SetDelay(0.25f);
	}
}
