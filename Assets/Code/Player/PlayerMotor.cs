using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Vector3 playerVol;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity = -9.8f;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
    }

    // receive the inputs from InputManager.cs
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.y = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * (movementSpeed * Time.deltaTime));
        playerVol.y += gravity * Time.deltaTime;
        if (isGrounded && playerVol.y < 0)
            playerVol.y = -2;
        controller.Move(playerVol * Time.deltaTime);
        Debug.Log(playerVol.y);

    }
}
