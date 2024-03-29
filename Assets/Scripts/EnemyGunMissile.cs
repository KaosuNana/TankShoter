using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyGunMissile : MonoBehaviour
{
	private sealed class _StartShootWithDelay_c__AnonStorey0
	{
		internal Vector3 randomTarget;

		internal EnemyMissileTargetController tempTarget;

		internal BulletEnemy temp;

		internal EnemyGunMissile _this;

		internal void __m__0()
		{
			UnityEngine.Object.Instantiate<GameObject>(this._this.missileExplosionPrefab, this.randomTarget, Quaternion.identity);
			SoundManager.PlaySFXInArray(this._this.explodeSFX, this._this.transform.position, 0.5f);
			this.tempTarget.Disappear();
			UnityEngine.Object.Destroy(this.temp.gameObject);
		}
	}

	public BulletEnemy bulletPrefab;

	public EnemyMissileTargetController missileTargetPrefab;

	public int bulletDamage;

	public GameObject missileExplosionPrefab;

	public float offsetXForFirstMissilePath;

	public AudioClip explodeSFX;

	public void StartShootWithDelay(float delay)
	{
		EnemyMissileTargetController tempTarget = UnityEngine.Object.Instantiate<EnemyMissileTargetController>(this.missileTargetPrefab);
		tempTarget.transform.SetParent(GameManager.WorldCanvas.transform, false);
		Vector3 randomTarget = this.GetRandomTarget();
		if (delay == 0f)
		{
			randomTarget = GameManager.Player.transform.position;
		}
		tempTarget.transform.position = randomTarget;
		tempTarget.AppearWithDelay(delay);
		Vector3[] path = new Vector3[]
		{
			new Vector3(base.transform.position.x + this.offsetXForFirstMissilePath, base.transform.position.y + 10f, base.transform.position.z),
			new Vector3(randomTarget.x, base.transform.position.y + 10f, randomTarget.z),
			randomTarget
		};
		BulletEnemy temp = UnityEngine.Object.Instantiate<BulletEnemy>(this.bulletPrefab);
		temp.transform.position = base.transform.position;
		temp.SetDamageAmount(this.bulletDamage);
		temp.transform.DOPath(path, 3f, PathType.Linear, PathMode.Full3D, 10, null).SetDelay(delay).SetEase(Ease.InQuad).SetLookAt(0.1f, null, null).OnComplete(delegate
		{
			UnityEngine.Object.Instantiate<GameObject>(this.missileExplosionPrefab, randomTarget, Quaternion.identity);
			SoundManager.PlaySFXInArray(this.explodeSFX, this.transform.position, 0.5f);
			tempTarget.Disappear();
			UnityEngine.Object.Destroy(temp.gameObject);
		});
	}

	private Vector3 GetRandomTarget()
	{
		return new Vector3((float)UnityEngine.Random.Range(-5, 5), 0f, (float)UnityEngine.Random.Range(1, 15));
	}
}
