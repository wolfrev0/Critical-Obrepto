using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float delay = 0;

    void Awake()
    {
        Invoke("OnTimeEnd", delay);
    }

    void OnTimeEnd()
    {
        Destroy(gameObject);
    }
}