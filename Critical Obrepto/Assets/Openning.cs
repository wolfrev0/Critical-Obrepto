using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Openning : MonoBehaviour {

    Image image;

    int s = 1;

    void Start()
    {
        image = GetComponent<Image>();
        Screen.SetResolution(1280, 720, true);
    }

    void Update()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + s * Time.deltaTime);
        if (image.color.a < 0)
            s = 1;
        if (image.color.a > 1)
            s = -1;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SoundManager.instance.PlayButton();
            SceneManager.LoadScene("MainMenu");
        }
    }
}
