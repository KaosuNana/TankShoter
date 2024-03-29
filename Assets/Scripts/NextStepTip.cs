using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.UI;

public class NextStepTip : NextStep
{
	public string[] tipList;

	public Text tipText;

	public override bool ShouldShow()
	{
		this.tipText.text = ScriptLocalization.Get(this.tipList[UnityEngine.Random.Range(0, this.tipList.Length)]);
		return true;
	}

	protected override void OnChangeLanguage()
	{
		this.tipText.text = ScriptLocalization.Get(this.tipList[UnityEngine.Random.Range(0, this.tipList.Length)]);
	}
}
