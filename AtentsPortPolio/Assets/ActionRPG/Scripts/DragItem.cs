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
        // UI에서 사용되기 때문에 Z값이 없다.
        dragOffset = (Vector2)transform.position - eventData.position;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        // input의 마우스포지션도 eventData와 값이 동일하다.
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
