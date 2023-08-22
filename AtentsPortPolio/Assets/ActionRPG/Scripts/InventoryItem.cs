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
    // ������ ���� ���¸� ����Ʈ ������Ƽ��� �Ѵ�
    // get, set�� ���������ڸ� ���� ������ �� �ִ�.
    public Transform orgParent { get; private set; }
    public Image myIcon;
    // override�� ���ؼ� �θ��� �Լ��� ������ش�.
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
        // �̹� ������ ���� �ִ� �����ۿ� ����Ǵ� �Լ�
        base.ResetParent(newParent);
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;

        transform.parent.GetComponent<ISetChild>()?.SetItem(this);
        // null���� �ƴ��� üũ
    }

    // Begin > Drag > Drop > End
}
