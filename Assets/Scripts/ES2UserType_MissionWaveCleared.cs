using System;

public class ES2UserType_MissionWaveCleared : ES2Type
{
	public ES2UserType_MissionWaveCleared() : base(typeof(MissionWaveCleared))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionWaveCleared missionWaveCleared = (MissionWaveCleared)obj;
		writer.Write<bool>(missionWaveCleared.isCompleted);
		writer.Write<float>(missionWaveCleared.fileVersion);
		writer.Write<int>(missionWaveCleared.successAmount);
		writer.Write<int>(missionWaveCleared.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionWaveCleared orCreate = ES2Type.GetOrCreate<MissionWaveCleared>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionWaveCleared missionWaveCleared = (MissionWaveCleared)c;
		missionWaveCleared.isCompleted = reader.Read<bool>();
		missionWaveCleared.fileVersion = reader.Read<float>();
		missionWaveCleared.successAmount = reader.Read<int>();
		missionWaveCleared.progressAmount = reader.Read<int>();
	}
}
