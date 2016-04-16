using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    RectTransform _rt;
    Vector2 _originPos;
    bool _moving = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _moving = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _moving = false;
        _rt.anchoredPosition = _originPos;
    }

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _originPos = _rt.anchoredPosition;
    }

    void Update()
    {
        if (_moving)
        {
            _rt.position = Input.mousePosition;
            var toPosition = _rt.anchoredPosition - _originPos;
            InputHandler.instance.onMove(toPosition.normalized);
        }
        else
            InputHandler.instance.onStop();
    }

    public void OnDrag(PointerEventData eventData)
    { }
}