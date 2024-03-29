using System;
using UnityEngine;
using UnityEngine.Events;

public class MissionShootBossTotal : Mission
{
	private void Start()
	{
		this.UpdateDescription();
	}

	public override void LoadMissionProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Load<MissionShootBossTotal>(this.savedFileName, this);
		}
		else
		{
			base.fileVersion = 1f;
			this.SaveMissionProgress();
		}
	}

	public override void UpdateDescription()
	{
		base.missionDescriptionText.text = string.Format(this.localisedMissionDescription, this.successAmount, Mathf.Clamp(this.successAmount - this.progressAmount, 0, this.successAmount));
	}

	public override void StartListening()
	{
		EventManager.StartListening("ShootBoss", new UnityAction(this.AddAmount));
	}

	private void SaveMissionProgress()
	{
		ES2.Save<MissionShootBossTotal>(this, this.savedFileName);
	}

	public override void StopListening()
	{
		EventManager.StopListening("ShootBoss", new UnityAction(this.AddAmount));
	}

	private void AddAmount()
	{
		this.progressAmount++;
		base.missionManagerInstance.UpdateMissionProgress(this.progressAmount.ToString() + "/" + this.successAmount.ToString());
		this.SaveMissionProgress();
		this.UpdateDescription();
		if (this.progressAmount >= this.successAmount)
		{
			base.MissionComplete(false);
			this.StopListening();
		}
	}
}
