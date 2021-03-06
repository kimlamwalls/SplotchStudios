using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Pathfinding;

public class SpiderEnemyController : EnemyShared
{
    
    
    [SerializeField] private float maxDistance = 5f;

    private bool dead;

    void Start() {
        // set the starting health to something lower
        health = 40;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (dead) return;
        
        animator.SetFloat("Horizontal", aiPath.desiredVelocity.x);
        if (aiPath.remainingDistance > maxDistance)
        {
            aiPath.canMove = false;
            animator.SetBool("Moving", false);
            
        }
        else
        {
            aiPath.canMove = true;
            animator.SetBool("Moving", true);
        }

        if (aiPath.remainingDistance <= attackRange)
        {
            Attack();
        }
        
        
    }

    public override void Hit(float damage)
    {
        if(health <= 0) return;
        
        
        health -= damage;
        
        if (health <= 0)
        {
            aiPath.canMove = false;
            dead = true;
            animator.SetTrigger("Death");
            StartCoroutine(DestroyEnemy());
        } 
        
        Debug.Log("Enemy Hit: " + gameObject.name);
        DisplayText(damage);
    }

    IEnumerator DestroyEnemy()
    {
        Kill(1f);
        yield return null;
    }
}
