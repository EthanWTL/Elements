using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlimeRoam : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform player;

    public float roamingSpeed = 0.05f;
    Vector2 startingPosition;
    Vector2 roamingPosition;
    SpriteRenderer spriteRenderer;
    public float chaseDistance = 0.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        startingPosition = rb.transform.position;
        roamingPosition = GetRoamingPosition();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(roamingPosition.x - rb.position.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, roamingPosition, roamingSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if(Vector2.Distance(rb.position, roamingPosition) <= 0.1f)
        {
            roamingPosition = GetRoamingPosition();
        }

        if(Vector2.Distance(rb.position, player.position) < chaseDistance)
        {
            animator.SetTrigger("chasePlayer");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("chasePlayer");
    }

    public Vector2 GetRoamingPosition()
    {
        System.Random random = new System.Random();
        float randomX = (float)((random.NextDouble() * 2 - 1)/2);
        float randomY = (float)((random.NextDouble() * 2 - 1)/2);
        Vector2 roamingPosition = new Vector2(startingPosition.x + randomX, startingPosition.y + randomY);
        return roamingPosition;
    }


}
