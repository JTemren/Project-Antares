using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    [Header("Components")] 
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private AnimationEventHandler _animationEventHandler;

    [Header("Movement")] 
    private Vector2 _moveInput;
    [SerializeField] private bool isFacingRight = true;
    private float _horizontal;
    [SerializeField] private float walkSpeed = 5f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null) Debug.Log("Rigidbody is Null");
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.Log("Animator is Null");
        _animationEventHandler = GetComponent<AnimationEventHandler>();
        if (_animationEventHandler == null) Debug.Log("Animation Handler is Null");
    }

    private void Update()
    {
        Debug.Log(_animationEventHandler.currentState);
        if ((!isFacingRight && _moveInput.x > 0f) || (isFacingRight && _moveInput.x < 0f)) Flip();

        switch (_moveInput.x != 0)
        {
            case true:
                _animationEventHandler.ChangeAnimationState(AnimationState.Run);
                break;
            case false:
                _animationEventHandler.ChangeAnimationState(AnimationState.Idle);
                break;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_moveInput.x * walkSpeed, _rigidbody2D.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        var localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    
}