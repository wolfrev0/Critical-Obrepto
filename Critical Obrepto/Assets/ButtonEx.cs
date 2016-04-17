using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEx : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onPointerDownEnter;
    public UnityEvent onPointerDownStay;
    public UnityEvent onPointerDownExit;
    bool pointerDowned = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDowned = true;
        onPointerDownEnter.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDowned = false;
        onPointerDownExit.Invoke();
    }

    void Update()
    {
        if (pointerDowned)
            onPointerDownStay.Invoke();
    }
}