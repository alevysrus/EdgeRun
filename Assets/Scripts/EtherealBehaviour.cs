using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EtherealBehaviour : MonoBehaviour
{
    public GameObject[] sphere;
    public GameObject player;
    bool[] isShpereActive;

    public GameObject[] etherealWall;
    public GameObject[] disabledWall;
    public Material _default;
    public Material _transparent;

    float[] distansceBetweenPlayerAndSphere;

    public float activedistance;
    float distanceToPlayerFromWall;
    float distanceToSphereFromWall;
    int[] indexOfEnebledSwitched;
    int[] indexOfDisabledSwitched;

    private void Start()
    {
        Activators.isPlayerHasEtherealSphere = false;
        isShpereActive = new bool[sphere.Length];
        for (int i = 0; i < isShpereActive.Length; i++)
        {
            isShpereActive[i] = true;
        }
        distansceBetweenPlayerAndSphere = new float[sphere.Length];
        indexOfEnebledSwitched = new int[etherealWall.Length];
        indexOfDisabledSwitched = new int[disabledWall.Length];
        for (int i = 0; i < indexOfEnebledSwitched.Length; i++)
        {
            indexOfEnebledSwitched[i] = 0;
        }
        for (int i = 0; i < indexOfDisabledSwitched.Length; i++)
        {
            indexOfDisabledSwitched[i] = 0;
        }
        Activators.executionIndex = 0;
        Activators.etherealDeathIndex = false;
        for (int i = 0; i < disabledWall.Length; i++)
        {
            disabledWall[i].GetComponent<BoxCollider>().enabled = false;
            disabledWall[i].GetComponent<MeshRenderer>().material = _transparent;
        }
    }

    private void Update()
    {
        Activators.executionIndex++;
        if (Input.GetKeyDown(KeyCode.E) && Activators.isGrounded)
        {
            if (!Activators.isPlayerHasEtherealSphere)
            {
                for (int i = 0; i < sphere.Length; i++)
                {
                    var longitude = (player.transform.position - sphere[i].transform.position).magnitude;
                    distansceBetweenPlayerAndSphere[i] = longitude;
                }

                int indexOfMiValue = Array.IndexOf(distansceBetweenPlayerAndSphere, distansceBetweenPlayerAndSphere.Min());

                if (isShpereActive[indexOfMiValue] && distansceBetweenPlayerAndSphere.Min() < 2f)
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

        if (Activators.etherealDeathIndex)
        {
            Activators.etherealDeathIndex = false;
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
                    indexOfEnebledSwitched[j] = Activators.executionIndex;
                    etherealWall[j].GetComponent<BoxCollider>().enabled = false;
                    etherealWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
                else if (!(indexOfEnebledSwitched[j] == Activators.executionIndex))
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
                else if (!(indexOfEnebledSwitched[j] == Activators.executionIndex))
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
                    indexOfDisabledSwitched[j] = Activators.executionIndex;
                    disabledWall[j].GetComponent<BoxCollider>().enabled = true;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _default;
                }
                else if (!(indexOfDisabledSwitched[j] == Activators.executionIndex))
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
                else if (!(indexOfDisabledSwitched[j] == Activators.executionIndex))
                {
                    disabledWall[j].GetComponent<BoxCollider>().enabled = false;
                    disabledWall[j].GetComponent<MeshRenderer>().material = _transparent;
                }
            }
        }
    }
}
