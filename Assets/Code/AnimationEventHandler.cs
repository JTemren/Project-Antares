using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationEventHandler : MonoBehaviour
{
    private Animator _animator;
    public string currentState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.Log("Animator is Null");
    }

    public void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;
        _animator.Play(newState);
        currentState = newState;
    }
}
