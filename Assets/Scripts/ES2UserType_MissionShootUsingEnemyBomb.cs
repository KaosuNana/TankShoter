using System;

public class ES2UserType_MissionShootUsingEnemyBomb : ES2Type
{
	public ES2UserType_MissionShootUsingEnemyBomb() : base(typeof(MissionShootUsingEnemyBomb))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		MissionShootUsingEnemyBomb missionShootUsingEnemyBomb = (MissionShootUsingEnemyBomb)obj;
		writer.Write<bool>(missionShootUsingEnemyBomb.isCompleted);
		writer.Write<float>(missionShootUsingEnemyBomb.fileVersion);
		writer.Write<int>(missionShootUsingEnemyBomb.successAmount);
		writer.Write<int>(missionShootUsingEnemyBomb.progressAmount);
	}

	public override object Read(ES2Reader reader)
	{
		MissionShootUsingEnemyBomb orCreate = ES2Type.GetOrCreate<MissionShootUsingEnemyBomb>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		MissionShootUsingEnemyBomb missionShootUsingEnemyBomb = (MissionShootUsingEnemyBomb)c;
		missionShootUsingEnemyBomb.isCompleted = reader.Read<bool>();
		missionShootUsingEnemyBomb.fileVersion = reader.Read<float>();
		missionShootUsingEnemyBomb.successAmount = reader.Read<int>();
		missionShootUsingEnemyBomb.progressAmount = reader.Read<int>();
	}
}
