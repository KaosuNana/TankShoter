using System;
using UnityEngine;

[RequireComponent(typeof(GUIText))]
public class UbhAutoScaleGUIText : MonoBehaviour
{
	private GUIText m_guiText;

	private float m_orgFontSize;

	private void Awake()
	{
		this.m_guiText = base.GetComponent<GUIText>();
		this.m_orgFontSize = (float)this.m_guiText.fontSize;
	}

	private void Update()
	{
		float num = (float)Screen.width / 600f;
		float num2 = (float)Screen.height / 450f;
		float num3 = (Screen.height >= Screen.width) ? num : num2;
		this.m_guiText.fontSize = (int)(this.m_orgFontSize * num3);
	}
}
