using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

public class EtherealWallBehaviour : EtherealSphereBehaviour
{
    public GameObject[] etherealWall;
    public Material _default;
    public Material _transparent;

    float[] longitude;
    float activedistance = 7f;

    private void Start()
    {
        longitude = new float[sphere.Length];
    }
    private void Update()
    {
        
        for (int a = 0; a < etherealWall.Length; a++)
        {
            for (int i = 0; i < sphere.Length; i++)
            {
                if (Activators.isShpereActive[i])
                {
                    longitude[i] = (sphere[i].transform.position - etherealWall[a].transform.position).magnitude;
                    if (longitude[i] < activedistance)
                    {
                        etherealWall[a].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[a].GetComponent<MeshRenderer>().material = _transparent;
                    }
                    else
                    {
                        etherealWall[a].GetComponent<BoxCollider>().enabled = true;
                        etherealWall[a].GetComponent<MeshRenderer>().material = _default;
                    }
                }
                if (Activators.isPlayerHasEtherealSphere)
                {
                    float distance = (player.transform.position - etherealWall[a].transform.position).magnitude;
                    if (distance < activedistance)
                    {
                        etherealWall[a].GetComponent<BoxCollider>().enabled = false;
                        etherealWall[a].GetComponent<MeshRenderer>().material = _transparent;
                    }
                    else
                    {
                        etherealWall[a].GetComponent<BoxCollider>().enabled = true;
                        etherealWall[a].GetComponent<MeshRenderer>().material = _default;
                    }
                }
            }
        }

    }
}
