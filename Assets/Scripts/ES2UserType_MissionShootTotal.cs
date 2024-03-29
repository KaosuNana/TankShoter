using System;

public class ES2UserType_MissionShootTotal : ES2Type
{
	public ES2UserType_MissionShootTotal() : base(typeof(MissionShootTotal))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionShootTotal missionShootTotal = (MissionShootTotal)obj;
		writer.Write<bool>(missionShootTotal.isCompleted);
		writer.Write<float>(missionShootTotal.fileVersion);
		writer.Write<int>(missionShootTotal.successAmount);
		writer.Write<int>(missionShootTotal.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionShootTotal orCreate = ES2Type.GetOrCreate<MissionShootTotal>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionShootTotal missionShootTotal = (MissionShootTotal)c;
		missionShootTotal.isCompleted = reader.Read<bool>();
		missionShootTotal.fileVersion = reader.Read<float>();
		missionShootTotal.successAmount = reader.Read<int>();
		missionShootTotal.progressAmount = reader.Read<int>();
	}
}
