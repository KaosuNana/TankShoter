using System;
using UnityEngine;

public class CapsuleBouncing : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<Capsule>().readyToOpen = true;
		animator.transform.parent.GetComponent<Capsule>().PlayBouncingSFX();
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<Capsule>().StopBouncingSFX();
	}
}
