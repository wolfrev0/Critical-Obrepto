using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour
{
    RectTransform rt;
    Vector2 destination;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, 0);
        Invoke("Hide", 2);
    }

    void Hide()
    {
        destination = new Vector2(0, 720);
    }
    void Update()
    {
        rt.anchoredPosition *= 9;
        rt.anchoredPosition += destination;
        rt.anchoredPosition /= 10;
    }
}