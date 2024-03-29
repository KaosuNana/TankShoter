using System;
using UnityEngine.Events;

public class MissionNoCollectPowerUpTimed : Mission
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
		base.missionDescriptionText.text = string.Format(this.localisedMissionDescription, this.timeLimit);
	}

	public override void StartListening()
	{
		EventManager.StartListening("HpTimed", new UnityAction(this.CheckHP));
		EventManager.StartListeningEventPowerUp("CollectPowerUp", new UnityAction<PowerUp.PowerUpType>(this.AddAmount));
	}

	private void SaveMissionProgress()
	{
	}

	public override void StopListening()
	{
		EventManager.StopListening("HpTimed", new UnityAction(this.CheckHP));
		EventManager.StopListeningEventPowerUp("CollectPowerUp", new UnityAction<PowerUp.PowerUpType>(this.AddAmount));
	}

	private void AddAmount(PowerUp.PowerUpType powerUpType)
	{
		this.progressAmount--;
	}

	private void CheckHP()
	{
		if (this.progressAmount == 0)
		{
			this.progressAmount = this.successAmount;
			base.MissionComplete(true);
			this.StopListening();
		}
	}
}
