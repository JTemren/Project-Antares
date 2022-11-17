using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    [Header("Components")] private Animator _animator;
    private AnimationEventHandler _animationEventHandler;

    [Header("Attack")] [SerializeField] private int currentAttackCounter;
    [SerializeField] private int numberOfAttacks;

    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isAnimationFinished = false;

    private int CurrentAttackCounter
    {
        get => currentAttackCounter;
        set => currentAttackCounter = value >= numberOfAttacks ? 0 : value;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.Log("Animator is Null");
        _animationEventHandler = GetComponent<AnimationEventHandler>();
        if (_animationEventHandler == null) Debug.Log("Animation Event Handler is Null");
    }

    private void Update()
    {
        switch (isAttacking)
        {
            case true:
                if (CurrentAttackCounter == 0)
                {
                    _animationEventHandler.ChangeAnimationState(AnimationState.Attack1);
                }
                break;
            case false when isAnimationFinished:
                _animationEventHandler.ChangeAnimationState(AnimationState.Idle);
                break;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log(context.phase);
            isAttacking = !isAnimationFinished;
        }

        if (context.canceled)
        {
            isAttacking = !isAnimationFinished;

        }
    }
}
