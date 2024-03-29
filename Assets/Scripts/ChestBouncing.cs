using System;
using UnityEngine;

public class ChestBouncing : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<Chest>().PlayBouncingSFX();
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.transform.parent.GetComponent<Chest>().StopBouncingSFX();
	}
}
