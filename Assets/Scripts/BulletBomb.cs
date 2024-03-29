using DG.Tweening;
using System;
using UnityEngine;

public class BulletBomb : Bullet
{
	private void Start()
	{
		base.transform.DOJump(new Vector3(base.transform.position.x, 0f, base.transform.position.z + 15f), 5f, 1, 0.85f, false).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
	}
}
