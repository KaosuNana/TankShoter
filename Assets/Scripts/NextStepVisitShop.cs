using System;
using UnityEngine;

public class NextStepVisitShop : NextStep
{
	public GameObject shopButton;

	public override bool ShouldShow()
	{
		return true;
	}

	public void OnPressShopButton()
	{
		SoundManager.PlayTapSFX();
		GameManager.ShowShopPageWithNotEnoughCoinOrBolt(false, false);
	}

	protected override void OnChangeLanguage()
	{
	}
}
