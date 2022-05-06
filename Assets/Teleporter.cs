using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
   private Transform destination;
   public bool isEast;
   public float distance = 0.2f;

   void start()
   {
       if (isEast == false)
       {
           destination = GameObject.FindGameObjectWithTag("TeleporterEast").GetComponent<Transform>();
       } else
       {
           destination = GameObject.FindGameObjectWithTag("TeleporterGate").GetComponent<Transform>();
       }
   }

   void OnTriggerEnter2D(Collider2D other)
   {
       if (Vector2.Distance(transform.position, other.transform.position) > distance)
       {
           other.transform.position = new Vector2 (destination.position.x, destination.position.y);
       }
   }
}
