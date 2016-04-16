using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEx : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPointerDownEnter;
    public UnityEvent onPointerDownStay;
    public UnityEvent onPointerDownExit;
    bool _pointerDowned = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDowned = true;
        onPointerDownEnter.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerDowned = false;
        onPointerDownExit.Invoke();
    }

    void Update()
    {
        if (_pointerDowned)
            onPointerDownStay.Invoke();
    }
}