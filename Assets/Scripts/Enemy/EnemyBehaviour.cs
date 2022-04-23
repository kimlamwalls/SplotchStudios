using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyShared : MonoBehaviour
    {
        public float health = 100f; 
        public abstract void Hit(float damage);

        // public bool CanMove()
        // {
        //     if (aiPath.remainingDistance < maxDistance) return true;
        //
        //     aiPath.canMove = false;
        //     return false;
        // }
    }
    
   
}