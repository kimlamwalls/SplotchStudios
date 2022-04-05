using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Sanity : MonoBehaviour
{
    
    [SerializeField] [Range(0f, 100f)] public float sanityVal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setSanity(sanityVal);
    }

    public void setSanity(float amount)
    {
        sanityVal = amount;
    }
    
    public void increaseSanity(float amount){}
    
    public void decreaseSanity(float amount){}
    
    
}
