using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionTimerController : MonoBehaviour
{
	private Text timerText;

	private float currentTime;

	private void Awake()
	{
		this.timerText = base.GetComponent<Text>();
	}

	public void ShowTimer()
	{
		if (this.currentTime > 0f)
		{
			base.gameObject.SetActive(true);
		}
	}

	public void SetTimer(float timer)
	{
		this.currentTime = timer;
	}

	private void Update()
	{
		if (GameManager.CurrentState == GameManager.GameState.PlayState && this.currentTime > 0f)
		{
			this.currentTime -= Time.deltaTime;
			this.timerText.text = Math.Round((double)Mathf.Max(this.currentTime, 0f), 1).ToString();
			if (this.currentTime <= 0f)
			{
				EventManager.TriggerEvent("HpTimed");
				base.gameObject.SetActive(false);
			}
		}
	}
}
