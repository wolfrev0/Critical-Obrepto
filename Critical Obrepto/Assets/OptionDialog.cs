using UnityEngine;
using System.Collections;

public class OptionDialog : MonoBehaviour {
    RectTransform rt;
    Vector2 destination;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        rt.anchoredPosition = destination = new Vector2(0, 720);
    }

    public void Show()
    {
        SoundManager.instance.PlayButton();
        destination = new Vector2(0, 0);
    }

    public void Hide()
    {
        SoundManager.instance.PlayButton();
        destination = new Vector2(0, 720);
    }

    void Update()
    {
        rt.anchoredPosition *= 9;
        rt.anchoredPosition += destination;
        rt.anchoredPosition /= 10;
    }
}
