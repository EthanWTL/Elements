using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    Vector2 rightAttackOffset;
    Vector2 upAttackOffset;
    Vector2 downAttackOffset;
    public Collider2D swordCollider;

    public float damage = 1;

    public float[] swordElement = new float[] { 3, 3, 3, 3, 3 };

    public float elementBuff;
    public float elementAdder;

    public enum AttackDirection
    {
        left,right,up,down
    }

    public AttackDirection attackDirection;

    private void Start()
    {
        rightAttackOffset = transform.localPosition;
        upAttackOffset = new Vector2(0, -0.02f);
        downAttackOffset = new Vector2(0, -0.2f);
    }

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
            case AttackDirection.up:
                AttackUp();
                break;
            case AttackDirection.down:
                AttackDown();
                break;
        }
    }

    private void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    private void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    private void AttackUp()
    {
        swordCollider.enabled = true;
        transform.localPosition = upAttackOffset;
    }

    private void AttackDown()
    {
        swordCollider.enabled = true;
        transform.localPosition = downAttackOffset;
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                if(elementBuff != 0)
                {
                    enemy.Health -= (damage + elementAdder) * elementBuff;
                    enemy.TakeDamage();
                }
                else
                {
                    enemy.Health -= damage;
                    enemy.TakeDamage();
                }
                
            }
        }
    }
}
