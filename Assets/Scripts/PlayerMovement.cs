using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    float speed = 12.8f;
    float gamevelocity = 12.8f;
    float gravity = -60f;
    float jumpHeihgt = 2.9f;
    public LayerMask groundMask;
    public LayerMask ladderMask;
    public Transform groundCheck;
    public Transform ladderCheck;

    float x;
    float z;
    float xChanched = 0;
    float zChanched = 0;

    Vector3 velocity;
    bool isLaddered;

    int indexOfDelay = 0;
    bool laddercheckcondition;

    //float y;

    private void Update()
    {
       laddercheckcondition = Physics.CheckSphere(ladderCheck.position, 1f, ladderMask);
        Activators.isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (isLaddered && !laddercheckcondition)
        {
            indexOfDelay++;
        }

        switch (indexOfDelay, laddercheckcondition)
        {
            case (0 , true):
                isLaddered = true;
                break;
            case (1 | 2 | 3 | 4 | 5  , false | true):
                isLaddered = true;
                Activators.isGrounded = true;
                break;
            case(6 , false):
                isLaddered = false;
                indexOfDelay = 0;
                break;
            default:
                break;
        }

        if ((Activators.isGrounded | isLaddered) && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButton("Jump") && (Activators.isGrounded | isLaddered))
        {
            velocity.y = Mathf.Sqrt(jumpHeihgt * -2f * gravity);
        }
        else
        {
            velocity.z = 0f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (x == xChanched & Mathf.Abs(x) != 1 & x != 0)
        {
            x = 0;
        }
        else
        {
            if (Mathf.Abs(x) == 1)
            {
                x = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                x = Input.GetAxis("Horizontal");
                xChanched = x;
            }

        }
        if (z == zChanched & Mathf.Abs(z) != 1 & z != 0)
        {
            z = 0;
        }
        else
        {
            if (Mathf.Abs(z) == 1)
            {
                z = Input.GetAxisRaw("Vertical");
            }
            else
            {
                z = Input.GetAxis("Vertical");
                zChanched = z;
            }
        }

        Vector3 laddermoving = (transform.right * x + transform.up * z);
        Vector3 moving = (transform.right * x + transform.forward * z);

        switch ((isLaddered, Activators.isGrounded))
        {
            case (true, true):
                controller.Move(gamevelocity * Time.deltaTime * moving);
                break;
            case (true, false):
                controller.Move(gamevelocity * Time.deltaTime * laddermoving);
                break;
            case (false, true):
                controller.Move(gamevelocity * Time.deltaTime * moving);
                break;
            case (false, false):
                controller.Move(gamevelocity * Time.deltaTime * moving);
                break;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetAxis("Horizontal") != 0 & Input.GetAxis("Vertical") != 0)
        {
            gamevelocity = speed / Mathf.Sqrt(2);
        }
        else
        {
            gamevelocity = speed;
        }

    }
    /*
     x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        switch (Input.GetKey(KeyCode.LeftShift), Input.GetKey(KeyCode.LeftControl))
        {
            case (true, false):
                y = 1;
                break;
            case (false, true):
                y = -1;
                break;
            default:
                y = 0;
                break;
        }
        if (x == xChanched & Mathf.Abs(x) != 1 & x != 0)
        {
            x = 0;
        }
        else
        {
            if (Mathf.Abs(x) == 1)
            {
                x = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                x = Input.GetAxis("Horizontal");
                xChanched = x;
            }

        }
        if (z == zChanched & Mathf.Abs(z) != 1 & z != 0)
        {
            z = 0;
        }
        else
        {
            if (Mathf.Abs(z) == 1)
            {
                z = Input.GetAxisRaw("Vertical");
            }
            else
            {
                z = Input.GetAxis("Vertical");
                zChanched = z;
            }
        }
        Vector3 moving = (transform.right * x + transform.forward * z + transform.up * y);
        controller.Move(gamevelocity * Time.deltaTime * moving);
    */
}