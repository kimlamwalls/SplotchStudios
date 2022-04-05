using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  
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

   
    
    void Awake()
    {
        log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
        hb = GameObject.Find("PlayerHealthBar").GetComponent<PlayerHealthBar>();
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
  

        Debug.Log($"x: {x}, y:{y}");

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
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Attack");
    }
}
