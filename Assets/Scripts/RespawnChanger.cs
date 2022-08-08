using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnChanger : MonoBehaviour
{
    public GameObject[] intermediaterespawn;
    public int jopa = 0;
    private void OnTriggerEnter(Collider other)
    {
        jopa++;
        Activators.CountOfRespawn++;
        for (int i = 0; i < intermediaterespawn.Length; i++)
        {
            if (Activators.CountOfRespawn == i)
            {
                intermediaterespawn[i].SetActive(false);
                
            }
        }
    }
}
