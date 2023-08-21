using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillIcon : MonoBehaviour, IPointerClickHandler
{
    // IPointerClickHandler �� ���콺 Ŭ���� ���� �̺�Ʈ�� ��������ִ� �������̽���.
    // ���콺 Ŭ���� ���� ������ ������ ������ �����Ѵ�.
    public Image myIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myIcon.fillAmount = 0.0f;
        StartCoroutine(Cooling());
    }

    IEnumerator Cooling()
    {
        while (myIcon.fillAmount < 1.0f)
        {
            myIcon.fillAmount += Time.deltaTime;
            yield return null;
        }
        myIcon.fillAmount = 1.0f;
    }
}