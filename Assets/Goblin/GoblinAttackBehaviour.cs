using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttackBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GoblinBehaviour.anim.SetBool("Punch_b", false);
        GoblinBehaviour.anim.SetBool("hit", false);

    }
}
