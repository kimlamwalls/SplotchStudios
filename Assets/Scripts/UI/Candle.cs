using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Candle : MonoBehaviour
{
    // Components
    [SerializeField] new Light2D light;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private bool lightsOn = true;
    [SerializeField] private Animator animator;
    public float minIntensity = 1.1f;
    public float maxIntensity = 1.2f;
    public float lightSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (!lightsOn)
        {
            LightsOff();
            return;
        }
        
        LightsOn();
    }

    // Update is called once per frame
    void Update()
    {
        if(lightsOn)
            light.intensity = Mathf.PingPong(Time.time * lightSpeed, maxIntensity) + minIntensity;
    }

    public void LightsOff()
    {
        animator.SetBool("Playing", false);
        animator.gameObject.SetActive(false);
        
        particles.Stop();
        
        light.intensity = 0f;
        lightsOn = false;
    }
    
    public void LightsOn()
    {
        if (animator.gameObject.activeSelf)
        {
            animator.gameObject.SetActive(true);
            animator.SetBool("Playing", true);
        }
        
        particles.Play();
        lightsOn = true;
    }
}
