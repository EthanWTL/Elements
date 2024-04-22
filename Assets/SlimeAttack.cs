using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{

    PlayerHealth playerHealth;
    GameObject player;
    Vector2 rightAttackOffset;

    public float attackDamage = 1f;
    public Collider2D slimeAttackCollider;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public enum AttackDirection
    {
        left, right
    }

    public AttackDirection attackDirection;

    public void Attack()
    {
        switch (attackDirection)
        {
            case AttackDirection.left:
                AttackLeft();
                break;
            case AttackDirection.right:
                AttackRight();
                break;
        }

    }

    public void AttackRight()
    {
        slimeAttackCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        slimeAttackCollider.enabled = true;
        transform.localPosition = new Vector2 (rightAttackOffset.x * -1, rightAttackOffset.y);
    }


    public void StopAttack()
    {
        slimeAttackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }
}
