using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [Header("Skills")]
    [SerializeField] bool _canDash;

    [Header("Stats")]
    [SerializeField] Collider2D _aggroRange;
    [SerializeField, Range(1f, 10f)] float _speedBase;
    [SerializeField, Range(1f, 100f)] float _speedMax;
    [SerializeField, Range(0f, 1f)] float _speedEatMultiplier;

    [HideInInspector] public float _speed;

    private void Reset()
    {
        _speedBase = 3f;
        _speedMax = 10f;
        _speedEatMultiplier = 0.2f;
    }

    private void Awake()
    {
        _speed = _speedBase;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // SETTERS
    public void AddSpeed(float speed)
    {
        float new_speed = _speed + speed * _speedEatMultiplier;
        _speed = new_speed < _speedMax ? new_speed : _speedMax;
    }

}
