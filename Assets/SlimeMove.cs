using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : StateMachineBehaviour
{

    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    public float speed = 0.10f;
    public float attackRange = 0.2f;
    bool isRight;

    Vector2 playerActualPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.LookAtPlayer(player.position.x);

        isRight = checkRight();

        if (isRight)
        {
            Vector2 target = new Vector2(player.position.x + 0.2f, player.position.y - 0.1f);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        else
        {
            Vector2 target = new Vector2(player.position.x - 0.2f, player.position.y - 0.1f);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }



        playerActualPosition = new Vector2(player.position.x, player.position.y - 0.1f);
        if (Vector2.Distance(rb.position, playerActualPosition) <= attackRange & Math.Abs(rb.position.y - playerActualPosition.y) <= 0.01f)
        {
            animator.SetTrigger("attack");
        }

        float distanceToPlayer = Vector2.Distance(rb.position, player.position);
        animator.SetFloat("distanceToPlayer", distanceToPlayer);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.SetFloat("distanceToPlayer", 0);
    }

    public bool checkRight()
    {
        if(player.position.x - rb.position.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
