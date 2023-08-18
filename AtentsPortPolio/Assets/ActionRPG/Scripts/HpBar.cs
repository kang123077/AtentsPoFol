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
    // WorldToScreenPoint�Լ��� ��� ������� ��ġ�� ī�޶� ����� �� ȭ����� ��ǥ�� �˷��ش�.
    // x, y���� ��ũ�� ��ġ ���������� z������ +-�� ���� ī�޶� �������� ��, �ڸ� ������ �� �ִ�.
    public Transform myTarget;
    public Slider mySlider;
    void Start()
    {

    }
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
        // ī�޶��� ������ ������ �Ǻ��ؼ�, ��Ȱ��ȭ �ϰų�
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
