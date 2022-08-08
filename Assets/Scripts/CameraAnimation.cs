using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public Animator CameraMoving;
    public string Forward;
    public string Back;

    public void MoveForward()
    {
        CameraMoving = GetComponent<Animator>();
        CameraMoving.Play(Forward);
    }
    public void MoveBack()
    {
        CameraMoving = GetComponent<Animator>();
        CameraMoving.Play(Back);
    }


}
