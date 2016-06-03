using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour
{
    float elapsed = 0;
	
	void Update () {
        elapsed += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Sin(elapsed * 4) / 3 + 1, transform.position.z);
        transform.Rotate(0, Time.deltaTime * 180, 0, Space.World);
	}

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player")
        {
            FindObjectOfType<PlayerHandler>().AddAmmo(30);
            Destroy(gameObject);
        }
    }
}
