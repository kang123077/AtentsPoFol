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
        // 더블클릭을 원할 때는 eventData를 사용
        // if eventData.clickCount == 2
        myIcon.fillAmount = 0.0f;
        Debug.Log("click");
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