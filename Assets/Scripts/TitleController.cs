using System;
using UnityEngine;

public class TitleController : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("TransitionIn", 0.5f);
	}

	private void TransitionIn()
	{
		base.GetComponent<Animator>().SetTrigger("TransitionIn");
	}
}
