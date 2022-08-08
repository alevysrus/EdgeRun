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

    //Vector3 gravCase;
    //float relMax;
    //float max;
    Vector3 gravVec;
    Vector3 remTransform;
    float xRot;
    float yRot;
    float zRot;
    private void Start()
    {
        isGod = false;
    }

    private void Update()
    {
        if (!isGod)
        {
            if (//Activators.level > 4 &&
                Input.GetKeyDown("r"))
            {
                remTransform = transform.position;
                gravVec = PlusRotation();
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x - 90, gravVec.y + 0, gravVec.z + 0), 1);
                transform.position = remTransform;


            }
            if (//Activators.level > 4 &&
                Input.GetKeyDown("t"))
            {
                remTransform = transform.position;
                gravVec = PlusRotation();
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 180, gravVec.y + 180, gravVec.z + 0), 1);
                transform.position = remTransform;

            }
            /*if (//Activators.level > 4 &&
                Input.GetButtonDown("Apply"))
            {
                GravityApply();
            }*/
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
    private void PlayerMovingInput()
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
   
        gravityDirection = transform.up;

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

    private void PlayerMoving()
    {
        PlayerMovingInput();
        laddermoving = (transform.right * x + transform.up * z);
        moving = (transform.right * x + transform.forward * z);
        
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
    private void GodMode()
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
    private void GravityApply()
    {
        remTransform = transform.position;
        switch(GameObject.Find("Canvas").GetComponent<Pause>().GravityIndex)
        {
            case 0:
                Activators.gravityIndex = 1;
                break;
            case 1:
                Activators.gravityIndex = 2;
                break;
            case 2:
                Activators.gravityIndex = 3;
                break;
            case 3:
                Activators.gravityIndex = 4;
                break;
            case 4:
                Activators.gravityIndex = 5;
                break;

        }
        gravVec = PlusRotation();
        switch (Activators.gravityIndex)
        {
            
            case 0:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 0, gravVec.y + 0, gravVec.z + 0), 1);
                break;
            case 1:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 0, gravVec.y + 0, gravVec.z + 90), 1);
                break;
            case 2:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 90, gravVec.y + 0, gravVec.z + 0), 1);
                break;
            case 3:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 0, gravVec.y + 0, gravVec.z - 90), 1);
                break;
            case 4:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x - 90, gravVec.y + 0, gravVec.z + 0), 1);
                break;
            case 5:
                controller.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(gravVec.x + 180, gravVec.y + 180, gravVec.z + 0), 1);
                break;
        }
        transform.position = remTransform;
    }

    /*private Vector3 GetGravity(Vector3 axis)
    {
        
        max = Mathf.Max(Mathf.Abs(axis.x), Mathf.Abs(axis.y), Mathf.Abs(axis.z));
        relMax = Mathf.Max(axis.x, axis.y, axis.z)
        if (max == Mathf.Abs(axis.x))
        {
            gravVec = new Vector3(1f, 0f, 0f);
        }
        else
        {
            if (max == Mathf.Abs(axis.y))
            {
                gravVec = new Vector3(0f, 1f, 0f);
            }
            else if (max == Mathf.Abs(axis.z))
            {
                gravVec = new Vector3(0f, 0f, 1f);
            }
        }
        if (relMax != max)
        {
            gravVec = -gravVec;
        }
        return gravVec;
    }*/
    private Vector3 PlusRotation()
    {
        Vector3 eulRot = Vector3.zero;
        xRot = controller.transform.eulerAngles.x % 360;
        yRot = controller.transform.eulerAngles.y % 360;
        zRot = controller.transform.eulerAngles.z % 360;
        if (xRot > 315 && xRot <= 45)
            eulRot.x = 0;
        if (xRot > 45 && xRot <= 135)
            eulRot.x = 90;
        if (xRot > 135 && xRot <= 225)
            eulRot.x = 180;
        if (xRot > 225 && xRot <= 315)
            eulRot.x = 270;
        if (yRot > 315 && yRot <= 45)
            eulRot.y = 0;
        if (yRot > 45 && yRot <= 135)
            eulRot.y = 90;
        if (yRot > 135 && yRot <= 225)
            eulRot.y = 180;
        if (yRot > 225 && yRot <= 315)
            eulRot.y = 270;
        if (zRot > 315 && zRot <= 45)
            eulRot.z = 0;
        if (zRot > 45 && zRot <= 135)
            eulRot.z = 90;
        if (zRot > 135 && zRot <= 225)
            eulRot.z = 180;
        if (zRot > 225 && zRot <= 315)
            eulRot.z = 270;
        return eulRot;
    }
    /*private void FuckYou()
    {
        xRot = controller.transform.rotation.x;
        yRot = controller.transform.rotation.y;
        zRot = controller.transform.rotation.z;
        switch(xRot,yRot, zRot)

    } */
}