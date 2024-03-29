using System;

public class ES2UserType_MissionCollectBoltsTotal : ES2Type
{
	public ES2UserType_MissionCollectBoltsTotal() : base(typeof(MissionCollectBoltsTotal))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionCollectBoltsTotal missionCollectBoltsTotal = (MissionCollectBoltsTotal)obj;
		writer.Write<bool>(missionCollectBoltsTotal.isCompleted);
		writer.Write<float>(missionCollectBoltsTotal.fileVersion);
		writer.Write<int>(missionCollectBoltsTotal.successAmount);
		writer.Write<int>(missionCollectBoltsTotal.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionCollectBoltsTotal orCreate = ES2Type.GetOrCreate<MissionCollectBoltsTotal>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionCollectBoltsTotal missionCollectBoltsTotal = (MissionCollectBoltsTotal)c;
		missionCollectBoltsTotal.isCompleted = reader.Read<bool>();
		missionCollectBoltsTotal.fileVersion = reader.Read<float>();
		missionCollectBoltsTotal.successAmount = reader.Read<int>();
		missionCollectBoltsTotal.progressAmount = reader.Read<int>();
	}
}
