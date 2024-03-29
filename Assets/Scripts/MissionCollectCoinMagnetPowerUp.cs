using System;
using UnityEngine;
using UnityEngine.Events;

public class MissionCollectCoinMagnetPowerUp : Mission
{
	public override void LoadMissionProgress()
	{
		if (ES2.Exists(this.savedFileName))
		{
			ES2.Load<MissionCollectCoinMagnetPowerUp>(this.savedFileName, this);
			this.UpdateDescription();
		}
		else
		{
			base.fileVersion = 1f;
			this.SaveMissionProgress();
		}
	}

	private void SaveMissionProgress()
	{
		ES2.Save<MissionCollectCoinMagnetPowerUp>(this, this.savedFileName);
	}

	private void Start()
	{
		this.UpdateDescription();
	}

	public override void UpdateDescription()
	{
		base.missionDescriptionText.text = string.Format(this.localisedMissionDescription, this.successAmount, Mathf.Max(this.successAmount - this.progressAmount, 0));
	}

	public override void StartListening()
	{
		EventManager.StartListening("CollectCoinMagnetPowerUp", new UnityAction(this.AddAmount));
	}

	public override void StopListening()
	{
		EventManager.StopListening("CollectCoinMagnetPowerUp", new UnityAction(this.AddAmount));
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
