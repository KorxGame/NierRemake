using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorCommon : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _character;
    private PlayerController _playerController;

    private Animator anim;

    public float gravity = -9.8f;

    void Start()
    {
        _playerController = transform.GetComponentInParent<PlayerController>();
        _character = _playerController?.CC;
        if (transform.TryGetComponent(out anim) && _playerController)
        {
            _playerController.anim = anim;
        }

        anim.speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnAnimatorMove()
    {
        if (anim && _character)
        {
            Vector3 newPosition = anim.deltaPosition;
            Quaternion newRotation = anim.deltaRotation;
            _character.transform.eulerAngles += newRotation.eulerAngles;
            
            newPosition += Vector3.up * gravity * Time.deltaTime;

            _character.Move(newPosition);
        }
    }
}