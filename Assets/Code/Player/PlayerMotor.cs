using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider2D;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState;

    [Header("Movement")] 
    
    [SerializeField] private float speed = 10f;
    private Vector2 _vecMove;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private LayerMask layerMask;
    private const float HangTime = 0.2f;
    private float _hangTimeCounter;
    private const float JumpBufferTime = 0.2f;
    private float _jumpBufferCounter;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsGrounded())
        {
            _hangTimeCounter = HangTime;
        }
        else
        {
            _hangTimeCounter -= Time.deltaTime;
        }

        if (Keyboard.current.spaceKey.wasReleasedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            _hangTimeCounter = Time.deltaTime;
        }
        
        IsGrounded();
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(_vecMove.x * speed, rigidbody2D.velocity.y);
    }

    private void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if (context.started && _hangTimeCounter > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            _jumpBufferCounter = JumpBufferTime;
        }
        if (context.canceled && rigidbody2D.velocity.y > 0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
            _hangTimeCounter = 0f;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        _vecMove = context.ReadValue<Vector2>();
        Flip();
    }

    private bool IsGrounded()
    {
        var raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, layerMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    private void Flip()
    {
        switch (_vecMove.x)
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