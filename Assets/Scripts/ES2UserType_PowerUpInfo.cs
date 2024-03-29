using System;

public class ES2UserType_PowerUpInfo : ES2Type
{
	public ES2UserType_PowerUpInfo() : base(typeof(PowerUpInfo))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		PowerUpInfo powerUpInfo = (PowerUpInfo)obj;
		writer.Write<int>(powerUpInfo.level);
		writer.Write<float>(powerUpInfo.fileVersion);
	}

	public override object Read(ES2Reader reader)
	{
		PowerUpInfo orCreate = ES2Type.GetOrCreate<PowerUpInfo>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		PowerUpInfo powerUpInfo = (PowerUpInfo)c;
		powerUpInfo.level = reader.Read<int>();
		powerUpInfo.fileVersion = reader.Read<float>();
	}
}
