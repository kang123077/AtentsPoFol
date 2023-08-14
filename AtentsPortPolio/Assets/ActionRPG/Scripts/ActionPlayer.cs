using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlayer : CharacterProperty
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);
    }
}
