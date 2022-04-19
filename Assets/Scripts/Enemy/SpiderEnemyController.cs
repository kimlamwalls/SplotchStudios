using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Pathfinding;

public class SpiderEnemyController : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    [SerializeField] public float maxDistance = 5f; 

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
}
