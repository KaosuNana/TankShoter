using DG.Tweening;
using System;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
	public void TransitionIn()
	{
		base.GetComponent<RectTransform>().localScale = Vector3.one * 4f;
		base.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutQuart).SetDelay(3.25f).OnComplete(delegate
		{
			GameManager.CurrentState = GameManager.GameState.MenuState;
			base.gameObject.SetActive(false);
		});
	}
}
