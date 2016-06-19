using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieDialog : MonoBehaviour
{
    public static DieDialog instance;
    public Text highScoreView;
    public Text scoreView;
    RectTransform rt;
    Vector2 destination;

    void Awake()
    {
        instance = this;
        rt = GetComponent<RectTransform>();
        destination = rt.anchoredPosition;
    }

    public void Show(int score)
    {
        destination = new Vector3(0, 0, 0);
        string curSceneName = SceneManager.GetActiveScene().name;
        int highScore = PlayerPrefs.GetInt(curSceneName + "HighScore");
        if (highScore < score)
            PlayerPrefs.SetInt(curSceneName + "HighScore", score);
        highScoreView.text = highScore.ToString();
        scoreView.text = score.ToString();
    }

    public void Update()
    {
        rt.anchoredPosition *= 9;
        rt.anchoredPosition += destination;
        rt.anchoredPosition /= 10;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
