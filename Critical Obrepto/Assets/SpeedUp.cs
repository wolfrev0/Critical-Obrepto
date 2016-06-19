using UnityEngine;
using System.Collections;

public class SpeedUp : MonoBehaviour {

    float elapsed = 0;
    float originYPos;

    void Start()
    {
        Minimap.instance.AddSpeed(transform);
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
            FindObjectOfType<PlayerHandler>().SpeedUpOn();
            transform.position += new Vector3(1000, 1000, 1000);
            Invoke("Respawn", 100);
        }
    }

    void Respawn()
    {
        Minimap.instance.AddSpeed(transform);
        transform.position -= new Vector3(1000, 1000, 1000);
    }
}
