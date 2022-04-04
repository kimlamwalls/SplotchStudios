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
    public float parameterValue;
    private int intParameter;

    public string ParameterName =  "parameterName";
    
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
    }


    public void SetParameter(float value)
    {
        intParameter = (int)value;
        instance.setParameterByName(ParameterName, intParameter);
        parameterValue = value;
    }
    
    
    

}
