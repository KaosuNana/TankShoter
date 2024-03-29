using System;

public class ES2UserType_VehicleInfo : ES2Type
{
	public ES2UserType_VehicleInfo() : base(typeof(VehicleInfo))
	{
	}

	public override void Write(object obj, ES2Writer writer)
	{
		VehicleInfo vehicleInfo = (VehicleInfo)obj;
		writer.Write<float>(vehicleInfo.fileVersion);
		writer.Write<int>(vehicleInfo.vehicleLevel);
		writer.Write<int>(vehicleInfo.remainingLife);
		writer.Write<int>(vehicleInfo.attackLevel);
		writer.Write<int>(vehicleInfo.chargeLevel);
		writer.Write<int>(vehicleInfo.hpLevel);
		writer.Write<int>(vehicleInfo.teamPosition);
		writer.Write<bool>(vehicleInfo.isUnlocked);
	}

	public override object Read(ES2Reader reader)
	{
		VehicleInfo orCreate = ES2Type.GetOrCreate<VehicleInfo>();
		this.Read(reader, orCreate);
		return orCreate;
	}

	public override void Read(ES2Reader reader, object c)
	{
		VehicleInfo vehicleInfo = (VehicleInfo)c;
		vehicleInfo.fileVersion = reader.Read<float>();
		vehicleInfo.vehicleLevel = reader.Read<int>();
		vehicleInfo.remainingLife = reader.Read<int>();
		vehicleInfo.attackLevel = reader.Read<int>();
		vehicleInfo.chargeLevel = reader.Read<int>();
		vehicleInfo.hpLevel = reader.Read<int>();
		vehicleInfo.teamPosition = reader.Read<int>();
		vehicleInfo.isUnlocked = reader.Read<bool>();
	}
}
