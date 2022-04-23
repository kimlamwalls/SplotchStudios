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
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float moveSpeed = 0.7f;
    [SerializeField] private LayerMask enemyLayers;
    
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
            log.AddEventMessage("Damage taken");
            
        }

        // 'P' key is for attack
        if (Input.GetKeyDown(KeyCode.P))
        {
            MeleeAttack();
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
        Collider2D[] enemies =  Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
        // Damage enemy
        foreach(var enemy in enemies)
        {
            var enemyObj = enemy.gameObject.GetComponentInChildren<EnemyShared>();
            enemyObj.Hit(50);
            if (enemyObj.health <= 0)
            {
                Debug.Log("Enemy dead");
                Destroy(enemy.gameObject);
            }
            else
            {
                Debug.Log("Enemy Hit: " + enemy.name);    
            }

            
        }
    }
}
