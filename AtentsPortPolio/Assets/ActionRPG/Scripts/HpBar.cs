using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDeadMsg
{
    void OnDead();
}

public interface IChangeValue
{
    void UpdateValue(float v);
}

public class HpBar : MonoBehaviour, IChangeValue, IDeadMsg
{
    // WorldToScreenPoint함수의 경우 월드상의 위치를 카메라에 찍었을 때 화면상의 좌표로 알려준다.
    // x, y값은 스크린 위치 기준이지만 z값에는 +-를 통해 카메라를 기준으로 앞, 뒤를 구별할 수 있다.
    public Transform myTarget;
    public Slider mySlider;
    void Start()
    {

    }
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        // 카메라의 뒤인지 앞인지 판별해서, 비활성화 하거나
        if (pos.z < 0.0f)
        {
            mySlider.gameObject.SetActive(false);
        }
        else
        {
            mySlider.gameObject.SetActive(true);
            transform.position = pos;
        }
    }

    public void Initialize(Transform target)
    {
        myTarget = target;
    }

    public void UpdateValue(float v)
    {
        mySlider.value = v;
    }

    public void OnDead()
    {
        myTarget = null;
        Destroy(gameObject);
    }
}
