using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
        // find all lights in the game that can restore the players sanity
        var lightObjects= GameObject.FindGameObjectsWithTag("LIGHT");
        lights = lightObjects.Select(l => l.GetComponentInChildren<Light2D>()).ToArray();
    }
    
    // Update is called once per frame
    void Update()
    {
        //stops movement if in dialogue screen
        if (GameObject.Find("DialogueManager") != null)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                movement.x = 0;
                movement.y = 0;
                animator.SetFloat("Speed", movement.sqrMagnitude);
                return;
            }
        }
        
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
        var inLight = false;
        foreach (var light2D in lights)
        {
            // if the lights intensity is 0 then they are currently turned off
            if(light2D.intensity == 0) continue;
            
            // get distance from player to light object
            var distance = Vector3.Distance(light2D.transform.position, rb.position);

            // if player is outside of light radius skip this light
            if (distance > light2D.pointLightOuterRadius) continue;
            
            inLight = true;
            break;
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
        const float critAttackChance = 0.01f;
        const float critMultiplier = 6.5f;
        
        animator.SetTrigger("Attack");
        
        const int baseAttack = 10;
        // The below uses a raycast method to detect hits
        var enemies = Physics2D.CircleCastAll(attackLocation.position, attackRange/2, movement, attackRange, enemyLayers);
        foreach(var enemy in enemies)
        {
            var obj = enemy.transform.gameObject.GetComponentInChildren<EnemyShared>();
            
            if (Random.value <= critAttackChance) obj.Hit(baseAttack * critMultiplier);
            else obj.Hit(baseAttack);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackLocation.position, attackRange/2);
    }

    /// <summary>
    /// Triggered by enemies when they attack.
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col)
    {
        var damage = 5f;
        
        // if the collider wasn't triggered by an enemy then ignore it
        if (!col.collider.CompareTag("Enemy")) return;

        if (col.collider.name == "Boss")
            damage *= 2.5f;
        
        // damage player
        Damage(damage);
    }
}
