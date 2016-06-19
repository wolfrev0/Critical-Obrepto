using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseDialog : MonoBehaviour
{
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
        Time.timeScale = 0;
    }

    public void Hide()
    {
        SoundManager.instance.PlayButton();
        destination = new Vector2(0, 720);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlayButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMain()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlayButton();
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        rt.anchoredPosition *= 9;
        rt.anchoredPosition += destination;
        rt.anchoredPosition /= 10;
    }
}
