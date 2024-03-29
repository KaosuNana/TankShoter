using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoltHUDController : MonoBehaviour
{
	public Text boltsText;

	public Text[] boltTextList;

	public GameObject[] boltHolderList;

	private int currentBolt;

	private void Awake()
	{
		EventManager.StartListening("EventBoltChanges", new UnityAction(this.UpdateBoltsHUD));
	}

	private void Start()
	{
		if (GameSingleton.IsiPhoneX)
		{
			this.boltsText = this.boltTextList[1];
			this.boltHolderList[0].SetActive(false);
			this.boltHolderList[1].SetActive(true);
		}
		else
		{
			this.boltsText = this.boltTextList[0];
			this.boltHolderList[0].SetActive(true);
			this.boltHolderList[1].SetActive(false);
		}
		this.currentBolt = GameManager.Bolt;
		this.boltsText.gameObject.SetActive(true);
		this.boltsText.text = GameManager.CurrencyToString((float)this.currentBolt);
	}

	private void UpdateBoltsHUD()
	{
		base.transform.GetComponent<RectTransform>().DOScale(Vector3.one * 1.2f, 0.1f).OnComplete(delegate
		{
			DOTween.To(() => this.currentBolt, delegate(int x)
			{
				this.currentBolt = x;
			}, GameManager.Bolt, 1f * Time.timeScale).OnUpdate(delegate
			{
				this.boltsText.text = GameManager.CurrencyToString((float)this.currentBolt);
			}).OnComplete(delegate
			{
				base.transform.GetComponent<RectTransform>().DOScale(Vector3.one * 1f, 0.2f);
			});
		});
	}
}
