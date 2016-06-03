using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoView : MonoBehaviour
{
    Text text;
    PlayerHandler player;

	void Awake ()
    {
        text = GetComponent<Text>();
        player = FindObjectOfType<PlayerHandler>();
	}

	void Update ()
    {
        text.text = player.ammo.ToString();
	}
}
