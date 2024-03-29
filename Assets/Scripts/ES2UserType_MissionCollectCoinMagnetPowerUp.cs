using System;

public class ES2UserType_MissionCollectCoinMagnetPowerUp : ES2Type
{
	public ES2UserType_MissionCollectCoinMagnetPowerUp() : base(typeof(MissionCollectCoinMagnetPowerUp))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionCollectCoinMagnetPowerUp missionCollectCoinMagnetPowerUp = (MissionCollectCoinMagnetPowerUp)obj;
		writer.Write<bool>(missionCollectCoinMagnetPowerUp.isCompleted);
		writer.Write<float>(missionCollectCoinMagnetPowerUp.fileVersion);
		writer.Write<int>(missionCollectCoinMagnetPowerUp.progressAmount);
		writer.Write<int>(missionCollectCoinMagnetPowerUp.successAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionCollectCoinMagnetPowerUp orCreate = ES2Type.GetOrCreate<MissionCollectCoinMagnetPowerUp>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionCollectCoinMagnetPowerUp missionCollectCoinMagnetPowerUp = (MissionCollectCoinMagnetPowerUp)c;
		missionCollectCoinMagnetPowerUp.isCompleted = reader.Read<bool>();
		missionCollectCoinMagnetPowerUp.fileVersion = reader.Read<float>();
		missionCollectCoinMagnetPowerUp.progressAmount = reader.Read<int>();
		missionCollectCoinMagnetPowerUp.successAmount = reader.Read<int>();
	}
}
