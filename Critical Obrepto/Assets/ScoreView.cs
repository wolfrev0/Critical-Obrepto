using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour {
    public static ScoreView instance;
    Text text;
    int score = 0;

    void Awake()
    {
        instance = this;
        text = GetComponent<Text>();
    }
    public void AddScore(int amount)
    {
        score += amount;
        text.text = score.ToString();
    }
    public int GetScore()
    { return score; }
}
