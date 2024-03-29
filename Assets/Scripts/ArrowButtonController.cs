using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButtonController : MonoBehaviour
{
	private bool isOn;

	public Image arrowImage;

	public void OnPressUniversalButton()
	{
		if (!this.isOn)
		{
			this.isOn = true;
			this.arrowImage.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 0f), 0.05f, RotateMode.Fast);
		}
		else
		{
			this.isOn = false;
			this.arrowImage.GetComponent<RectTransform>().DOLocalRotate(new Vector3(0f, 0f, 180f), 0.05f, RotateMode.Fast);
		}
	}
}
