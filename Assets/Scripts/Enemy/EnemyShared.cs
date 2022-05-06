using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Enemy
{
    public abstract class EnemyShared : MonoBehaviour
    {
        /// <summary>
        /// The enemies health, defaults to 100. Override this in your enemy controller start/awake function
        /// </summary>
        public float health = 100f;
        public Animator animator;
        public AIPath aiPath;
        [SerializeField] LayerMask playerLayer;
        [SerializeField] Transform attackLocation;
        [SerializeField] public float attackRange;
        [SerializeField] public float attackRate = 1f;
        private float nextAttackTime;
        

        public GameObject FloatingText;

        public abstract void Hit(float damage);
        
        /// <summary>
        /// Triggers the attack animation for the enemy
        /// The animator for this enemy should have a trigger called 'Attack'
        /// </summary>
        protected void Attack()
        {
            // exit if we cant attack or move
            if (Time.time <= nextAttackTime || aiPath.canMove == false) return;

            aiPath.canMove = false;
            animator.SetTrigger("Attack");
            
            // overlay a collider, this will be detected by the players OnColliderEnter2d function
            Physics2D.OverlapCircle(attackLocation.position, attackRange / 2, playerLayer);
  
            // set next attack time, this is required otherwise the player will die almost instantly
            nextAttackTime = Time.time + 1f / attackRate;
            Debug.Log("Next attack time: " + nextAttackTime);
            // allow the enemy to move after 2 seconds
            Invoke(nameof(EnemyCanMoveAgain), 2);
        }

        private void EnemyCanMoveAgain()
        {
            aiPath.canMove = true;
            Debug.Log("Enemy can now move again");
        }

       

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackLocation.position, attackRange/2);
        }

        /// <summary>
        /// Displays a floating text equally the damage amount when the player attacks
        /// </summary>
        /// <param name="value">The value to display</param>
        protected void DisplayText(float value)
        {
            // create a new text object
            var text = Instantiate(FloatingText, transform.position, Quaternion.identity);
            text.gameObject.GetComponentInChildren<TextMesh>().text = $"-{value:0}";
        }

        /// <summary>
        /// Destroys the game object
        /// </summary>
        /// <param name="delay">The delay in seconds to destroy the object, default is 0 == instant</param>
        protected void Kill(float delay = 0)
        {
            Debug.Log($"Destroying {gameObject.name} in {delay} seconds");
            Destroy(transform.parent.gameObject, delay);
        }
  
    }
    
   
}