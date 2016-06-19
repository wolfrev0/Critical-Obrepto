using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public void OnHelp()
    { }

    public void OnStart()
    {
        SoundManager.instance.PlayButton();
        SceneManager.LoadScene("MapSelect");
    }

    public void OnRecords()
    { }

    public void OnQuit()
    {
        SoundManager.instance.PlayButton();
        Application.Quit();
    }
}