using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunWide : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullets01;

		internal Bullet bullets02;

		internal Bullet bullets03;

		internal Bullet bullets04;

		internal Bullet bullets05;

		internal void __m__0()
		{
			this.bullets01.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullets02.gameObject.SetActive(false);
		}

		internal void __m__2()
		{
			this.bullets03.gameObject.SetActive(false);
		}

		internal void __m__3()
		{
			this.bullets04.gameObject.SetActive(false);
		}

		internal void __m__4()
		{
			this.bullets05.gameObject.SetActive(false);
		}
	}

	private sealed class _Shoot_c__AnonStorey1
	{
		internal Bullet bullets06;

		internal Bullet bullets07;

		internal void __m__0()
		{
			this.bullets06.gameObject.SetActive(false);
		}

		internal void __m__1()
		{
			this.bullets07.gameObject.SetActive(false);
		}
	}

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		if (this.autoTarget && (this.target == null || !this.target.activeInHierarchy))
		{
			this.target = EnemyManager.GetEnemy();
		}
		Bullet bullets01 = base.GetPooledBullets();
		bullets01.gameObject.SetActive(true);
		bullets01.transform.DOKill(false);
		bullets01.transform.position = new Vector3(base.transform.position.x - 0.1f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets01.SetDamageAmount(this.currentBulletDamage);
		bullets01.transform.rotation = Quaternion.Euler(0f, 355f, 0f);
		Vector3 positionWithAngle = this.GetPositionWithAngle(bullets01.transform.position, 30f, 355);
		positionWithAngle.y = bullets01.transform.position.y;
		bullets01.transform.DOMove(positionWithAngle, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets01.gameObject.SetActive(false);
		});
		Bullet bullets02 = base.GetPooledBullets();
		bullets02.transform.DOKill(false);
		bullets02.gameObject.SetActive(true);
		bullets02.transform.position = new Vector3(base.transform.position.x + 0.1f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets02.SetDamageAmount(this.currentBulletDamage);
		bullets02.transform.rotation = Quaternion.Euler(0f, 5f, 0f);
		Vector3 positionWithAngle2 = this.GetPositionWithAngle(bullets02.transform.position, 30f, 5);
		positionWithAngle2.y = bullets02.transform.position.y;
		bullets02.transform.DOMove(positionWithAngle2, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets02.gameObject.SetActive(false);
		});
		Bullet bullets03 = base.GetPooledBullets();
		bullets03.transform.DOKill(false);
		bullets03.gameObject.SetActive(true);
		bullets03.transform.position = new Vector3(base.transform.position.x - 0.2f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets03.SetDamageAmount(this.currentBulletDamage);
		bullets03.transform.rotation = Quaternion.Euler(0f, 350f, 0f);
		Vector3 positionWithAngle3 = this.GetPositionWithAngle(bullets03.transform.position, 30f, 350);
		positionWithAngle3.y = bullets03.transform.position.y;
		bullets03.transform.DOMove(positionWithAngle3, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets03.gameObject.SetActive(false);
		});
		Bullet bullets04 = base.GetPooledBullets();
		bullets04.transform.DOKill(false);
		bullets04.gameObject.SetActive(true);
		bullets04.transform.position = new Vector3(base.transform.position.x + 0.2f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		Vector3 positionWithAngle4 = this.GetPositionWithAngle(bullets04.transform.position, 30f, 10);
		positionWithAngle4.y = bullets04.transform.position.y;
		bullets04.transform.rotation = Quaternion.Euler(0f, 10f, 0f);
		bullets04.SetDamageAmount(this.currentBulletDamage);
		bullets04.transform.DOMove(positionWithAngle4, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets04.gameObject.SetActive(false);
		});
		Bullet bullets05 = base.GetPooledBullets();
		bullets05.transform.DOKill(false);
		bullets05.gameObject.SetActive(true);
		bullets05.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z + 1f);
		Vector3 positionWithAngle5 = this.GetPositionWithAngle(bullets05.transform.position, 30f, 0);
		positionWithAngle5.y = bullets05.transform.position.y;
		bullets05.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		bullets05.SetDamageAmount(this.currentBulletDamage);
		bullets05.transform.DOMove(positionWithAngle5, this.bulletTravelDuration, false).OnComplete(delegate
		{
			bullets05.gameObject.SetActive(false);
		});
		if (!this.autoTarget)
		{
			Bullet bullets06 = base.GetPooledBullets();
			bullets06.gameObject.SetActive(true);
			bullets06.transform.DOKill(false);
			bullets06.transform.position = new Vector3(base.transform.position.x - 0.3f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets06.SetDamageAmount(this.currentBulletDamage);
			bullets06.transform.rotation = Quaternion.Euler(0f, 345f, 0f);
			Vector3 positionWithAngle6 = this.GetPositionWithAngle(bullets06.transform.position, 30f, 345);
			positionWithAngle6.y = bullets06.transform.position.y;
			bullets06.transform.DOMove(positionWithAngle6, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets06.gameObject.SetActive(false);
			});
			Bullet bullets07 = base.GetPooledBullets();
			bullets07.gameObject.SetActive(true);
			bullets07.transform.DOKill(false);
			bullets07.transform.position = new Vector3(base.transform.position.x + 0.3f, base.transform.position.y + 1f, base.transform.position.z + 1f);
			bullets07.SetDamageAmount(this.currentBulletDamage);
			bullets07.transform.rotation = Quaternion.Euler(0f, 15f, 0f);
			Vector3 positionWithAngle7 = this.GetPositionWithAngle(bullets07.transform.position, 30f, 15);
			positionWithAngle7.y = bullets07.transform.position.y;
			bullets07.transform.DOMove(positionWithAngle7, this.bulletTravelDuration, false).OnComplete(delegate
			{
				bullets07.gameObject.SetActive(false);
			});
		}
	}

	public override string GetBulletDamageWithAttackLevel(int attackLevel)
	{
		this.SetAttackLevel(attackLevel);
		return GameManager.CurrencyToString((float)this.currentBulletDamage * 7f / 100f);
	}

	private Vector3 GetPositionWithAngle(Vector3 center, float radius, int ang)
	{
		Vector3 result;
		result.x = center.x + radius * Mathf.Sin((float)ang * 0.0174532924f);
		result.y = center.y;
		result.z = center.z + radius * Mathf.Cos((float)ang * 0.0174532924f);
		return result;
	}
}
