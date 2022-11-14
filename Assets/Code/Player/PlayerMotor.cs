using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private Transform groundCheck;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private string _currentState;

    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float horizontal; 

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float hangTimeCounter;
    [SerializeField] private float bufferTimeCounter;
    private const float HangTime = .2f;
    private const float BufferTime = .2f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch (IsGrounded())
        {
            case false:
            {
                hangTimeCounter -= Time.deltaTime;
                bufferTimeCounter -= Time.deltaTime;
                if (rigidbody2D.velocity.y > 0) ChangeAnimationState("P_Jump");
                if (rigidbody2D.velocity.y < 0) ChangeAnimationState("P_Fall");
                break;
            }
            case true:
            {
                hangTimeCounter = HangTime;
                bufferTimeCounter = BufferTime;
                ChangeAnimationState(horizontal != 0 ? "P_Run" : "P_Idle");
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontal * speed, rigidbody2D.velocity.y);
    }

    private void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        animator.Play(newState);
        _currentState = newState;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump " + context.phase);
        switch (bufferTimeCounter > 0f)
        {
            case true when hangTimeCounter > 0:
                bufferTimeCounter = 0f;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
                break;
        }

        switch (context.canceled)
        {
            case true when rigidbody2D.velocity.y > 0f:
                hangTimeCounter = 0f;
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * .5f);
                break;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move " + context.phase);               
        horizontal = context.ReadValue<Vector2>().x;
        Flip();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, .2f, layerMask);
    }
    private void Flip()
    {
        switch (horizontal)
        {
            case < 0f:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case > 0f:
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
    }
}