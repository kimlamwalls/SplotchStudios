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

    AdventureLog log;
    PlayerHealthBar hb;

    int count = 0;
    
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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // DEBUG :: REMOVE LATER
        // triggers the damage animation
        if(Input.GetKeyDown(KeyCode.T))
        {
            animator.SetTrigger("TakeDamage");
            hb.Damage(5);
            log.AddEventMessage("Damage " + count);
            count++;
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void MeleeAttack()
    {
        animator.SetTrigger("Attack");
    }
}
