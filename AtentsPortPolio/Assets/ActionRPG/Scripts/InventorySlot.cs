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
            // �̹� ���� ������
            // DragItem�� ��ӹ��� InventoryItem�̹Ƿ� IRestParent�� ��ӹް��ִ�.
            (myItem as IResetParent).ResetParent(eventData.pointerDrag.GetComponent<IGetOrgParent>().orgParent);
        }
        // slot ���忡���� �ϴ��� ������ ����.
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
