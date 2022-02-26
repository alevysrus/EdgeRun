using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EtherealSphereBehaviour : MonoBehaviour
{
    public GameObject[] sphere;
    public CharacterController player;

    float[] distansce;

    private void Start()
    {
        Activators.isShpereActive = new bool[sphere.Length];
        distansce = new float[sphere.Length];
        for (int i = 0; i < Activators.isShpereActive.Length; i++)
        {
            Activators.isShpereActive[i] = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Activators.isPlayerHasEtherealSphere)
            {
                for (int i = 0; i < sphere.Length; i++)
                {
                    var longitude = (player.transform.position - sphere[i].transform.position).magnitude;
                    distansce[i] = Mathf.Abs(longitude);
                }

                int indexOfMiValue = Array.IndexOf(distansce, distansce.Min());

                if (Activators.isShpereActive[indexOfMiValue] && distansce.Min() < 3f)
                {
                    Activators.isShpereActive[indexOfMiValue] = false;
                    sphere[indexOfMiValue].SetActive(false);
                    Activators.isPlayerHasEtherealSphere = true;
                }
            }
            else
            {
                int indexOfCurrentSphere = Array.IndexOf(Activators.isShpereActive, false);
                Activators.isShpereActive[indexOfCurrentSphere] = true;
                sphere[indexOfCurrentSphere].transform.position = player.transform.position;
                sphere[indexOfCurrentSphere].SetActive(true);
                Activators.isPlayerHasEtherealSphere = false;
            } 
        }
    }
}
