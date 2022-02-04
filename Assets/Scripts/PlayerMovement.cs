using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    float speed = 12.8f;
    float gamevelocity;
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
    bool isGrounded;
    bool isLaddered;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundMask | ladderMask);
        isLaddered = Physics.CheckSphere(ladderCheck.position, 1f, ladderMask);

        if ((isGrounded | isLaddered) && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && (isGrounded | isLaddered))
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

        switch ((isLaddered, isGrounded))
        {
            case (true, true):
                if (Input.GetAxis("Horizontal") != 0 & Input.GetAxis("Vertical") != 0)
                {
                    gamevelocity = speed / Mathf.Sqrt(2);
                }
                else
                {
                    gamevelocity = speed;
                }
                controller.Move(gamevelocity * Time.unscaledDeltaTime * moving);
                break;
            case (true, false):
                gamevelocity = speed / 2;
                controller.Move(gamevelocity * Time.unscaledDeltaTime * laddermoving);
                break;
            case (false, true):
                if (Input.GetAxis("Horizontal") != 0 & Input.GetAxis("Vertical") != 0)
                {
                    gamevelocity = speed / Mathf.Sqrt(2);
                }
                else
                {
                    gamevelocity = speed;
                }
                controller.Move(gamevelocity * Time.unscaledDeltaTime * moving);
                break;
            case (false, false):
                if (Input.GetAxis("Horizontal") != 0 & Input.GetAxis("Vertical") != 0)
                {
                    gamevelocity = speed / Mathf.Sqrt(2);
                }
                else
                {
                    gamevelocity = speed;
                }
                controller.Move(gamevelocity * Time.unscaledDeltaTime * moving);
                break;

        }
        velocity.y += gravity * Time.unscaledDeltaTime;
        controller.Move(velocity * Time.unscaledDeltaTime);
    }
}
