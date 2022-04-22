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

    int indexOfDelayForLadder = 0;
    bool laddercheckcondition;

    private float y;
    private bool isGod;

    private void Start()
    {
        isGod = false;
    }

    private void Update()
    {
        if (!isGod)
        {
            PlayerMoving();
        }
        else
        {
            GodMode();
        }

        if (Input.GetKey(KeyCode.LeftBracket) && Input.GetKey(KeyCode.E) && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isGod)
            {
                isGod = true;
            }
            else
            {
                isGod = false;
            }
        }
    }

    public void PlayerMoving()
    {
        laddercheckcondition = Physics.CheckSphere(ladderCheck.position, 0.5f, ladderMask);
        Activators.isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask);

        if (isLaddered && !laddercheckcondition)
        {
            indexOfDelayForLadder++;
        }

        switch (indexOfDelayForLadder, laddercheckcondition)
        {
            case (0, true):
                isLaddered = true;
                break;
            case (1 | 2 | 3, false | true):
                isLaddered = true;
                Activators.isGrounded = true;
                break;
            case (4, false):
                isLaddered = false;
                indexOfDelayForLadder = 0;
                break;
            default:
                break;
        }

        if ((Activators.isGrounded | isLaddered) && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButton("Jump") && Activators.isGrounded)
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
    public void GodMode()
    {
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
    }
}