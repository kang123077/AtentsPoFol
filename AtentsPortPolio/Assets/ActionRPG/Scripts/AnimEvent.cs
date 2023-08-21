using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent attackActs;
    public UnityEvent comboCheckStartActs;
    public UnityEvent comboCheckEndActs;

    public void OnAttack()
    {
        attackActs?.Invoke();
    }

    public void OnCombocheckStart()
    {
        comboCheckStartActs?.Invoke();
    }

    public void OnComboCheckEnd()
    {
        comboCheckEndActs?.Invoke();
    }
}