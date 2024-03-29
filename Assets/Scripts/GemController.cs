using System;

public class GemController : CoinController
{
	protected override void DeliverValueToGM()
	{
		EventManager.TriggerEvent("CollectBolts");
		GameManager.ExtraBolt(this.value);
	}
}
