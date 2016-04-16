using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    RectTransform _rt;
    Vector2 _originPos;
    bool _update = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _update = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _update = false;
        _rt.anchoredPosition = _originPos;
    }

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _originPos = _rt.anchoredPosition;
    }

    void Update()
    {
        if (_update)
        {
            _rt.position = Input.mousePosition;
            var toPosition = _rt.anchoredPosition - _originPos;
            InputHandler.instance.onMove(toPosition.normalized);
        }
    }

    public void OnDrag(PointerEventData eventData)
    { }
}