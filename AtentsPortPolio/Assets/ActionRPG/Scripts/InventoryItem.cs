using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface IGetOrgParent
{
    Transform orgParent { get; }
}

public class InventoryItem : DragItem, IGetOrgParent
{
    // 다음과 같은 형태를 디폴트 프로퍼티라고 한다
    // get, set에 접근한정자를 따로 설정할 수 있다.
    public Transform orgParent { get; private set; }
    public Image myIcon;
    // override를 통해서 부모의 함수를 덮어씌워준다.
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        base.OnBeginDrag(eventData);
        orgParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        myIcon.raycastTarget = false;

        orgParent.GetComponent<ISetChild>()?.SetItem(null);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        base.OnEndDrag(eventData);
        transform.SetParent(orgParent);
        myIcon.raycastTarget = true;

        transform.parent.GetComponent<ISetChild>()?.SetItem(this);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        base.OnDrop(eventData);
    }

    public override void ChangeParent(Transform newParent)
    {
        base.ChangeParent(newParent);
        orgParent = newParent;
    }

    public override void ResetParent(Transform newParent)
    {
        // 이미 떨어진 곳에 있던 아이템에 실행되는 함수
        base.ResetParent(newParent);
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;

        transform.parent.GetComponent<ISetChild>()?.SetItem(this);
        // null인지 아닌지 체크
    }

    // Begin > Drag > Drop > End
}
