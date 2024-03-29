using DG.Tweening;
using System;
using UnityEngine;

public class EnemyNormal : Enemy
{
	protected override void OnAwakeFunction()
	{
		this.myRenderer = base.GetComponentInChildren<Renderer>();
		base.GetComponentInChildren<Animator>().Play("Idle_Baddy");
		this.SetupHealthBar();
	}

	protected override void OnStartFunction()
	{
	}

	public override void SetInitialPosition(Vector3 localPos, bool shouldFlyDown)
	{
		if (!shouldFlyDown)
		{
			base.transform.localPosition = localPos;
		}
		else
		{
			localPos.y = 20f;
			base.transform.localPosition = localPos;
			base.transform.DOMoveY(0f, 0.75f, false).SetEase(Ease.OutQuart);
		}
	}

	protected override void ExtraFunctionOnEnable()
	{
	}

	protected override void SpecialEffectOnFirstHit()
	{
	}

	protected override void OnEnemyDestroyed()
	{
	}
}
