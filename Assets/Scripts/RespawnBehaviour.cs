using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    public CustomController player;
    public GameObject[] respawn;

    private void OnTriggerEnter(Collider other)
    {
        Respawn(respawn[Activators.CountOfRespawn]);
    }
    private void Update()
    {
        if (Activators.isEasy == 0 && Input.GetKey(KeyCode.F))
        {
            respawn[Activators.CountOfRespawn].transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        }
    }
    public void Respawn(GameObject respawn)
    {
        player.transform.SetPositionAndRotation(respawn.transform.position, respawn.transform.rotation);
        player.GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Activators.etherealDeathIndex = true;
        Activators.cameraDeathIndex = true;
        Activators.playerDeathIndex = true;
    }
}