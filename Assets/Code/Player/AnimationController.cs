using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private int _currentState;
    private float _lockedTill;
    private PlayerMotor _playerMotor;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody2D;
    
    [Header("States")]
    int Idle = Animator.StringToHash ("Idle");
    int Walk = Animator.StringToHash ("Walk");
    int Run = Animator.StringToHash ("Run");
    int Sprint = Animator.StringToHash ("Spring");
    int Crouch = Animator.StringToHash ("Crouch");
    int Hit = Animator.StringToHash ("Hit");
    int Dash = Animator.StringToHash ("Dash");
    int Death = Animator.StringToHash ("Death");
    int Block = Animator.StringToHash ("Block");
    int Jump = Animator.StringToHash ("Jump");
    int Fall = Animator.StringToHash ("Fall");
    int FallAttack = Animator.StringToHash ("FallAttack");
    int Attack = Animator.StringToHash ("Attack");
    int Attack2 = Animator.StringToHash ("Attack2");
    int Attack3 = Animator.StringToHash ("Attack3");
    int WallSlide = Animator.StringToHash ("WallSlide");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMotor = GetComponent<PlayerMotor>();
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        var state = GetState();
        if (state == _currentState) return;
        _animator.CrossFade(state,0,0);
        _currentState = state;
    }
    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;
        if (_playerMotor.isWallSliding) return WallSlide;
        if (_playerMotor.isDashing) return Dash;
        if (_playerMotor.Grounded) return _playerMotor.horizontalDirection == 0 ? Idle : Run;
        return _rigidbody2D.velocity.y > 0 ? Jump : Fall;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
}
