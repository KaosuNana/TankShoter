using System;
using UnityEngine;

public class BulletEnemyLaserGrid : BulletEnemy
{
	public BulletEnemyLaserGridChildren[] bulletChildren;

	private void Start()
	{
		this.bulletChildren = base.GetComponentsInChildren<BulletEnemyLaserGridChildren>();
		this.SetDamageAmount(this.damage);
	}

	public void StartAppearing()
	{
		int num = UnityEngine.Random.Range(0, 3);
		float x = 0f;
		if (num < 1)
		{
			x = -0.5f;
		}
		else if (num < 2)
		{
			x = 0.5f;
		}
		base.transform.position = new Vector3(x, base.transform.position.y, base.transform.position.z);
		for (int i = 0; i < this.bulletChildren.Length; i++)
		{
			this.bulletChildren[i].StartAppearing();
		}
	}

	public override void SetDamageAmount(int amount)
	{
		for (int i = 0; i < this.bulletChildren.Length; i++)
		{
			this.bulletChildren[i].SetDamageAmount(amount);
			this.bulletChildren[i].shootRate = this.shootRate;
		}
	}
}
