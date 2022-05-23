using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightningTrigger : Collidable
{
    public string[] sceneNames;
    public GameObject lighting;
    private Light2D lightingComponent;
    private float startIntensity;
    private float brightnessOfFlash = 12;
    private int flashCounter = 0;

    //Component dungeon lighting.GetComponent("light 2D");

    private void Awake()
    { 
        lighting = GameObject.Find("Dungeon_Global_Light");
        lightingComponent = lighting.GetComponent<Light2D>();
    }

    void Start()
    {
        //get the default lighting
        startIntensity = lightingComponent.intensity;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //invoke lighting flashes   
            InvokeRepeating("Flash", 1.23f, 0.2f);
            InvokeRepeating("TurnOffFlash", 1.33f, 0.2f);
        }
    }

    void Update()
    {
        if (flashCounter>=3) 
        {
            lightingComponent.intensity = startIntensity;
            CancelInvoke("Flash");
        }
    }
    
    
   
    void Flash()
    {
        lightingComponent.intensity = brightnessOfFlash;
        flashCounter += 1;
    }   
   
    void TurnOffFlash()
    {
        lightingComponent.intensity = startIntensity;
    }
}