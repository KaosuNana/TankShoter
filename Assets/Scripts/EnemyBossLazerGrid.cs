using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBossLazerGrid : EnemyBoss
{
	public BulletEnemyLaserGrid laserGrid;

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
		}).AppendInterval(1.5f).AppendCallback(delegate
		{
			this.laserGrid.StartAppearing();
		}).AppendInterval(3f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(0);
		}).AppendInterval(2f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(1);
		}).AppendInterval(2f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(1.5f).AppendCallback(delegate
		{
			this.laserGrid.StartAppearing();
		}).AppendInterval(3f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(0);
		}).AppendInterval(2f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(1);
		}).AppendInterval(3.5f);
	}
}
