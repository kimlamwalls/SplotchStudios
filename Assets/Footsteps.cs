using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [FMODUnity.EventRef]
    public string inputsound;
    bool playerismoving;
    public float walkingSpeed;


    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            //Debug.Log ("Player is moving");
            playerismoving = true;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
        {
            //Debug.Log ("Player is not moving");
            playerismoving = false;
        }
    }


    void CallFootsteps()
    {
        if (playerismoving == true)
        {
            //Debug.Log ("Player is moving");
            FMODUnity.RuntimeManager.PlayOneShot(inputsound);
        }
    }


    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }


    void OnDisable()
    {
        playerismoving = false;
    }
}