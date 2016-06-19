using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    new AudioSource audio;
    public AudioClip button;
    public AudioClip playerDie;
    public AudioClip step;
    public AudioClip breath;

    void Awake()
    {
        instance = this;
        audio = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayButton()
    {
        audio.clip = button;
        audio.Play();
    }

    public void PlayStep()
    {
        audio.clip = step;
        audio.loop = true;
        audio.Play();
    }

    public void StopStep()
    {
        audio.loop = false;
    }

    public void PlayPlayerDie()
    {
        audio.clip = playerDie;
        audio.Play();
    }

    public void PlayBreath()
    {
        audio.clip = breath;
        audio.Play();
    }
}
