using DG.Tweening;
using System;
using UnityEngine;

public class EnemyDrop : EnemyNormal
{
	private void Start()
	{
		base.transform.position = new Vector3(base.transform.position.x, 20f, base.transform.position.z);
		base.transform.DOMoveY(0f, 0.5f, false).SetEase(Ease.InQuad).OnComplete(delegate
		{
			base.transform.DOMoveZ(-10f, 2f, false).SetDelay(0.5f).OnComplete(delegate
			{
				base.gameObject.SetActive(false);
			});
		});
	}

	private void OnDisable()
	{
		base.CancelInvoke();
		base.transform.DOKill(false);
		this.shootable = false;
	}
}
