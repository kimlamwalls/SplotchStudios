using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float sanityMultiplier = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float moveSpeed = 0.7f;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private Transform attackLocation;
    // Component references
    public Rigidbody2D rb;
    public Animator animator;

    // player movement vector
    Vector2 movement;
    // the players last movement vector
    Vector2 prevVector;

    AdventureLog log;
    PlayerHealthBar hb;
    Slider sanity;

    // store all the lights in the game
    private Light2D[] lights;
    
    
    // message booleans
    private bool loggedLowSanity;
    
    void Awake()
    {
        log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
        hb = GameObject.Find("PlayerHealthBar").GetComponent<PlayerHealthBar>();
        sanity = GameObject.Find("SanitySlider").GetComponent<Slider>();
        var lightObjects= GameObject.FindGameObjectsWithTag("LIGHT");
        lights = lightObjects.Select(l => l.GetComponent<Light2D>()).ToArray();
    }

    void Start()
    {
        log.AddEventMessage("I Awaken to the damp smells of water and stone");
        // TODO: add a bunch of Invoke functions at random times to trigger story elements
    }


    // Update is called once per frame
    void Update()
    {
        // Input
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");
        

        movement.x = x;
        movement.y = y;


        // when the player stops moving x and y reset to 0 which causes the down idle animation to play
        // even though the player is facing a different direction, so we store the last vector movement of the player
        // and use that as the animation state, as this is only updated when the player moves, 
        // the player now idles in the last direction they were moving
        if (x != 0 || y != 0)
        {
            prevVector.x = x;
            prevVector.y = y;
        }

        // update animation state
        animator.SetFloat("Horizontal", prevVector.x);
        animator.SetFloat("Vertical", prevVector.y);
        

        animator.SetFloat("Speed", movement.sqrMagnitude);

        // DEBUG :: REMOVE LATER
        // triggers the damage animation
        if(Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("TakeDamage");
            hb.Damage(5);
            log.AddEventMessage("Damage taken"); // TODO: REMOVE THIS LOG
            
        }

        // 'P' key is for attack
        if (Input.GetKeyDown(KeyCode.P))
        {
            MeleeAttack();
        }

        // Load death scene
        if (hb.Health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Death");
        } 
      
    }

    /// <summary>
    /// Will log any logs needed to be logged
    /// </summary>
    void LogAnyLogs()
    {
        // add a log when user has low sanity
        if (!loggedLowSanity && sanity.value < 20)
        {
            loggedLowSanity = true;
            log.LowSanityMessage();
        } else if (loggedLowSanity && sanity.value > 35) loggedLowSanity = false;
        
    }


    /// <summary>
    /// Damage the player, updates the players health bar and triggers the damage animation
    /// </summary>
    /// <param name="amount">The amount health points to remove from the player</param>
    public void Damage(float amount)
    {
        animator.SetTrigger("TakeDamage");
        hb.Damage(amount);
    }


    void FixedUpdate()
    {
        // move player
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        bool inLight = false;
        foreach (var light in lights)
        {
            // get distance from player to light object
            var distance = Vector3.Distance(light.transform.position, rb.position);

            // if player is outside of light radius skip this light
            if (distance > light.pointLightOuterRadius) continue;
            
            // Debug.Log("In light radius");
            inLight = true;
            break;
                // Debug.Log($"Distance to light: {distance}, light outer radius: {light.pointLightOuterRadius}");
        }

        // slowly increment/decrement the sanity value based on if the user is within the light
        if (inLight)
        {
            sanity.value += sanityMultiplier * Time.deltaTime;
            if (sanity.value > 95)
            {
                hb.Heal((sanityMultiplier * Time.deltaTime));
            }
        }
        else
        {
            sanity.value -= sanityMultiplier * Time.deltaTime;
            // slowly start damaging player while the y have low sanity
            if (sanity.value < 5)
            {
                hb.Damage((sanityMultiplier / sanity.value) * Time.deltaTime);
            }
            
        }
        
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Attack");
        
        // the below overlays a circle collider and checks for collisions
        
        // Collider2D[] enemies =  Physics2D.OverlapCircleAll(attackLocation.position, attackRange, enemyLayers);
        // foreach(var enemy in enemies)
        // {
        //     var enemyObj = enemy.gameObject.GetComponentInChildren<EnemyShared>();
        //     enemyObj.Hit(5);
        // }
        
        
        // The below uses a raycast method to detect hits
        var enemies = Physics2D.CircleCastAll(attackLocation.position, attackRange/2, movement, attackRange, enemyLayers);
        foreach(var enemy in enemies)
        {
            var obj = enemy.transform.gameObject.GetComponentInChildren<EnemyShared>();
            obj.Hit(105);
            // var enemyObj = enemy.collider.gameObject.GetComponent<EnemyShared>();
            // Debug.DrawLine(new Vector3(enemy.centroid.x, enemy.centroid.y), new Vector3(enemy.point.x, enemy.point.y), Color.green);
            // .collider.gameObject.GetComponentInChildren<EnemyShared>();
            // enemyObj.Hit(5);
        
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackLocation.position, attackRange/2);
    }
}
