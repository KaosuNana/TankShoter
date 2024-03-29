using System;

public class EnemyPoolManager : PoolManager
{
	protected override void StartPooling()
	{
		for (int i = 0; i < this.pooledAmount; i++)
		{
			base.AddNewObject(i);
			if (this.addRandomly)
			{
				base.AddNewObjectRandomly();
			}
		}
	}
}
