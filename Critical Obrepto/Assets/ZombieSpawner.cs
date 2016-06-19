using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public ZombieHandler[] pfZombie;
    public static int zombieCount = 0;
    SphereCollider coll;
    float totalElapsed = 0;
    float SpawnDelay = 10;
    float spawnElapsed = 20;

    void Awake()
    {
        coll = GetComponent<SphereCollider>();
        zombieCount = 0;
    }

    void Update()
    {
        totalElapsed += Time.deltaTime;
        spawnElapsed += Time.deltaTime;
        if (spawnElapsed > SpawnDelay && zombieCount < 20)
        {
            spawnElapsed -= SpawnDelay;
            ZombieHandler zombie = Instantiate(pfZombie[Random.Range(0, pfZombie.Length)]);
            float r = Random.Range(0, coll.radius);
            float seed = Random.Range(0f, Mathf.PI * 2);
            zombie.transform.position = new Vector3(Mathf.Sin(seed), 0, Mathf.Cos(seed)) * r + transform.position;
            SpawnDelay *= 0.95f;
            if (SpawnDelay < 2)
                SpawnDelay = 2;
        }
    }
}