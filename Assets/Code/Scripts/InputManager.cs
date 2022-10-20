using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    private PlayerInput.PlayerOnfootActions _onFoot;
    private PlayerInput _playerInput;

    private PlayerMotor _motor;
    // Start is called before the first frame update
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.PlayerOnfoot;
        _motor = GetComponent<PlayerMotor>();
    }

    private void FixedUpdate()
    {
        _motor.ProcessMove(_onFoot.Move.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void OnEnable()
    {
        _onFoot.Enable();
    }
}