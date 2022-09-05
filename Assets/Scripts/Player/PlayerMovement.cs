using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _runAction;
    [SerializeField] InputActionReference _sprintAction;
    [SerializeField] InputActionReference _rollingAction;
    [SerializeField] Animator _anim;
    [SerializeField, Range(0f,10f)] float _runSpeed;
    [SerializeField, Range(0f,10f)] float _sprintSpeed;
    [SerializeField] float _rollingCooldown;
    [SerializeField] AnimationCurve _rollingCurve;
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private float _moveSpeed;
    private bool _isSprinting;
    private bool _isRolling;

    private void Reset()
    {
        _moveSpeed = 0;
        _runSpeed = 4f;
        _sprintSpeed = 8f;
        _isSprinting = false;
        _isRolling = false;
        _rollingCooldown = .3f;
    }

    void Start()
    {
        _runAction.action.started += RunStarted;
        _runAction.action.performed += RunPerformed;
        _runAction.action.canceled += RunCanceled;

        _sprintAction.action.started += SprintStarted;
        _sprintAction.action.performed += SprintPerformed;
        _sprintAction.action.canceled += SprintCanceled;

        _rollingAction.action.started += RollingStarted;

        _rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        _runAction.action.started -= RunStarted;
        _runAction.action.performed -= RunPerformed;
        _runAction.action.canceled -= RunCanceled;

        _sprintAction.action.started -= SprintStarted;
        _sprintAction.action.performed -= SprintPerformed;
        _sprintAction.action.canceled -= SprintCanceled;

        _rollingAction.action.started -= RollingStarted;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        _moveDir = GetMoveDir();
        _moveSpeed = GetMoveSpeed();
        _rb.MovePosition(_rb.position + (_moveDir * _moveSpeed * Time.fixedDeltaTime));
    }

    void RunStarted(InputAction.CallbackContext cc)
    {
        if (_isRolling) return;

        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetFloat("DirectionX", _moveDir.x);
        _anim.SetFloat("DirectionY", _moveDir.y);
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        _anim.SetBool("IsIdle", false);
        _anim.SetBool("IsRunning", true);
    }

    void RunPerformed(InputAction.CallbackContext cc)
    {
        if (_isRolling) return;

        _moveDir = cc.ReadValue<Vector2>();
        _anim.SetFloat("DirectionX", _moveDir.x);
        _anim.SetFloat("DirectionY", _moveDir.y);
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        _anim.SetBool("IsRunning", true);
    }

    void RunCanceled(InputAction.CallbackContext cc)
    {
        if (_isRolling) return;

        _moveDir = Vector2.zero;
        _anim.SetFloat("AnimationSpeed", _moveDir.magnitude);
        _anim.SetBool("IsRunning", false);
        _anim.SetBool("IsIdle", true);
    }

    void SprintStarted(InputAction.CallbackContext cc)
    {
        _isSprinting = true;
        _anim.SetBool("IsSprinting", true);
    }

    void SprintPerformed(InputAction.CallbackContext cc)
    {
        _isSprinting = true;
        _anim.SetBool("IsSprinting", true);
    }

    void SprintCanceled(InputAction.CallbackContext cc)
    {
        _isSprinting = false;
        _anim.SetBool("IsSprinting", false);
    }

    void RollingStarted(InputAction.CallbackContext cc)
    {
        _isRolling = true;
        _anim.SetBool("IsRolling", true);
        StartCoroutine(RollingCancel());
    }

    float GetMoveSpeed()
    {
        if (_isRolling)
        {
            return _moveSpeed;
        }
        else if (_isSprinting)
        {
            return _sprintSpeed;
        }
        else if (!_isSprinting)
        {
            return _runSpeed;
        }
            
        return 0;
    }

    Vector2 GetMoveDir()
    {
        if (_isRolling)
        {
            return _moveDir;
        }

        return _runAction.action.ReadValue<Vector2>().magnitude > 0.1f ? _runAction.action.ReadValue<Vector2>() : Vector2.zero;
    }

    IEnumerator RollingCancel()
    {
        float cd = 0;
        while(cd < _rollingCooldown)
        {
            _moveSpeed = _rollingCurve.Evaluate(cd / _rollingCooldown) * (_isSprinting ? _sprintSpeed : _runSpeed);
            cd += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        // roll end
        _isRolling = false;
        _anim.SetBool("IsRolling", false);

        // update
        _moveDir = GetMoveDir();
        _moveSpeed = GetMoveSpeed();
        _anim.SetBool("IsRunning", _moveDir.magnitude != 0 ? true : false);
        _anim.SetBool("IsIdle", _moveDir.magnitude == 0 ? true : false);
    }

    void ForceState(string anim)
    {
        _anim.SetBool("IsRunning", anim == "IsRunning" ? true : false);
        _anim.SetBool("IsSprinting", anim == "IsSprinting" ? true : false);
        _anim.SetBool("IsRolling", anim == "IsRolling" ? true : false);
        _anim.SetBool("IsIdle", anim == "IsIdle" ? true : false);
    }
}
