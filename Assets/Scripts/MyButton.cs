using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IEventSystemHandler
{
	private Image[] buttonChildrenObjects;

	private Vector2[] originalChildrenPos;

	private bool buttonPressed;

	public ColorButton colorButton;

	public bool highlightDebug;

	private bool isBlinking;

	private int indexBlinking;

	private void Start()
	{
		this.buttonChildrenObjects = new Image[base.transform.childCount];
		this.originalChildrenPos = new Vector2[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			this.buttonChildrenObjects[i] = base.transform.GetChild(i).GetComponent<Image>();
			this.originalChildrenPos[i] = this.buttonChildrenObjects[i].GetComponent<RectTransform>().anchoredPosition;
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

	public void OnPointerEnter(PointerEventData data)
	{
		if (!this.buttonPressed)
		{
			return;
		}
		this.PressButton();
	}

	public void OnPointerExit(PointerEventData data)
	{
		this.ReleaseButton();
	}

	public void OnPointerUp(PointerEventData data)
	{
		this.buttonPressed = false;
		this.ReleaseButton();
	}
}
