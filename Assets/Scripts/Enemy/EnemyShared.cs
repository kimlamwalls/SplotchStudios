using System;
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

        public GameObject FloatingText;
        public abstract void Hit(float damage);

        /// <summary>
        /// Displays a floating text equally the damage amount when the player attacks
        /// </summary>
        /// <param name="value">The value to display</param>
        protected void DisplayText(float value)
        {
            // create a new text object
            var text = Instantiate(FloatingText, transform.position, Quaternion.identity);
            text.gameObject.GetComponentInChildren<TextMesh>().text = $"-{value:0.##}";
        }

        /// <summary>
        /// Destroys the game object
        /// </summary>
        protected void Kill()
        {
            Destroy(transform.parent.gameObject);
        }
        
    }
    
   
}