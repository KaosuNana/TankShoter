using System;

public class ES2UserType_MissionShootBossTotal : ES2Type
{
	public ES2UserType_MissionShootBossTotal() : base(typeof(MissionShootBossTotal))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionShootBossTotal missionShootBossTotal = (MissionShootBossTotal)obj;
		writer.Write<bool>(missionShootBossTotal.isCompleted);
		writer.Write<float>(missionShootBossTotal.fileVersion);
		writer.Write<int>(missionShootBossTotal.successAmount);
		writer.Write<int>(missionShootBossTotal.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionShootBossTotal orCreate = ES2Type.GetOrCreate<MissionShootBossTotal>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionShootBossTotal missionShootBossTotal = (MissionShootBossTotal)c;
		missionShootBossTotal.isCompleted = reader.Read<bool>();
		missionShootBossTotal.fileVersion = reader.Read<float>();
		missionShootBossTotal.successAmount = reader.Read<int>();
		missionShootBossTotal.progressAmount = reader.Read<int>();
	}
}
