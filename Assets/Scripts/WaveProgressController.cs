using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressController : MonoBehaviour
{
	public void SetProgressTo(float progress)
	{
		base.GetComponent<Image>().DOFillAmount(progress, 0.3f);
	}
}
