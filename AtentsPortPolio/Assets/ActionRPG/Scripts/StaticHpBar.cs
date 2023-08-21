using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticHpBar : MonoBehaviour, IChangeValue
{
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValue(float v)
    {
        mySlider.value = v;
    }
}
