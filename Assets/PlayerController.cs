using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public bool canMove = true;
    public SwordAttack swordAttack;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        if (canMove)
        {
            //move character base on the input
            if (movementInput != Vector2.zero)
            {
                Vector2 direction = TryMove(movementInput);

                //try move left or right
                if (direction == Vector2.zero)
                {
                    direction = TryMove(new Vector2(movementInput.x, 0));

                    if (direction == Vector2.zero)
                    {
                        direction = TryMove(new Vector2(0, movementInput.y));
                    }

                }
                animator.SetInteger("movingRight", (int)(direction.x * 10));
                animator.SetInteger("movingUp", (int)(direction.y));
            }
            else
            {
                animator.SetInteger("movingRight", 0);
                animator.SetInteger("movingUp", 0);
            }

            //set the direction of the sprite into the movement direciton
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
                swordAttack.attackDirection = SwordAttack.AttackDirection.left;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
                swordAttack.attackDirection = SwordAttack.AttackDirection.right;
            }else if(movementInput.x == 0 & movementInput.y > 0)
            {
                swordAttack.attackDirection = SwordAttack.AttackDirection.up;
            }else if(movementInput.x == 0 & movementInput.y < 0)
            {
                swordAttack.attackDirection = SwordAttack.AttackDirection.down;
            }
        }
        
    }

    private Vector2 TryMove(Vector2 direction)
    {   
        //check collision
        int count = rb.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset);
        //move the character into the desired position
        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * direction * Time.fixedDeltaTime);
            return direction;
        }
        else
        {
            return Vector2.zero;
        }
    }





    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void PlayerAttack()
    {
        LockMovement();

        swordAttack.Attack();
    }

    public void EndSwordAttack()
    {
        UnLockMovement();

        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnLockMovement()
    {
        canMove = true;
    }
}
