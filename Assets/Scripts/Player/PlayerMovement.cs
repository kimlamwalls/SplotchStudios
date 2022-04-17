using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float sanityMultiplier = 1f;
    
    public float moveSpeed = 0.7f;

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
        // even though the player is facing a different direction, so we store the lastvector movement of the player
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

    void FixedUpdate()
    {
        // move player
        rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        bool inLight = false;
        foreach (var light in lights)
        {
            // get distance fromplayer to light object
            var distance = Vector3.Distance(light.transform.position, rb.position);
            // check if player is inside the lights outside radius
            if (distance < light.pointLightOuterRadius)
            {
                Debug.Log("In light radius");
                inLight = true;
                break;
            }
            // Debug.Log($"Distance to light: {distance}, light outer radius: {light.pointLightOuterRadius}");
        }

        // slowly increment/decrement the santy value based on if the user is within the light
        if (inLight)
        {
            sanity.value += sanityMultiplier * Time.deltaTime;
        }
        else
        {
            sanity.value -= sanityMultiplier * Time.deltaTime;
        }
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Attack");
    }
}
