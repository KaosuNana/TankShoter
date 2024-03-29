using System;

public class ES2UserType_MissionShootMiniBossTotal : ES2Type
{
	public ES2UserType_MissionShootMiniBossTotal() : base(typeof(MissionShootMiniBossTotal))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionShootMiniBossTotal missionShootMiniBossTotal = (MissionShootMiniBossTotal)obj;
		writer.Write<bool>(missionShootMiniBossTotal.isCompleted);
		writer.Write<float>(missionShootMiniBossTotal.fileVersion);
		writer.Write<int>(missionShootMiniBossTotal.successAmount);
		writer.Write<int>(missionShootMiniBossTotal.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionShootMiniBossTotal orCreate = ES2Type.GetOrCreate<MissionShootMiniBossTotal>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionShootMiniBossTotal missionShootMiniBossTotal = (MissionShootMiniBossTotal)c;
		missionShootMiniBossTotal.isCompleted = reader.Read<bool>();
		missionShootMiniBossTotal.fileVersion = reader.Read<float>();
		missionShootMiniBossTotal.successAmount = reader.Read<int>();
		missionShootMiniBossTotal.progressAmount = reader.Read<int>();
	}
}
