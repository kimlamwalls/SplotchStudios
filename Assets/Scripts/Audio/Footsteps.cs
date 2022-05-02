using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    public FMODUnity.EventReference SelectedEvent;
    private FMOD.Studio.EventInstance instance;
    bool playerismoving;
    public float walkingSpeed;
    public string nameOfParameter = "Footsteps Type: ";
    public string NameOfTag;
    public int SetToValue;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
    }
    
    
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
            FMODUnity.RuntimeManager.PlayOneShot(SelectedEvent);
        }
    }

    void SetFootstepsType(int value)
    {
        instance.setParameterByName(nameOfParameter, value);
    }




    void OnDisable()
    {
        playerismoving = false;
    }
}