using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IChangeParent
{
    void ChangeParent(Transform newParent);
}

public interface IResetParent
{
    void ResetParent(Transform newParent);
}

public interface IDragItem : IChangeParent, IResetParent
{
}

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IDragItem
{
    Vector3 orgPos = Vector3.zero;
    Vector2 dragOffset = Vector3.zero;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        orgPos = transform.position;
        // UI���� ���Ǳ� ������ Z���� ����.
        dragOffset = (Vector2)transform.position - eventData.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // input�� ���콺�����ǵ� eventData�� ���� �����ϴ�.
        transform.position = eventData.position + dragOffset;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.position = orgPos;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {

    }

    public virtual void ChangeParent(Transform newParent)
    {
        orgPos = newParent.position;
    }

    public virtual void ResetParent(Transform newParent)
    {
        throw new System.NotImplementedException();
    }
}
