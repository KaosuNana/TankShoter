using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private SlotRowManager _slotRowInstance_k__BackingField;

	public SlotRowManager slotRowInstance
	{
		get;
		set;
	}

	private void Awake()
	{
		this.slotRowInstance = base.transform.parent.GetComponent<SlotRowManager>();
	}

	public void ResetMinMaxToOriginal()
	{
		if (this.slotRowInstance == null)
		{
			this.slotRowInstance = base.transform.parent.GetComponent<SlotRowManager>();
		}
		if (this.slotRowInstance != null)
		{
			this.slotRowInstance.ResetMinMaxToOriginal();
		}
	}
}
