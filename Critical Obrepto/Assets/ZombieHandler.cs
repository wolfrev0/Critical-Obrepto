using UnityEngine;
using System.Collections;

public class ZombieHandler : MonoBehaviour
{
    public int hp;
    public float power = 1;

    void Awake()
    {
        ZombieSpawner.zombieCount++;
        Minimap.instance.AddEnemy(transform);
    }

    public void ApplyDamage()
    {
        GetComponent<Animator>().SetTrigger("Wound");
        if (--hp <= 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
        }
    }

    void Attack()
    {
        PlayerHandler.instance.ApplyDamage((int)(Random.Range(10, 20) * power));
    }
}