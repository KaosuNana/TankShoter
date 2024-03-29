using DG.Tweening;
using System;
using UnityEngine;

public class EnemyCheeky : EnemyNormal
{
	private int currentHitCount = 1;

	private float currentHitTreshold = 0.9f;

	private int direction = 1;

	public int minCoinWhenHit;

	public int maxCoinWhenHit;

	public int minCoinWhenDestroyed;

	public int maxCoinWhenDestroyed;

	private int countShootRate;

	public float cheekyAppearDuration;

	protected override void ExtraFunctionOnEnable()
	{
		this.currentHitTreshold = 0.9f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 20 || other.gameObject.layer == 27)
		{
			base.ActionWhenHitByBullet(other);
			this.currentHitCount++;
			this.UpdatePosition();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 20 || other.gameObject.layer == 27)
		{
			this.currentShootRate += Time.deltaTime;
			if (this.currentShootRate >= other.gameObject.GetComponent<Bullet>().shootRate)
			{
				this.currentShootRate = 0f;
				this.currentHitCount++;
				base.ActionWhenHitByBullet(other);
				this.UpdatePosition();
			}
		}
	}

	private void UpdatePosition()
	{
		if (this.healthBarInstance.progress <= this.currentHitTreshold)
		{
			this.currentHitTreshold -= 0.05f;
			this.direction *= -1;
			base.transform.DOMoveX((float)Mathf.Clamp(this.direction * UnityEngine.Random.Range(3, 5), -6, 6), 0.5f, false);
			Vector3 position = base.transform.position;
			position.y = 0.4f;
			int num = UnityEngine.Random.Range(this.minCoinWhenHit, this.maxCoinWhenHit + 1);
			for (int i = 0; i < num; i++)
			{
				GameManager.SpawnCoin(position);
			}
		}
	}

	public override void SetInitialPosition(Vector3 localPos, bool shouldFlyDown)
	{
		localPos.y = 20f;
		base.transform.localPosition = localPos;
		base.transform.DOMoveY(4f, 1f, false).SetEase(Ease.OutBack).OnComplete(delegate
		{
			base.transform.DOMoveY(0f, 0.75f, false).SetEase(Ease.OutQuart).SetDelay(0.6f).OnComplete(delegate
			{
				base.transform.DOMove(new Vector3(base.transform.position.x, 20f, base.transform.position.z + 20f), 3f, false).SetDelay(this.cheekyAppearDuration).OnComplete(delegate
				{
					base.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
					base.gameObject.SetActive(false);
				}).OnStart(delegate
				{
					base.transform.DORotate(new Vector3(-25f, 180f, 0f), 0.25f, RotateMode.Fast);
					this.shootable = false;
				});
			});
		});
	}

	protected override void OnEnemyDestroyed()
	{
		Vector3 position = base.transform.position;
		position.y = 0.4f;
		position.z += UnityEngine.Random.Range(-1f, 1f);
		int num = UnityEngine.Random.Range(this.minCoinWhenDestroyed, this.maxCoinWhenDestroyed + 1);
		for (int i = 0; i < num; i++)
		{
			GameManager.SpawnCoin(position);
		}
	}
}
