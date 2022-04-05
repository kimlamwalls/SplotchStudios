using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ParametersSetByNameContinuous : MonoBehaviour
{

    public FMODUnity.EventReference SelectedEvent;
    FMOD.Studio.EventInstance instance;
    

    public string ParameterName =  "ParameterName";
    [SerializeField] [Range(0f, 100f)] public float sanity;
    
    public string ParameterName2 = "ParameterName2";
    [SerializeField] [Range(0, 1)] public int trackSelect;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(SelectedEvent);
        instance.start();
    }

    public void SetSanity(float value)
    {
        instance.setParameterByName(ParameterName, value);
    }
    
    public void SetTrackSelect(int value)
    {
        instance.setParameterByName(ParameterName2, value);
    }

    // Update is called once per frame
    void Update()
    {
        SetSanity(sanity);
        SetTrackSelect(trackSelect);
    }


    
    

}
