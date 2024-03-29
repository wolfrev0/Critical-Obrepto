﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveJoystick : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    RectTransform rt;
    Vector2 originPos;
    Vector2 mousePos;
    bool moving = false;
    PlayerHandler player;

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
        player = FindObjectOfType<PlayerHandler>();
    }

    void Update()
    {
        if (player.died)
            return;
        if (InputHandler.instance.UIType != UIType.Mobile)
            return;
        if (moving)
        {
            rt.position = mousePos;
            var toPosition = rt.anchoredPosition - originPos;
            InputHandler.instance.onMove(toPosition.normalized);
        }
        else
            InputHandler.instance.onStop();
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = eventData.position;
    }
}