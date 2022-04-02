using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanity : MonoBehaviour
{
    public float curSanity;
    public float maxSanity;
    public Slider sanityBar;
    
    // Start is called before the first frame update
    void Start()
    {
        curSanity = maxSanity;
        sanityBar.value = curSanity;
        sanityBar.maxValue = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        updateValue();
    }

    public void updateValue()
    {
        curSanity = sanityBar.value;
    }
    
}
