using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODSanity : MonoBehaviour
{

    FMODUnity.EmitterRef Sanity;
    public float slider;

    [SerializeField] [Range(0f, 100f)] public float sanity = 0;

    // Start is called before the first frame update
    FMODUnity.StudioEventEmitter emitter;
    void Start()
    {
        var target = GameObject.Find("MusicDetection");
        emitter = target.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    void Update()
    {
        emitter.SetParameter("Sanity", sanity);
    }
    
    public void setViaSlider(float value)
    {
        sanity = value;
    }
}