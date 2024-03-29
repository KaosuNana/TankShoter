using DG.Tweening;
using System;

public class EnemyJumpy : EnemyNormal
{
	private void Start()
	{
		base.InvokeRepeating("Jump", 0f, 0.6f);
	}

	private void Jump()
	{
		base.transform.DOMoveY(2f, 0.3f, false).OnComplete(delegate
		{
			base.transform.DOMoveY(0f, 0.3f, false).SetEase(Ease.InQuad);
		});
	}
}
