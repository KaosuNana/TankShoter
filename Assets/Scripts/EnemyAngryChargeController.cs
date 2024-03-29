using DG.Tweening;
using System;
using UnityEngine;

public class EnemyAngryChargeController : MonoBehaviour
{
	public GameObject[] chargeImages;

	private Tween chargeTween0;

	public void AnimatingChargeImage()
	{
		this.chargeImages[0].SetActive(false);
		this.chargeImages[1].SetActive(false);
		if (this.chargeTween0 != null)
		{
			this.chargeTween0.Kill(false);
		}
		this.chargeTween0 = DOTween.Sequence().SetLoops(5, LoopType.Restart).OnStart(delegate
		{
			this.chargeImages[0].SetActive(true);
			this.chargeImages[1].SetActive(true);
			this.chargeImages[0].transform.localPosition = new Vector3(0f, 0f, 3f);
		}).OnComplete(delegate
		{
			this.TurnOff();
		}).Append(this.chargeImages[0].transform.DOLocalMoveZ(21f, 0.35f, false).SetEase(Ease.Linear));
	}

	public void TurnOff()
	{
		this.chargeTween0.Kill(false);
		for (int i = 0; i < this.chargeImages.Length; i++)
		{
			this.chargeImages[i].SetActive(false);
		}
	}
}
