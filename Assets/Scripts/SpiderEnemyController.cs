using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpiderEnemyController : MonoBehaviour
{
    //TODO: refactor serializable fields into an interface or inherited class that an enemy can extend
    public AIPath aiPath;
    public Animator spiderAnimator;
    [SerializeField] private float maxDistance = 5f; 
    
    // Update is called once per frame
    void Update()
    {
        if (aiPath.remainingDistance > maxDistance)
        {
            aiPath.canMove = false;
            spiderAnimator.SetBool("Moving", false);
            return;
        }

        aiPath.canMove = true;
        
        if (aiPath.velocity != Vector3.zero)
        {
            spiderAnimator.SetBool("Moving", true);
        }
        spiderAnimator.SetFloat("Horizontal", aiPath.desiredVelocity.x);
        // Debug.Log($"x velo: {aiPath.desiredVelocity.x} || y velo: {aiPath.desiredVelocity.y}" );
        Debug.Log($"Distance: {aiPath.remainingDistance}");
    }
}
