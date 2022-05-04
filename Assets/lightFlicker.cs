using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lightFlicker : MonoBehaviour

{

    public GameObject lighting2d;
    private Light2D lightingComponent;
    private double secondsToWait = 1.3f;
    private float startIntensity;
    private float brightnessOfFlash = 12;

    private void Awake()
    {   
        lighting2d = GameObject.Find("Dungeon_Global_Light");
        lightingComponent = lighting2d.GetComponent<Light2D>();
        startIntensity = lightingComponent.intensity;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Flash", 0.8f, 0.2f);
        InvokeRepeating("TurnOffFlash", 0.9f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > secondsToWait)
        {
            lightingComponent.intensity = startIntensity;
            CancelInvoke("Flash");
        }
    }

    void Flash()
    {
        lightingComponent.intensity = brightnessOfFlash;
    }

    void TurnOffFlash()
    {
        lightingComponent.intensity = startIntensity;
    }
}
