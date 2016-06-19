using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    Text text;

    public string mapName;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Start ()
    {
        if (mapName == "")
            mapName = SceneManager.GetActiveScene().name;
        text.text = PlayerPrefs.GetInt(mapName + "HighScore").ToString();
	}
}
