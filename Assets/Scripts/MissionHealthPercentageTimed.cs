using System;
using UnityEngine.Events;

public class MissionHealthPercentageTimed : Mission
{
	public bool timedMission;

	public float timeLimit;

	private void Start()
	{
		this.UpdateDescription();
		base.missionManagerInstance.SetMissionTimer(this.timeLimit);
	}

	public override void LoadMissionProgress()
	{
	}

	public override void UpdateDescription()
	{
		base.missionDescriptionText.text = string.Format(this.localisedMissionDescription, this.timeLimit, this.successAmount);
	}

	public override void StartListening()
	{
		EventManager.StartListening("HpTimed", new UnityAction(this.CheckHP));
	}

	private void SaveMissionProgress()
	{
	}

	public override void StopListening()
	{
		EventManager.StopListening("HpTimed", new UnityAction(this.CheckHP));
	}

	private void CheckHP()
	{
		if (GameManager.Player.GetHPPercentage() >= (float)this.successAmount / 100f)
		{
			this.progressAmount = this.successAmount;
			base.MissionComplete(true);
			this.StopListening();
		}
	}
}
