using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhTitle : UbhMonoBehaviour
{
	private const string TITLE_PC = "Press X";

	private const string TITLE_MOBILE = "Tap To Start";

	[FormerlySerializedAs("_StartGUIText"), SerializeField]
	private GUIText m_startGUIText;

	private void Start()
	{
		this.m_startGUIText.text = ((!UbhUtil.IsMobilePlatform()) ? "Press X" : "Tap To Start");
	}
}
