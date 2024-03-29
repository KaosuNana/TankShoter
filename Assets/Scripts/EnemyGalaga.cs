using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class EnemyGalaga : EnemyNormal
{
	public Vector3[] pathArray;

	public override void SetInitialPosition(Vector3 localPos, bool shouldFlyDown)
	{
	}

	public void StartAnimatingWithDelay(float delay)
	{
		this.enemySpeed = 0f;
		base.transform.DOPath(this.pathArray, 10f, PathType.Linear, PathMode.Full3D, 10, null).SetEase(Ease.Linear).SetLookAt(0f, null, null).SetDelay(delay).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}

	protected override void OnEnemyDestroyed()
	{
		base.transform.DOKill(false);
		base.gameObject.SetActive(false);
	}
}
