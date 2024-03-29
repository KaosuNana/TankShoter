using DG.Tweening;
using System;

public class EnemySway : EnemyNormal
{
	public float maxSwayX;

	public float minSwayX;

	private void Start()
	{
		base.InvokeRepeating("StartSwaying", 0f, 2f);
	}

	private void StartSwaying()
	{
		base.transform.DOMoveX(this.maxSwayX, 1f, false).OnComplete(delegate
		{
			base.transform.DOMoveX(this.minSwayX, 1f, false);
		});
	}
}
