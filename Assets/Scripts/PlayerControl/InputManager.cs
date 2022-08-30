using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;

    public float rotateSpeed = 20f;

    //move
    public bool moveKeyDown = false;
    public Vector2 inputVector2;

    //dodge
    public bool dodgeKeyDown = false;
    public float dodgeKeyValue;

    public bool sprintStatus = false; //


    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        InputSystem.Instance.moveStart += MoveStart;
        InputSystem.Instance.movePerformed += Move;
        InputSystem.Instance.moveCanceled += MoveCanceled;

        InputSystem.Instance.dodgeStart += ctx =>
        {
            dodgeKeyDown = true;

            if (moveKeyDown)
                sprintStatus = true;
        };
        InputSystem.Instance.dodgePerformed += ctx =>
        {
            //Debug.Log(ctx.ReadValue<float>());
            dodgeKeyValue = ctx.ReadValue<float>();
        };
        InputSystem.Instance.dodgeCanceled += ctx => { dodgeKeyDown = false; };
    }


    #region Move

    void MoveStart(InputAction.CallbackContext ctx)
    {
        moveKeyDown = true;

        //playerController?.anim?.SetBool("Move", moveKeyDown);
    }

    void Move(InputAction.CallbackContext ctx)
    {
        if (!moveKeyDown)
            moveKeyDown = true;

        inputVector2 = ctx.ReadValue<Vector2>();

        // Debug.Log(inputVector2.normalized);
        // Debug.Log(inputVector2.sqrMagnitude);
    }

    void MoveCanceled(InputAction.CallbackContext ctx)
    {
        moveKeyDown = false;
        sprintStatus = false;
        playerController?.anim?.SetBool("Move", moveKeyDown);
    }

    #endregion


    void Update()
    {
        InputMove();
    }


    private float targetInputYValue = 0;
    private float inputValue = 0;

    void InputMove()
    {
        if (!moveKeyDown)
        {
            return;
        }

        //状态
        inputValue = inputVector2.sqrMagnitude; //0-1
        bool animMoving = playerController.anim.GetBool("Move");
        if (!animMoving && inputValue < 0.2f)
        {
            return;
        }
        
        print(inputValue);
        playerController?.anim?.SetBool("Move", true);


        targetInputYValue = 0;

        if (!sprintStatus)
        {
            if (inputValue < 0.9f)
            {
                targetInputYValue = 0;
            }
            else
            {
                targetInputYValue = 1;
            }
        }
        else
        {
            targetInputYValue = 2;
        }


        float InputY_Curr = playerController.anim.GetFloat("InputY");
        float tempY = Mathf.Lerp(InputY_Curr, targetInputYValue, 5 * Time.deltaTime);
        playerController?.anim?.SetFloat("InputY", tempY);


        //旋转
        Vector3 targetDir = GetTargetDir(inputVector2);
        transform.forward = Vector3.Lerp(transform.forward, targetDir, rotateSpeed * Time.deltaTime);
    }


    Vector3 GetTargetDir(Vector2 input)
    {
        // float h = inputVector2.x;
        // float v = inputVector2.y;
        Vector3 velocity = new Vector3(inputVector2.x, 0, inputVector2.y);
        float camY = Camera.main.transform.rotation.eulerAngles.y;
        return Quaternion.Euler(0, camY, 0) * velocity;
    }
}