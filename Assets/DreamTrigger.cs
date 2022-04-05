using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object Entered the Trigger");
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("object is within trigger");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("player left trigger");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
