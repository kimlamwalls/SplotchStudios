using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Random = UnityEngine.Random;

public class Portal : Collidable
{
    public string[] sceneNames;
    public GameObject lighting;
    private Light2D lightingComponent;
    private double secondsToWait = 1.3f;
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
            // Teleport the player 
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        //invoke lighting flashes   
             InvokeRepeating("Flash", 0.8f, 0.2f);
             InvokeRepeating("TurnOffFlash", 0.9f, 0.2f);

        }
    }

   void Update()
   {
       if (flashCounter >= 3) ;
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
