using System;
using UnityEngine;

public class GiftBoxBouncing : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetComponent<GiftBox>().PlayBouncingSFX();
	}
}
