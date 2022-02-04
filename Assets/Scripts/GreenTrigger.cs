using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTrigger : MonoBehaviour
{
    public GameObject[] Cubes;
    public Vector3[] changed;
    public Vector3[] _default;
    private void Start()
    {
        if (!Activators.isGreenDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
            Activators.isGreenDefault = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Activators.isGreenDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(changed[i].x, changed[i].y, changed[i].z);
            }
            Activators.isGreenDefault = false;
        }
        else
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
            Activators.isGreenDefault = true;
        }

    }
}
