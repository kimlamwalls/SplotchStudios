using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Pathfinding;

public class SpiderEnemyController : EnemyShared
{
    public AIPath aiPath;
    public Animator animator;
    [SerializeField] private float maxDistance = 5f;
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player collision");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.remainingDistance > maxDistance)
        {
            aiPath.canMove = false;
            animator.SetBool("Moving", false);
            return;
        }

        aiPath.canMove = true;
        
        if (aiPath.velocity != Vector3.zero)
        {
            animator.SetBool("Moving", true);
        }
        animator.SetFloat("Horizontal", aiPath.desiredVelocity.x);
        // Debug.Log($"x velo: {aiPath.desiredVelocity.x} || y velo: {aiPath.desiredVelocity.y}" );
        // Debug.Log($"Distance: {aiPath.remainingDistance}");
    }

    public override void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }
    
}
