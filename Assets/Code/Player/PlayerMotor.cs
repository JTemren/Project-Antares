using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    
    public PlayerInput.PlayerOnfootActions OnFoot;
    
    [Header("Components")] 
    [SerializeField]
    public new Rigidbody2D rigidbody2D;
    public float gravity = -9.81f;
    public LayerMask groundLayers;

    [Header("Movement")] 
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float acceleration = 5f;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float hangTime = .2f;
    [SerializeField] private float jumpBuffer = .5f;
    [SerializeField] private BoxCollider2D boxCollider2D;
    private float _hangTimeCounter;
    private float _jumpBufferCounter;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Grounded())
            _hangTimeCounter = hangTime;
        else
            _hangTimeCounter -= Time.deltaTime;
        if (!Grounded())
            _jumpBufferCounter -= Time.deltaTime;
        else
            _jumpBufferCounter = jumpBuffer;
    }

    // receive the inputs from InputManager.cs
    public void ProcessMove(Vector2 input)
    {
        var speedDif = movementSpeed - rigidbody2D.velocity.x;
        var movement = speedDif * acceleration;

        rigidbody2D.velocity = new Vector2(input.x * movement, rigidbody2D.velocity.y);

        // rigidbody2D.velocity = new Vector2(input.x * movementSpeed, rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (!(_hangTimeCounter > 0f) || !(_jumpBufferCounter > 0f)) return;
        _hangTimeCounter = 0f;
        _jumpBufferCounter = 0f;
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        
        Debug.Log("is not Grounded");
    }

    private bool Grounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f,
            groundLayers);
    }
}