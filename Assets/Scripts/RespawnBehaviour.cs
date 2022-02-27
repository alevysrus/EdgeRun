using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    public CharacterController player;
    public GameObject respawn;

    private void OnTriggerEnter(Collider other)
    {
        player.transform.SetPositionAndRotation(respawn.transform.position, respawn.transform.rotation);
        Activators.deathIndex = true;
    }
    private void Update()
    {
        if (Activators.isEasy == 0 && Input.GetKey(KeyCode.F))
        {
            respawn.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        }
    }
}