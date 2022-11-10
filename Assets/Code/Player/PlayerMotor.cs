using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] public new Rigidbody2D rigidbody2D;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float jumpForce = 10f;
    public bool isGrounded;
    public float gravity = -9.81f;
    public LayerMask groundLayers;
    

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f), new Vector2(transform.position.x+0.5f,transform.position.y -0.51f), groundLayers);
    }

    // receive the inputs from InputManager.cs
    public void ProcessMove(Vector2 input)
    {
        rigidbody2D.velocity = new Vector2(input.x *movementSpeed,rigidbody2D.velocity.y);
    }

    public void Jump()
    {
        if (!isGrounded) return;
        rigidbody2D.velocity = jumpHeight * (Vector2.up * jumpForce);
    }
}
