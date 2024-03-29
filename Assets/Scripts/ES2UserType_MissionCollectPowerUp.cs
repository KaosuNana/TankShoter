using System;

public class ES2UserType_MissionCollectPowerUp : ES2Type
{
	public ES2UserType_MissionCollectPowerUp() : base(typeof(MissionCollectPowerUp))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionCollectPowerUp missionCollectPowerUp = (MissionCollectPowerUp)obj;
		writer.Write<bool>(missionCollectPowerUp.isCompleted);
		writer.Write<float>(missionCollectPowerUp.fileVersion);
		writer.Write<PowerUp.PowerUpType>(missionCollectPowerUp.powerUpType);
		writer.Write<int>(missionCollectPowerUp.successAmount);
		writer.Write<int>(missionCollectPowerUp.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionCollectPowerUp orCreate = ES2Type.GetOrCreate<MissionCollectPowerUp>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionCollectPowerUp missionCollectPowerUp = (MissionCollectPowerUp)c;
		missionCollectPowerUp.isCompleted = reader.Read<bool>();
		missionCollectPowerUp.fileVersion = reader.Read<float>();
		missionCollectPowerUp.powerUpType = reader.Read<PowerUp.PowerUpType>();
		missionCollectPowerUp.successAmount = reader.Read<int>();
		missionCollectPowerUp.progressAmount = reader.Read<int>();
	}
}
