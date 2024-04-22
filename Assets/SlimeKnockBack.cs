using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKnockBack : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;

    Vector2 knockBackDirection;
    float knockBackDistance = 0.2f;
    float knockBackSpeed = 1.5f;

    Vector2 knockBackPosition;
    Vector2 knockBackMove;

    Vector2 playerActualPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        playerActualPosition = new Vector2(player.position.x, player.position.y - 0.1f);
        
        knockBackDirection = new Vector2(rb.transform.position.x - playerActualPosition.x, rb.transform.position.y - playerActualPosition.y).normalized;
        knockBackPosition = rb.position + knockBackDistance * knockBackDirection; 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knockBackMove = Vector2.MoveTowards(rb.position, knockBackPosition, knockBackSpeed * Time.fixedDeltaTime);
        rb.MovePosition(knockBackMove);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        knockBackDirection = Vector2.zero;
        knockBackPosition = Vector2.zero;
        knockBackMove = Vector2.zero;
        playerActualPosition = Vector2.zero;
    }


}
