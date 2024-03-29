using System;
using UnityEngine;
using UnityEngine.Events;

public class MissionCollectPowerUp : Mission
{
	public PowerUp.PowerUpType powerUpType;

	private void Start()
	{
		this.UpdateDescription();
	}

	public override void LoadMissionProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Load<MissionCollectPowerUp>(this.savedFileName, this);
		}
		else
		{
			base.fileVersion = 1f;
			this.SaveMissionProgress();
		}
	}

	public override void UpdateDescription()
	{
		base.missionDescriptionText.text = string.Format(this.localisedMissionDescription, this.successAmount, this.powerUpType.ToString(), Mathf.Clamp(this.successAmount - this.progressAmount, 0, this.successAmount));
	}

	public override void StartListening()
	{
		EventManager.StartListeningEventPowerUp("CollectPowerUp", new UnityAction<PowerUp.PowerUpType>(this.AddAmount));
	}

	private void SaveMissionProgress()
	{
		ES2.Save<MissionCollectPowerUp>(this, this.savedFileName);
	}

	public override void StopListening()
	{
		EventManager.StopListeningEventPowerUp("CollectPowerUp", new UnityAction<PowerUp.PowerUpType>(this.AddAmount));
	}

	private void AddAmount(PowerUp.PowerUpType powerUpTypeCollected)
	{
		if (powerUpTypeCollected == this.powerUpType)
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
}
