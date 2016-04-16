using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEx : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPointerDownStay;
    bool _pointerDowned = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDowned = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerDowned = false;
    }

    void Update()
    {
        if (_pointerDowned)
            onPointerDownStay.Invoke();
    }
}