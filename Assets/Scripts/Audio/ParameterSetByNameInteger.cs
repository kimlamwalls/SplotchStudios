using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ParameterSetByNameInteger : MonoBehaviour
{

    public FMODUnity.EventReference SelectedEvent;
    private FMOD.Studio.EventInstance instance;
    public int parameterValue;
    
    public string ParameterName =  "parameterName";
    
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
    }


    public void SetParameter(int value)
    {
        
        instance.setParameterByName(ParameterName, value);
        parameterValue = value;
        
    }
    
    
    

}
