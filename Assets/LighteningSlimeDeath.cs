using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningSlimeDeath : StateMachineBehaviour
{
    private PlayerElement playerElement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerElement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElement>();
        playerElement.AddLighteningElement(1);
    }
}
