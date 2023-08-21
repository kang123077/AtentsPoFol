using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillIcon : MonoBehaviour, IPointerClickHandler
{
    // IPointerClickHandler 는 마우스 클릭에 대한 이벤트를 실행시켜주는 인터페이스다.
    // 마우스 클릭에 따른 일종의 옵저버 패턴을 제공한다.
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