using DG.Tweening;
using System;
using UnityEngine;

public class BulletInvisibleWideThrower : Bullet
{
	public override void AfterHitEnemy()
	{
		if (this.impactSFXLength > 0)
		{
			SoundManager.PlaySFXInArray(this.impactArraySFX[UnityEngine.Random.Range(0, this.impactSFXLength)], base.transform.position, 0.5f);
		}
	}

	public override void SetDamageAmount(int amount)
	{
		base.damage = amount;
		base.UpdateDamageValue();
	}

	public void ShootForward(float byZAmount, float duration)
	{
		base.transform.DOBlendableMoveBy(new Vector3(0f, 0f, 30f), duration, false).OnComplete(delegate
		{
			base.gameObject.SetActive(false);
		});
	}
}
