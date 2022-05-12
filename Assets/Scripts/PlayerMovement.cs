using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CustomController controller;
    float speed = 15f;
    float gamevelocity = 15f;
    float gravity = -90f;
    float jumpHeihgt = 3.5f;
    public LayerMask groundMask;
    public LayerMask ladderMask;
    public Transform groundCheck;
    public Transform ladderCheck;

    float x;
    float z;
    float xChanched = 0;
    float zChanched = 0;

    Vector3 velocity;
    Vector3 gravityDirection;
    bool isLaddered;

    int indexOfDelayForLadder = 0;
    bool laddercheckcondition;

    private float y;
    private bool isGod;

    Vector3 laddermoving;
    Vector3 moving;

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
        if (Activators.playerDeathIndex)
        {
            velocity.x = 0;
            velocity.y = 0;
            velocity.z = 0;
            Activators.playerDeathIndex = false;
        }
    }

    public void PlayerMovingInput()
    {
        laddercheckcondition = Physics.CheckSphere(ladderCheck.position, 0.5f, ladderMask);
        Activators.isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

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

        PlayerGravityDirection();

        if (Activators.isGrounded | isLaddered)
        {
            velocity = gravityDirection * -2f;
        }

        if (Input.GetButton("Jump") && Activators.isGrounded)
        {
            velocity = gravityDirection * Mathf.Sqrt(jumpHeihgt * -2f * gravity);
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

    }
    public void PlayerGravityDirection()
    {
        switch (Activators.gravityIndex)
        {
            case 0:
                gravityDirection = transform.up;
                break;
            case 1:
                gravityDirection = -transform.right;
                break;
            case 2:
                gravityDirection = transform.forward;
                break;
            case 3:
                gravityDirection = transform.right;
                break;
            case 4:
                gravityDirection = -transform.forward;
                break;
            case 5:
                gravityDirection = -transform.up;
                break;
        }
    }
    public void PlayerMovingDirection()
    {
        switch (Activators.gravityIndex)
        {
            case 0:
                laddermoving = (transform.right * x + transform.up * z);
                moving = (transform.right * x + transform.forward * z);
                break;
            case 1:
                laddermoving = (transform.up * x + transform.right * -z);
                moving = (transform.up * x + transform.forward * z);
                break;
            case 2:
                laddermoving = (transform.right * -x + transform.forward * z);
                moving = (transform.right * -x + transform.up * -z);
                break;
            case 3:
                laddermoving = (transform.up * -x + transform.right * z);
                moving = (transform.up * -x + transform.forward * -z);
                break;
            case 4:
                laddermoving = (transform.right * x + transform.forward * -z);
                moving = (transform.right * x + transform.up * z);
                break;
            case 5:
                laddermoving = (transform.right * -x + transform.up * -z);
                moving = (transform.right * x + transform.forward * z);
                break;
        }
    }
    public void PlayerMoving()
    {
        PlayerMovingInput();
        PlayerMovingDirection();
        switch ((isLaddered, Activators.isGrounded))
        {
            case (true, true):
                controller.Move(gamevelocity * moving);
                break;
            case (true, false):
                controller.Move((gamevelocity/2) * laddermoving);
                break;
            case (false, true):
                controller.Move(gamevelocity * moving);
                break;
            case (false, false):
                controller.Move(gamevelocity * moving);
                break;
        }

        velocity = gravity * Time.deltaTime * gravityDirection + velocity;
        controller.Move(velocity);


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
        controller.Move(gamevelocity * moving);
    }
}