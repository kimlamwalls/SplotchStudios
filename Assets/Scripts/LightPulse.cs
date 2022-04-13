using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightPulse : MonoBehaviour
{
    // Components
    [SerializeField] Light2D light;

    public float minIntensity = 1.1f;
    public float maxIntensity = 1.2f;
    public float lightSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.PingPong(Time.time * lightSpeed, maxIntensity) + minIntensity;
    }
}
