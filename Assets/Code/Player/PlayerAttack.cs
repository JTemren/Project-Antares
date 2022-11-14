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

    [Header("Attacks")] 
    [SerializeField] private int numberOfAttacks;
    [SerializeField] private int currentAttackCounter;
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= numberOfAttacks ? 0 : value; 
    }

    private PlayerMotor _playerMotor;
    [SerializeField] private bool isAttacking;
    // Start is called before the first frame update
    void Awake()
    { 
        animator = GetComponent<Animator>();
        _playerMotor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (isAttacking)
        {
            case true:
                switch (CurrentAttackCounter)
                {
                    case 0:
                        ChangeAnimationState("P_Slash");
                        break;
                    case 1:
                        ChangeAnimationState("P_Slash2");
                        break;
                    case 2:
                        ChangeAnimationState("P_Slam");
                        break;
                    case 3:
                        ChangeAnimationState("P_Spin");
                        break;
                }
                break;
            case false:
                ChangeAnimationState("P_Idle");
                break;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAttacking = true;
            CurrentAttackCounter++;
        }

        if (context.canceled)
        {
            isAttacking = false;
        }
        Debug.Log("Attack " + context.phase);
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
