using UnityEngine;
using System.Collections;

public class ZombieHandler : MonoBehaviour
{
    static Transform playerTr = null;
    NavMeshAgent nma;

    void Awake()
    {
    //    if (playerTr == null)
    //        playerTr = FindObjectOfType<PlayerHandler>().transform;
    //    StartCoroutine(RenewDestination());
    //    nma = GetComponent<NavMeshAgent>();
    }

    IEnumerator RenewDestination()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            nma.SetDestination(playerTr.position);
        }
    }
}