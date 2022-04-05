using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;



/// <summary>
/// /casts float to int first
/// </summary>
public class ParameterSetByNameInteger : MonoBehaviour
{

    public FMODUnity.EventReference SelectedEvent;
    private FMOD.Studio.EventInstance instance;
    public string ParameterName =  "parameterName";
    public float sanityInput;
    public string ParameterName2 = "parameterName2";
    public float trackSelect;
    private int trackInt;
   

    
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
    }
    
    public void SetTrackSelect(float value)
    {
        trackInt = (int)value;
        instance.setParameterByName(ParameterName, trackInt);
        trackSelect = value;
    }

    public void SetSanity(float value)
    {
        instance.setParameterByName(ParameterName, sanityInput);
        sanityInput = value;
    }
    
    
    

}
