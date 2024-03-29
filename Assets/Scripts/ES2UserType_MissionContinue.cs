using System;

public class ES2UserType_MissionContinue : ES2Type
{
	public ES2UserType_MissionContinue() : base(typeof(MissionContinue))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionContinue missionContinue = (MissionContinue)obj;
		writer.Write<bool>(missionContinue.isCompleted);
		writer.Write<float>(missionContinue.fileVersion);
		writer.Write<int>(missionContinue.successAmount);
		writer.Write<int>(missionContinue.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionContinue orCreate = ES2Type.GetOrCreate<MissionContinue>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionContinue missionContinue = (MissionContinue)c;
		missionContinue.isCompleted = reader.Read<bool>();
		missionContinue.fileVersion = reader.Read<float>();
		missionContinue.successAmount = reader.Read<int>();
		missionContinue.progressAmount = reader.Read<int>();
	}
}
