using UnityEngine;
using System.Collections;

public class FirstAid : MonoBehaviour {

    float elapsed = 0;
    float originYPos;

    void Start()
    {
        Minimap.instance.AddHp(transform);
        originYPos = transform.position.y;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, originYPos + Mathf.Sin(elapsed * 4) / 3 + 1, transform.position.z);
        transform.Rotate(0, Time.deltaTime * 180, 0, Space.World);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            Minimap.instance.Remove(transform);
            FindObjectOfType<PlayerHandler>().ApplyHeal(50);
            transform.position += new Vector3(1000, 1000, 1000);
            Invoke("Respawn", 120);
        }
    }

    void Respawn()
    {
        Minimap.instance.AddHp(transform);
        transform.position -= new Vector3(1000, 1000, 1000);
    }
}
