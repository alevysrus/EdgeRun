using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnBehaviour : MonoBehaviour
{
    public CustomController player;
    public GameObject[] respawn;
    public GameObject[] sphere;

    private Vector3 playerpostiion;
    private Quaternion playerrotation;
    private Vector3[] SphereLocation;
    private bool BlueCondition;
    private bool GreenCondition;
    private bool OrangeCondition;
    private bool MagentaCondition;
    private int GravityCondition;

    private void Start()
    {
        SphereLocation = new Vector3[sphere.Length];
        Activators.CountOfRespawn = 0;
        Activators.IsSaved = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Respawn(respawn[Activators.CountOfRespawn]);
    }
    private void Update()
    {
        if (Activators.isEasy == 0 && Activators.isGrounded && Input.GetButton("Save"))
        {
            playerpostiion = player.transform.position;
            playerrotation = player.transform.rotation;

            BlueCondition = Activators.isBlueDefault;
            GreenCondition = Activators.isGreenDefault;
            OrangeCondition = Activators.isOrangeDefault;
            MagentaCondition = Activators.isMagentaDefault;
            GravityCondition = Activators.gravityIndex;
            for (int i = 0; i < sphere.Length; i++)
            {
                    SphereLocation[i] = sphere[i].transform.position;
            }
            Activators.IsSaved++;
        }
        if (Activators.IsLoaded && Activators.IsSaved > 0)
        {
            LoadSave(playerpostiion, playerrotation);
            Activators.IsLoaded = false;
        }
    }
    public void Respawn(GameObject respawn)
    {
        player.transform.SetPositionAndRotation(respawn.transform.position, respawn.transform.rotation);
        player.GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Activators.etherealDeathIndex = true;
        Activators.playerDeathIndex = true;
        Activators.gravityIndex = 0;
    }
    public void LoadSave(Vector3 pos, Quaternion rot)
    {
        player.transform.SetPositionAndRotation(pos, rot);
            Activators.isBlueDefault = BlueCondition;
            Activators.isGreenDefault = GreenCondition;
            Activators.isOrangeDefault = OrangeCondition;
            Activators.isMagentaDefault = MagentaCondition;
            Activators.gravityIndex = GravityCondition;
            for (int i = 0; i < sphere.Length; i++)
            {
                if (sphere[i].activeSelf)
                {
                    sphere[i].transform.position = SphereLocation[i];
                }
                else
                {
                    sphere[i].SetActive(true);
                    sphere[i].transform.position = SphereLocation[i];
                }
            }
    }
}