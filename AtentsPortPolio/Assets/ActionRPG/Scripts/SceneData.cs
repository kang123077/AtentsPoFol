using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData inst = null;

    public Transform hpBars;

    private void Awake()
    {
        inst = this;
    }
}
