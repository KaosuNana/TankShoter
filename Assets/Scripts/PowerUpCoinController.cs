using DG.Tweening;
using System;
using UnityEngine;

public class PowerUpCoinController : MonoBehaviour
{
	private CoinController[] coinList;

	private void Awake()
	{
		this.coinList = base.GetComponentsInChildren<CoinController>(true);
	}

	public void RevealCoins()
	{
		for (int i = 0; i < this.coinList.Length; i++)
		{
			this.coinList[i].transform.SetParent(null, true);
			this.coinList[i].transform.localScale = Vector3.zero;
			this.coinList[i].gameObject.SetActive(true);
			this.coinList[i].ShowUpWithDelay((float)i * 0.03f);
		}
		base.transform.DOScaleY(0.1f, 0.2f);
	}
}
