using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    public SlimeAttack slimeAttack;

    private SpawnManager spawnManager;



    private void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float Health
    {
        set
        {
            health = value;
            if(health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health = 3;


    public void Defeated()
    {
        animator.SetTrigger("death");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);

        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        spawnManager.CountEnemy();

    }

    public void TakeDamage()
    {
        animator.SetTrigger("takenDamage");
    }

    public void LookAtPlayer(float posX)
    {
        if (posX < rb.transform.position.x)
        {
            spriteRenderer.flipX = true;
            slimeAttack.attackDirection = SlimeAttack.AttackDirection.left;
        }
        else
        {
            spriteRenderer.flipX = false;
            slimeAttack.attackDirection = SlimeAttack.AttackDirection.right;
        }
    }

    public void StartSlimeAttack()
    {
        slimeAttack.Attack();
    }

    public void EndSlimeAttack()
    {
        slimeAttack.StopAttack();
    }

 














}
