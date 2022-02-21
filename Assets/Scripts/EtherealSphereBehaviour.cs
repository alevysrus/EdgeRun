using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EtherealSphereBehaviour : MonoBehaviour
{
    public GameObject[] sphere;
    public LayerMask playerMask;
    public CharacterController player;

    bool[] isShpereActive;
    bool[] isNearPlayer;

    private void Start()
    {
        isShpereActive = new bool[sphere.Length];
        isNearPlayer = new bool[sphere.Length];
        for (int i = 0; i < isShpereActive.Length; i++)
        {
            isShpereActive[i] = true;
        }
    }

    private void Update()
    {
        byte countOfActive = 0;
        
        for (int i = 0; i < sphere.Length; i++)
        {
            isNearPlayer[i] = Physics.CheckSphere(sphere[i].transform.position, 3f, playerMask);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isShpereActive[i])
                {
                    if (isNearPlayer[i])
                    {
                        isShpereActive[i] = false;
                        sphere[i].SetActive(false);
                        break;
                    }
                }
                else
                {
                    isShpereActive[i] = true;
                    sphere[i].transform.position = player.transform.position;
                    sphere[i].SetActive(true);
                    break;
                }
            }
        }
        for (int i = 0; i < sphere.Length; i++)
        {
            if (!isShpereActive[i])
            {
                countOfActive++;
            }
            if (countOfActive > 0)
            {
                Activators.isEtherealActive = true;
                break;
            }
            else
            {
                Activators.isEtherealActive = false;
            }
        }
    }
}
