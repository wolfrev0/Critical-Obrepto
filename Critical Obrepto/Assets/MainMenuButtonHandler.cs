using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public void OnHelp()
    { }

    public void OnStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnRecords()
    { }

    public void OnQuit()
    {
        Application.Quit();
    }
}