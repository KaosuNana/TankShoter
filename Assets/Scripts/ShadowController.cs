using System;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
	public GameObject iceBallInstance;

	private void LateUpdate()
	{
		base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
		float num = 3f - this.iceBallInstance.transform.position.y * 0.1f;
		base.GetComponent<RectTransform>().localScale = new Vector3(num, num, num);
	}
}
