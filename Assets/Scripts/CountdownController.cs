using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
	public Text countdownText;

	public void StartCountingDown()
	{
		this.countdownText.text = "3";
		this.countdownText.transform.localScale = Vector3.one;
		this.countdownText.transform.DOScale(Vector3.zero, 0.7f).SetUpdate(true).OnComplete(delegate
		{
			this.countdownText.transform.localScale = Vector3.one;
			this.countdownText.text = "2";
			this.countdownText.transform.DOScale(Vector3.zero, 0.7f).SetUpdate(true).OnComplete(delegate
			{
				this.countdownText.transform.localScale = Vector3.one;
				this.countdownText.text = "1";
				this.countdownText.transform.DOScale(Vector3.zero, 0.7f).SetUpdate(true).OnComplete(delegate
				{
					GameManager.CountdownResumeCompleted();
					base.gameObject.SetActive(false);
				});
			});
		});
	}
}
