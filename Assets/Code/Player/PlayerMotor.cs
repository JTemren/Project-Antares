using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMotor : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask layerMask;

    [Header("Movement")] 
    [SerializeField] private float speed = 5f;
    public float horizontalDirection;
    private bool _isFacingRight = true;
    public bool Running => rigidbody2D.velocity.x != 0 && Grounded;
    public bool Jumping => rigidbody2D.velocity.y > 0f;
    public bool Grounded => Physics2D.OverlapCircle(groundCheck.position, .2f,layerMask);
    public bool WallHang => Physics2D.OverlapCircle(wallCheck.position, .2f,layerMask);
    
    [Header("Wall Slide")]
    private readonly float _wallSlideSpeed = 2f;
    public bool isWallSliding;


    [Header("Dash")]
    [SerializeField] private bool canDash = true;
    private readonly float _dashingPower = 24f;
    private readonly float _dashingTime = 0.2f;
    private readonly float _dashingCooldown = 1f;
    public bool isDashing;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hangTime = .2f;
    private float _hangTimeCounter;
    public bool Falling => rigidbody2D.velocity.y < 0 && !Grounded;

    private void Awake()
    {
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        switch (Grounded)
        {
            case true:
                _hangTimeCounter = hangTime;
                break;
            default:
                _hangTimeCounter -= Time.deltaTime;
                break;
        }
        switch (_isFacingRight)
        {
            case false when horizontalDirection > 0f:
            case true when horizontalDirection < 0f:
                Flip();
                break;
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            Debug.Log("dash");
            canDash = false;
            rigidbody2D.gravityScale = 0f;
            rigidbody2D.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
            return;
        }
        rigidbody2D.gravityScale = Falling ? 2f : 1;
        if (Falling)
        {
            rigidbody2D.gravityScale = 2f;
        }

        rigidbody2D.velocity = new Vector2(horizontalDirection * speed, rigidbody2D.velocity.y);
        WallSlide();
    }

    private void WallSlide()
    {
        if (WallHang && !Grounded && horizontalDirection != 0)
        {
            isWallSliding = true;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,
                Mathf.Clamp(rigidbody2D.velocity.y, -_wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalDirection = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed || context.started)
        {
            if (!(_hangTimeCounter > 0)) return;
            
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }

        if (context.canceled && rigidbody2D.velocity.y >0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            _hangTimeCounter = 0f;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            Debug.Log("dash");
            isDashing = true;
            StartCoroutine(DashCooldown());
        }
    }
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashingTime);
        rigidbody2D.gravityScale = 1f;
        isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        canDash = true;
    }
    private void Flip()
    {
        if ((!_isFacingRight || !(horizontalDirection < 0f)) &&
            (_isFacingRight || !(horizontalDirection > 0f))) return;
        Vector3 localScale = transform.localScale;
        _isFacingRight = !_isFacingRight;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        switch (Grounded) 
        {
            case true:
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(groundCheck.position,.2f);
                break;
            case false:
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position,.2f);
                break;
        }
        switch (WallHang) 
        {
            case true:
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(wallCheck.position,.2f);
                break;
            case false:
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(wallCheck.position,.2f);
                break;
        }
        
    }
}
