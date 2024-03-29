using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using UnityEngine;

public class EnemyBossSway : EnemyBoss
{
	private Vector3[] waypoints;

	private void Awake()
	{
		this.waypoints = new Vector3[]
		{
			new Vector3(7f, 0f, 20f),
			new Vector3(7f, 0f, 0f),
			new Vector3(-7f, 0f, 0f),
			new Vector3(-7f, 0f, 20f),
			new Vector3(5f, 0f, 20f),
			new Vector3(5f, 0f, 5f),
			new Vector3(-5f, 0f, 5f),
			new Vector3(-5f, 0f, 15f),
			new Vector3(0f, 0f, 15f),
			new Vector3(0f, 0f, -10f)
		};
	}

	protected override void ExtraFunctionOnEnable()
	{
		if (this.bossSequence != null)
		{
			this.bossSequence.Kill(false);
		}
		this.bossSequence = DOTween.Sequence().SetLoops(-1, LoopType.Restart).Append(base.transform.DOMoveZ(20f, 1f, false)).Insert(0f, base.transform.DORotate(new Vector3(0f, 180f, 0f), 0.1f, RotateMode.Fast)).AppendInterval(1f).AppendCallback(delegate
		{
			this.uniBulletShotControllerList[0].StartShotRoutine();
		}).AppendInterval(3f).Append(base.transform.DOPath(this.waypoints, 8f, PathType.Linear, PathMode.Full3D, 10, null).SetEase(Ease.Linear).SetLookAt(0f, null, null));
	}
}
