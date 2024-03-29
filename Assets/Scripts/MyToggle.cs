using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyToggle : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	private Image[] buttonChildrenObjects;

	private Vector2[] originalChildrenPos;

	private bool buttonPressed;

	public ColorButton colorButton;

	public bool highlightDebug;

	private bool isBlinking;

	private int indexBlinking;

	private Toggle myToggle;

	private bool isToggleOn;

	private void Start()
	{
		this.buttonChildrenObjects = new Image[base.transform.childCount];
		this.originalChildrenPos = new Vector2[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.buttonChildrenObjects[i] = base.transform.GetChild(i).GetComponent<Image>();
			this.originalChildrenPos[i] = this.buttonChildrenObjects[i].GetComponent<RectTransform>().anchoredPosition;
		}
		this.myToggle = base.GetComponent<Toggle>();
		if (this.myToggle.isOn)
		{
			this.PressButton();
		}
		this.myToggle.onValueChanged.AddListener(new UnityAction<bool>(this.ToggleChanged));
	}

	private void ToggleChanged(bool isOn)
	{
		this.isToggleOn = isOn;
		if (this.isToggleOn)
		{
			this.PressButton();
		}
		else
		{
			this.ReleaseButton();
		}
	}

	public void DisableButton()
	{
		this.buttonChildrenObjects[0].color = this.colorButton.backDisableColor;
		this.buttonChildrenObjects[1].color = this.colorButton.frontDisableColor;
		this.buttonChildrenObjects[1].GetComponent<Outline>().effectColor = this.colorButton.frontDisableColor;
	}

	public void EnableButton()
	{
		this.ResetButtonToOriginalColor();
	}

	private void ResetButtonToOriginalColor()
	{
		this.indexBlinking = 0;
		this.buttonChildrenObjects[0].color = this.colorButton.backColor[0];
		this.buttonChildrenObjects[1].color = this.colorButton.frontColor[0];
		this.buttonChildrenObjects[1].GetComponent<Outline>().effectColor = this.colorButton.outlineColor[0];
	}

	public void HighlightButton()
	{
		this.indexBlinking++;
		this.buttonChildrenObjects[0].color = this.colorButton.backColor[this.indexBlinking % 2];
		this.buttonChildrenObjects[1].color = this.colorButton.frontColor[this.indexBlinking % 2];
		this.buttonChildrenObjects[1].GetComponent<Outline>().effectColor = this.colorButton.outlineColor[this.indexBlinking % 2];
	}

	private void Stophighlight()
	{
		base.CancelInvoke();
		this.ResetButtonToOriginalColor();
	}

	private void ReleaseButton()
	{
		this.buttonChildrenObjects[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(this.originalChildrenPos[1].x, this.originalChildrenPos[1].y);
	}

	private void PressButton()
	{
		this.Stophighlight();
		this.buttonChildrenObjects[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(this.originalChildrenPos[1].x, this.originalChildrenPos[1].y - 8f);
	}

	public void OnPointerDown(PointerEventData data)
	{
		this.buttonPressed = true;
		this.PressButton();
	}
}
