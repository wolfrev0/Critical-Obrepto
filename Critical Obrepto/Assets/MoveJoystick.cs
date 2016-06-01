using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    RectTransform rt;
    Vector2 originPos;
    bool moving = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        moving = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        moving = false;
        rt.anchoredPosition = originPos;
    }

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        originPos = rt.anchoredPosition;
    }

    void Update()
    {
        if (InputHandler.instance.UIType != UIType.Mobile)
            return;
        if (moving)
        {
            rt.position = Input.mousePosition;
            var toPosition = rt.anchoredPosition - originPos;
            InputHandler.instance.onMove(toPosition.normalized);
        }
        else
            InputHandler.instance.onStop();
    }

    public void OnDrag(PointerEventData eventData)
    { }
}