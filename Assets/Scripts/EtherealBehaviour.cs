using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EtherealBehaviour : MonoBehaviour
{
    public GameObject[] sphere;
    public CharacterController player;
    bool[] isShpereActive;

    public GameObject[] etherealWall;
    public GameObject[] disabledWall;
    public Material _default;
    public Material _transparent;

    float[] distansceBetweenPlayerAndSphere;

    public float activedistance;
    float distanceToPlayerFromWall;
    float distanceToSphereFromWall;
    int[] indexOfSwithed;

    private void Start()
    {
        Activators.isPlayerHasEtherealSphere = false;
        isShpereActive = new bool[sphere.Length];
        for (int i = 0; i < isShpereActive.Length; i++)
        {
            isShpereActive[i] = true;
        }
        distansceBetweenPlayerAndSphere = new float[sphere.Length];
        indexOfSwithed = new int[etherealWall.Length];
        for (int i = 0; i < indexOfSwithed.Length; i++)
        {
            indexOfSwithed[i] = 0;
        }
        Activators.executionIndex = 0;
        Activators.deathIndex = false;
        for (int i = 0; i < disabledWall.Length; i++)
        {
            disabledWall[i].GetComponent<BoxCollider>().enabled = true;
            disabledWall[i].GetComponent<MeshRenderer>().material = _default;
        }
    }

    private void Update()
    {
        Activators.executionIndex++;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Activators.isPlayerHasEtherealSphere)
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
                    Activators.isPlayerHasEtherealSphere = true;
                }
            }
            else
            {
                int indexOfCurrentSphere = Array.IndexOf(isShpereActive, false);
                isShpereActive[indexOfCurrentSphere] = true;
                sphere[indexOfCurrentSphere].transform.position = player.transform.position;
                sphere[indexOfCurrentSphere].SetActive(true);
                Activators.isPlayerHasEtherealSphere = false;
            }
        }

        if (Activators.deathIndex)
        {
            Activators.deathIndex = false;
            if (Activators.isPlayerHasEtherealSphere)
            {
                Activators.isPlayerHasEtherealSphere = false;
                int _indexOfCurrentSphere = Array.IndexOf(isShpereActive, false);
                isShpereActive[_indexOfCurrentSphere] = true;
                sphere[_indexOfCurrentSphere].SetActive(true);
            }
            
        }

        for (int i = 0; i < sphere.Length; i++)
        {
            
            for (int j = 0; j < etherealWall.Length; j++)
            {
                distanceToSphereFromWall = (sphere[i].transform.position - etherealWall[j].transform.position).magnitude;

                if (isShpereActive[i] && distanceToSphereFromWall < activedistance)
                {
                    indexOfSwithed[j] = Activators.executionIndex;
                    etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                    etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
                else if (!(indexOfSwithed[j] == Activators.executionIndex))
                {
                    etherealWall[j].GetComponent<BoxCollider>().enabled = true;
                    etherealWall[j].GetComponent<MeshRenderer>().material = _default;
                }
                distanceToPlayerFromWall = (player.transform.position - etherealWall[j].transform.position).magnitude;
                if (Activators.isPlayerHasEtherealSphere && distanceToPlayerFromWall < activedistance)
                {
                    etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                    etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
                else if (!(indexOfSwithed[j] == Activators.executionIndex))
                {
                    etherealWall[j].GetComponent<BoxCollider>().enabled = true;
                    etherealWall[j].GetComponent<MeshRenderer>().material = _default;
                }
            }

            for (int j = 0; j < disabledWall.Length; j++)
            {
                distanceToSphereFromWall = (sphere[i].transform.position - disabledWall[j].transform.position).magnitude;

                if (isShpereActive[i] && distanceToSphereFromWall < activedistance)
                {
                    indexOfSwithed[j] = Activators.executionIndex;
                    disabledWall[j].GetComponent<BoxCollider>().enabled = true;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _default;
                }
                else if (!(indexOfSwithed[j] == Activators.executionIndex))
                {
                    disabledWall[j].GetComponent<BoxCollider>().enabled = false;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
                distanceToPlayerFromWall = (player.transform.position - disabledWall[j].transform.position).magnitude;
                if (Activators.isPlayerHasEtherealSphere && distanceToPlayerFromWall < activedistance)
                {
                    disabledWall[j].GetComponent<BoxCollider>().enabled = true;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _default;
                }
                else if (!(indexOfSwithed[j] == Activators.executionIndex))
                {
                    disabledWall[j].GetComponent<BoxCollider>().enabled = false;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
            }
        }
    }
}
