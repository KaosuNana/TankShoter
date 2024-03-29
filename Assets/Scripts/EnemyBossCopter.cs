using DG.Tweening;
using System;

public class EnemyBossCopter : EnemyBoss
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
		}).Append(base.transform.DOMoveZ(20f, 1f, false)).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(0.4f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(0.4f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(0.4f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(0.4f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(0.4f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[1].StartShotRoutine();
		}).AppendInterval(0.8f).Append(base.transform.DOMoveY(4f, 1.5f, false)).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(0);
		}).AppendInterval(2f).AppendCallback(delegate
		{
			base.SpawnEnemyWaves(1);
		}).AppendInterval(3f).Append(base.transform.DOMoveY(0f, 1.5f, false));
	}
}
