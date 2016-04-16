﻿using UnityEngine;
using System.Collections;

public class PlayerHandler : MonoBehaviour
{
    public float moveSpeed = 4;
    CharacterController _characterController;
    Animator _animator;
    Transform _waistTr;
    Vector3 _overrideRotation;
    Vector3 _velocity;
    bool _jumping = false;
    const float _kJumpPower = 6;

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _waistTr = _animator.GetBoneTransform(HumanBodyBones.Chest);
        _overrideRotation = _waistTr.localEulerAngles;
    }

    void Start()
    {
        InputHandler.instance.onMove = v =>
        {
            v = Quaternion.Euler(0, 0, -transform.eulerAngles.y) * v;
            _velocity.x += v.x / 2;
            _velocity.z += v.y / 2;

            Vector3 xz = _velocity;
            xz.y = 0;
            xz = xz.magnitude > moveSpeed ? xz.normalized * moveSpeed : xz;
            _velocity.x = xz.x;
            _velocity.z = xz.z;
        };
        InputHandler.instance.onStop = () =>
        {
            _velocity.x /= 1.5f;
            _velocity.z /= 1.5f;
        };
        InputHandler.instance.onJump = () =>
        {
            if (_jumping == false)
            {
                _velocity.y = _kJumpPower;
                _jumping = true;
            }
        };
        InputHandler.instance.onAimMove = v =>
        {
            if (90 < _waistTr.localEulerAngles.z + v.y && _waistTr.localEulerAngles.z + v.y < 270)
                v.y = 0;
            _overrideRotation += new Vector3(0, 0, v.y);
            transform.localEulerAngles += new Vector3(0, v.x, 0);
        };
    }

    void Update()
    {
        if (_characterController.isGrounded && _jumping&& _velocity.y != _kJumpPower)
            _jumping = false;

        _velocity += Physics.gravity * Time.deltaTime;

        if (_characterController.isGrounded&& _jumping == false)
            _velocity.y = 0.0f;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    void LateUpdate()
    {
        _waistTr.localEulerAngles = _overrideRotation;
    }
}
