
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag:MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPos;
    private Vector2 draggingPos;
    private Vector2 endPos;

    public bool isDragging = false;
    public float deltaY = 0f;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggingPos = eventData.position;
        deltaY = draggingPos.y - startPos.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        endPos = eventData.position;
        deltaY = endPos.y - startPos.y;
    }
}
