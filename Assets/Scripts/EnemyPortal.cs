using DG.Tweening;
using System;
using UnityEngine;

public class EnemyPortal : EnemyNormal
{
	public int hitCountBeforeReact = 5;

	private int currentHitCount = 1;

	public int direction = 1;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 20 || other.gameObject.layer == 24)
		{
			base.ActionWhenHitByBullet(other);
			this.currentHitCount++;
			if (this.currentHitCount % this.hitCountBeforeReact == 0)
			{
				this.enemySpeed = 0f;
				base.transform.DOScale(Vector3.zero, 0.2f).OnComplete(delegate
				{
					base.transform.position = new Vector3((float)UnityEngine.Random.Range(-6, 6), 0f, (float)UnityEngine.Random.Range(10, 20));
					base.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
				});
			}
		}
	}
}
