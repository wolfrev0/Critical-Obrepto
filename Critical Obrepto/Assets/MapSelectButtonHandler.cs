using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectButtonHandler : MonoBehaviour
{
    public void Desert()
    {
        SoundManager.instance.PlayButton();
        SceneManager.LoadScene("Desert");
    }

    public void Forest()
    {
        SoundManager.instance.PlayButton();
        SceneManager.LoadScene("Forest");
    }
}