using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeningLog : MonoBehaviour
{
    AdventureLog log;
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];
    private bool displayed;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // The array is not cleaned up, so we do it ourself
            hits[i] = null;
        }
    }

    // If player collides with Collider2D then call DisplayLog()
    void OnCollide(Collider2D coll)
        {
            if(coll.name == "Player")
                DisplayLog();
        }
    
    // Display text in event log
    void DisplayLog()
    {
        if(!displayed)
        {
            displayed = true;
            log = GameObject.Find("AdventureLogView").GetComponent<AdventureLog>();
            log.AddEventMessage("Your vision unblurs, dark caverns unveil themselves before you.\n" +
                                "Your mind is hazy as control returns.\n" + 
                                "A pendant sits in your hand, a strange symbol in yellow.");
        }
    }
}
