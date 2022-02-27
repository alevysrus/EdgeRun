using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EtherealBehaviour : MonoBehaviour
{
    public GameObject[] sphere;
    public CharacterController player;
    bool isPlayerHasEtherealSphere;
    bool[] isShpereActive;

    public GameObject[] etherealWall;
    public Material _default;
    public Material _transparent;

    float[] distansceBetweenPlayerAndSphere;

    public float activedistance;
    float distanceToPlayerFromWall;
    float distanceToSphereFromWall;

    private void Start()
    {
        isPlayerHasEtherealSphere = false;
        isShpereActive = new bool[sphere.Length];
        for (int i = 0; i < isShpereActive.Length; i++)
        {
            isShpereActive[i] = true;
        }
        distansceBetweenPlayerAndSphere = new float[sphere.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPlayerHasEtherealSphere)
            {
                for (int i = 0; i < sphere.Length; i++)
                {
                    var longitude = (player.transform.position - sphere[i].transform.position).magnitude;
                    distansceBetweenPlayerAndSphere[i] = longitude;
                }

                int indexOfMiValue = Array.IndexOf(distansceBetweenPlayerAndSphere, distansceBetweenPlayerAndSphere.Min());

                if (isShpereActive[indexOfMiValue] && distansceBetweenPlayerAndSphere.Min() < 3f)
                {
                    isShpereActive[indexOfMiValue] = false;
                    sphere[indexOfMiValue].SetActive(false);
                    isPlayerHasEtherealSphere = true;
                }
            }
            else
            {
                int indexOfCurrentSphere = Array.IndexOf(isShpereActive, false);
                isShpereActive[indexOfCurrentSphere] = true;
                sphere[indexOfCurrentSphere].transform.position = player.transform.position;
                sphere[indexOfCurrentSphere].SetActive(true);
                isPlayerHasEtherealSphere = false;
            }
        }
        for (int j = 0; j < etherealWall.Length; j++)
        {
            for (int i = 0; i < sphere.Length; i++)
            {
                distanceToSphereFromWall = (sphere[i].transform.position - etherealWall[j].transform.position).magnitude;
                distanceToPlayerFromWall = (player.transform.position - etherealWall[j].transform.position).magnitude;
                switch (isPlayerHasEtherealSphere, distanceToPlayerFromWall < activedistance, isShpereActive[i], distanceToSphereFromWall < activedistance)
                {
                    case (true, true, false, false):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (true, true, true, false):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (true, true, false, true):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (true, false, true, true):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (false, true, true, true):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (false, false, true, true):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    case (true, true, true, true):
                        etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                        break;
                    default:
                        etherealWall[j].GetComponent<BoxCollider>().enabled = true;
                        etherealWall[j].GetComponent<MeshRenderer>().material = _default;
                        break;
                }
            }
        }
    }       
}
