using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    Text text;
    PlayerHandler player;

    void Awake()
    {
        text = GetComponent<Text>();
        player = FindObjectOfType<PlayerHandler>();
    }

    void Update()
    {
        text.text = player.hp.ToString();
    }
}
