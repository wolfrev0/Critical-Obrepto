using UnityEngine;
using System.Collections;

public class ZombieHandler : MonoBehaviour
{
    int hp = 3;

    void Awake()
    {}

    public void ApplyDamage()
    {
        if (--hp <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
            coll.GetComponent<PlayerHandler>().Die();
    }
}