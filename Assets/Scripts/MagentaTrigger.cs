using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagentaTrigger : MonoBehaviour
{
    public GameObject[] Cubes;
    public Vector3[] changed;
    public Vector3[] _default;
    private void Start()
    {
        if (!Activators.isMagentaDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
            Activators.isMagentaDefault = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Activators.isMagentaDefault)
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(changed[i].x, changed[i].y, changed[i].z);
            }
            Activators.isMagentaDefault = false;
        }
        else
        {
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = new Vector3(_default[i].x, _default[i].y, _default[i].z);
            }
            Activators.isMagentaDefault = true;
        }

    }
}
