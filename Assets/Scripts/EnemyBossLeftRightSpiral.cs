using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBossLeftRightSpiral : EnemyBoss
{
	protected override void ExtraFunctionOnEnable()
	{
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		this.bossSequence = DOTween.Sequence().OnComplete(delegate
		{
			this.ExtraFunctionOnEnable();
		}).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(0f, base.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.Fast)).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(2f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(0);
		}).Append(base.transform.DOMoveX(-5f, 1f, false).SetEase(Ease.Linear)).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(0);
		}).Append(base.transform.DOMoveX(5f, 2f, false).SetEase(Ease.Linear)).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(1);
		}).Append(base.transform.DOMoveX(0f, 1f, false).SetEase(Ease.Linear));
	}
}
