using UnityEngine;

public class InputManager : MonoBehaviour
{
    public PlayerInput.PlayerOnfootActions OnFoot;
    private PlayerInput _playerInput;

    private PlayerMotor _motor;
    // Start is called before the first frame update
    private void Awake()
    {
        _playerInput = new PlayerInput();
        OnFoot = _playerInput.PlayerOnfoot;
        _motor = GetComponent<PlayerMotor>();
        OnFoot.Jump.started += ctx => _motor.Jump();
    }

    private void FixedUpdate()
    {
        _motor.ProcessMove(OnFoot.Move.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void OnEnable()
    {
        OnFoot.Enable();
    }

    private void OnDisable()
    {
        OnFoot.Disable();
    }
}