using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ParametersSetByNameContinuous : MonoBehaviour
{

    public FMODUnity.EventReference SelectedEvent;
    FMOD.Studio.EventInstance instance;
    
    [SerializeField] [Range(0f, 100f)] public float parameterValue;
    
    public string ParameterName =  "parameterName";
    
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetParameter(float value)
    {
        instance.setParameterByName(ParameterName, value);
        parameterValue = value;
    }
    
    
    

}
