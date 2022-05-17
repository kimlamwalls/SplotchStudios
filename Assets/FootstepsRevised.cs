using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FootstepsRevised : MonoBehaviour
{
    
    public FMODUnity.StudioEventEmitter Footsteps;
    private bool playerIsMoving = false;
    public float walkingSpeed;
    public string CurrentTerrainTag;
    private int TerrainValue;
    private string[] TerrainTypes = {"Wood", "Stone Floor"} ;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("DialogueManager") != null)
        {
            if (DialogueManager.GetInstance().dialogueIsPlaying)
            {
                playerIsMoving = false;
            }
            else if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
            {
                //Debug.Log ("Player is moving");
                playerIsMoving = true;
            }
            else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
            {
                //Debug.Log ("Player is not moving");
                playerIsMoving = false;
            }
        }
        

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //string.IsNullOrEmpty(CollisionTag) ||
        for (int i = 0; i<TerrainTypes.Length; i++) {
            if (string.IsNullOrEmpty(TerrainTypes[i]) || other.CompareTag(TerrainTypes[i]))
            {
                CurrentTerrainTag = other.tag;
                Debug.Log("entered surface: " + CurrentTerrainTag);
            }
        }
    }

    void CallFootsteps()
    {
        if (playerIsMoving == true)
        {
            MaterialCheck();
            Footsteps.Play();
            Footsteps.SetParameter("Terrain", TerrainValue);
            //Debug.Log ("Player is moving, Terrain value is " + TerrainValue );
            
        }
    }

    public void MaterialCheck()
    {
        if (String.Equals(CurrentTerrainTag, "Wood")){
            TerrainValue = 1;
            Debug.Log("terrain value set to wood and 0");
        }
        
        if (String.Equals(CurrentTerrainTag,"Stone Floor")){
            TerrainValue = 0;
            Debug.Log("terrain value set to stone floor and 1");
        }
    }
    


}
