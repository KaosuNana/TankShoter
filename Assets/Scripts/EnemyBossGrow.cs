using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBossGrow : EnemyBoss
{
	private static TweenCallback __f__am_cache0;

	protected override void OnStartFunction()
	{
		if (this.uniBulletShotControllerList.Count > 0)
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}
	}

	protected override void ExtraFunctionOnEnable()
	{
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		this.bossSequence = DOTween.Sequence().OnComplete(delegate
		{
			this.ExtraFunctionOnEnable();
		}).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(0f, base.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.Fast)).AppendInterval(1f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(3f).Append(base.transform.DOMoveX(-5f, 1f, false).SetEase(Ease.Linear)).Append(base.transform.DOMoveX(5f, 2f, false).SetEase(Ease.Linear)).Append(base.transform.DOMoveX(-5f, 2f, false).SetEase(Ease.Linear)).Append(base.transform.DOMoveX(5f, 2f, false).SetEase(Ease.Linear)).AppendCallback(delegate
		{
		}).Append(base.transform.DOMoveX(-5f, 2f, false).SetEase(Ease.Linear)).Append(base.transform.DOMoveX(5f, 2f, false).SetEase(Ease.Linear)).Append(base.transform.DOMoveX(0f, 1f, false).SetEase(Ease.Linear));
	}
}
