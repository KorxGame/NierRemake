using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.InputSystem;

public class InputSystem : MonoSingleton<InputSystem>
{
    private InputAct _inputAct;

    // public override void Init()
    // {
    //     base.Init();
    //     if (_inputAct == null)
    //     {
    //         _inputAct = new InputAct();
    //     }
    // }

    #region event

    public delegate void inputCallbackContext(InputAction.CallbackContext ctx);

    //move
    public inputCallbackContext moveStart;
    public inputCallbackContext movePerformed;
    public inputCallbackContext moveCanceled;

    //jump
    public inputCallbackContext jumpStart;
    public inputCallbackContext jumpCanceled;

    //Dodge
    public inputCallbackContext dodgeStart;
    public inputCallbackContext dodgePerformed;
    public inputCallbackContext dodgeCanceled;

    public delegate void action();

    #endregion


    void Start()
    {
        if (_inputAct == null)
        {
            _inputAct = new InputAct();
        }
        _inputAct.Enable();

        Inputevent();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _inputAct.Disable();
    }


    void Inputevent()
    {
        //move
        _inputAct.Player.move.started += ctx =>
        {
            moveStart?.Invoke(ctx);
        };
        _inputAct.Player.move.performed += ctx => { movePerformed?.Invoke(ctx); };
        _inputAct.Player.move.canceled += ctx =>
        {
            moveCanceled?.Invoke(ctx);
        };
        //jump
        _inputAct.Player.jump.started += ctx =>
        {
            jumpStart?.Invoke(ctx);
        };
        // _inputAct.Player.jump.performed += ctx =>
        // {
        //
        //
        // };
        _inputAct.Player.jump.canceled += ctx =>
        {
            jumpCanceled?.Invoke(ctx);
        };
        //dodge
        _inputAct.Player.DashDodge.started += ctx => { dodgeStart?.Invoke(ctx); };
        _inputAct.Player.DashDodge.performed += ctx => { dodgePerformed?.Invoke(ctx); };
        _inputAct.Player.DashDodge.canceled += ctx => { dodgeCanceled?.Invoke(ctx); };
        
    }
}