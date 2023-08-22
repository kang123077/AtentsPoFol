using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISetChild
{
    void SetItem(InventoryItem item);
}

public class InventorySlot : MonoBehaviour, IDropHandler, ISetChild
{
    InventoryItem myItem = null;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (myItem != null)
        {
            // 이미 누가 있으면
            // DragItem을 상속받은 InventoryItem이므로 IRestParent를 상속받고있다.
            (myItem as IResetParent).ResetParent(eventData.pointerDrag.GetComponent<IGetOrgParent>().orgParent);
        }
        // slot 입장에서는 하단이 아이템 정보.
        IChangeParent cp = eventData.pointerDrag.GetComponent<IChangeParent>();
        if (cp != null )
        {
            cp.ChangeParent(transform);
        }
        myItem = eventData.pointerDrag.GetComponent<InventoryItem>();
    }

    public void SetItem(InventoryItem item)
    {
        myItem = item;
    }

    void Start()
    {
        myItem = GetComponentInChildren<InventoryItem>();
    }
}
