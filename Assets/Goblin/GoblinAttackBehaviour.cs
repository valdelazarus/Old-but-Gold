using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<GoblinBehaviour>().anim.SetBool("Punch_b", false);
        animator.GetComponent<GoblinBehaviour>().anim.SetBool("hit", false);

    }
}
