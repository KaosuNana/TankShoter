using DG.Tweening;
using System;
using UnityEngine;

public class TrophyTouchDetector : MonoBehaviour
{
	private Tween rotatingTween;

	private void OnMouseDown()
	{
		if (this.rotatingTween != null && this.rotatingTween.IsPlaying())
		{
			return;
		}
		this.rotatingTween = base.transform.DOBlendableLocalRotateBy(Vector3.up * 360f, 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
	}
}
