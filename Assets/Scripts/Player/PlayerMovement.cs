using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveAction;
    [SerializeField] InputActionReference _dashAction;
    [SerializeField] Animator _anim;
    [SerializeField, Range(0f,10f)] float _moveSpeed;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;

    void Start()
    {
        _moveAction.action.started += MoveStarted;
        _moveAction.action.performed += MovePerformed;
        _moveAction.action.canceled += MoveCanceled;

        _rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        _moveAction.action.started -= MoveStarted;
        _moveAction.action.performed -= MovePerformed;
        _moveAction.action.canceled -= MoveCanceled;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + (_moveDir * _moveSpeed * Time.fixedDeltaTime));
    }

    void MoveStarted(InputAction.CallbackContext cc)
    {
        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetFloat("DirectionX", _moveDir.x);
        _anim.SetFloat("DirectionY", _moveDir.y);
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        PlayAnimState("IsRunning");

    }

    void MovePerformed(InputAction.CallbackContext cc)
    {
        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetFloat("DirectionX", _moveDir.x);
        _anim.SetFloat("DirectionY", _moveDir.y);
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        PlayAnimState("IsRunning");
    }

    void MoveCanceled(InputAction.CallbackContext cc)
    {
        _moveDir = Vector2.zero;
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        PlayAnimState("IsIdle");
    }

    void PlayAnimState(string anim)
    {
        _anim.SetBool("IsRunning", anim == "IsRunning" ? true : false);
        _anim.SetBool("IsSprinting", anim == "IsSprinting" ? true : false);
        _anim.SetBool("IsRolling", anim == "IsRolling" ? true : false);
        _anim.SetBool("IsIdle", anim == "IsIdle" ? true : false);
    }
}
