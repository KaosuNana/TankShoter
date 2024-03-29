using System;

public class EnemyLaserGateChildren : EnemyNormal
{
	protected override void OnStartFunction()
	{
	}

	protected override void OnEnemyDestroyed()
	{
		base.transform.parent.GetComponent<EnemyLaserGate>().LaserGateChildDestroyed();
	}

	protected override void OnTriggerEnemyTriggerDisabler()
	{
		base.transform.parent.GetComponent<EnemyLaserGate>().gameObject.SetActive(false);
		base.gameObject.SetActive(false);
	}
}
