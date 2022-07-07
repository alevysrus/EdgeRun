using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTrigger : MonoBehaviour
{
    public GameObject[] Cubes;
    public Vector3[] changed;
    public Vector3[] _default;
    private void Start()
    {
        if (!Activators.isBlueDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
            Activators.isBlueDefault = true;
        }
    }
    private void Update()
    {
        if (!Activators.isBlueDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(changed[i].x, changed[i].y, changed[i].z);
            }
        }
        else
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Activators.isBlueDefault)
        {
            Activators.isBlueDefault = false;
        }
        else
        {
            Activators.isBlueDefault = true;
        }
        
    }
}