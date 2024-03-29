using System;

public class NextStepChangeHero : NextStep
{
	public override bool ShouldShow()
	{
		return true;
	}

	public void OnPressChangeHeroButton()
	{
		SoundManager.PlayTapSFX();
	}

	protected override void OnChangeLanguage()
	{
	}
}
