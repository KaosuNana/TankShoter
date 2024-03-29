using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunPlus : GunController
{
	private sealed class _Shoot_c__AnonStorey0
	{
		internal Bullet bullets01;

		internal Bullet bullets02;

		internal Bullet bullets03;

		internal Bullet bullets04;

		internal void __m__0()
		{
			UnityEngine.Object.Destroy(this.bullets01.gameObject);
		}

		internal void __m__1()
		{
			UnityEngine.Object.Destroy(this.bullets02.gameObject);
		}

		internal void __m__2()
		{
			UnityEngine.Object.Destroy(this.bullets03.gameObject);
		}

		internal void __m__3()
		{
			UnityEngine.Object.Destroy(this.bullets04.gameObject);
		}
	}

	protected override void Shoot()
	{
		SoundManager.PlaySFXInArray(this.mainGunShootSFX, base.transform.position, 0.5f);
		Bullet bullets01 = UnityEngine.Object.Instantiate<Bullet>(this.currentBulletPrefab);
		bullets01.name = "Bullets";
		bullets01.transform.position = new Vector3(base.transform.position.x - 0.5f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets01.transform.DOBlendableMoveBy(new Vector3(0f, 0f, 50f), 1.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(bullets01.gameObject);
		});
		Bullet bullets02 = UnityEngine.Object.Instantiate<Bullet>(this.currentBulletPrefab);
		bullets02.name = "Bullets";
		bullets02.transform.position = new Vector3(base.transform.position.x + 0.5f, base.transform.position.y + 1f, base.transform.position.z + 1f);
		bullets02.transform.DOBlendableMoveBy(new Vector3(0f, 0f, 50f), 1.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(bullets02.gameObject);
		});
		Bullet bullets03 = UnityEngine.Object.Instantiate<Bullet>(this.currentBulletPrefab);
		bullets03.name = "Bullets";
		bullets03.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z);
		bullets03.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
		bullets03.transform.DOBlendableMoveBy(new Vector3(-50f, 0f, 0f), 1.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(bullets03.gameObject);
		});
		Bullet bullets04 = UnityEngine.Object.Instantiate<Bullet>(this.currentBulletPrefab);
		bullets04.name = "Bullets";
		bullets04.transform.position = new Vector3(base.transform.position.x + 0.5f, base.transform.position.y + 1f, base.transform.position.z);
		bullets04.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
		bullets04.transform.DOBlendableMoveBy(new Vector3(50f, 0f, 0f), 1.5f, false).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(bullets04.gameObject);
		});
	}
}
