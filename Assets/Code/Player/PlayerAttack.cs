using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    private string _currentState;

    private PlayerMotor _playerMotor;
    [SerializeField] private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    { 
        animator = GetComponent<Animator>();
        _playerMotor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (isAttacking)
        {
            default:
                ChangeAnimationState("P_Idle");
                break;
            case true:
                ChangeAnimationState("P_Slash");
                break;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack " + context.phase);
        if (context.started)
        {
            isAttacking = !isAttacking;
        }
        else if (context.canceled)
        {
            isAttacking = !isAttacking;

        }
    }

    private void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        animator.Play(newState);
        _currentState = newState;
    }

    private bool IsAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return true;
        }
        return false;
    }
    
}
