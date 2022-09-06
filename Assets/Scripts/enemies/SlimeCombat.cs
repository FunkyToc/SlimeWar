using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeCombat : MonoBehaviour
{
    [Header("Skills")]
    [SerializeField] bool _canEat;
    
    [Header("Stats")]
    [SerializeField] Collider2D _atkRange;
    [SerializeField, Range(1f, 10f)] float _atkBase;
    [SerializeField, Range(1f, 100f)] float _atkMax;
    [SerializeField, Range(0f, 1f)] float _atkEatMultiplier;
    [SerializeField, Range(1f, 100f)] public float _healthBase;
    [SerializeField, Range(1f, 100f)] float _healthMax;
    [SerializeField, Range(0f, 1f)] float _healthEatMultiplier;

    [HideInInspector] public float _atk;
    [HideInInspector] public float _health;

    private void Reset()
    {
        _canEat = true;
        _atkBase = 1f;
        _atkMax = 10f;
        _atkEatMultiplier = 0.5f;
        _atk = _atkBase;
        _healthBase = 20f;
        _healthMax = 200f;
        _healthEatMultiplier = 0.5f;
        _health = _healthBase;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    void EatSlime(SlimeTag target)
    {
        SlimeSize target_size = target.GetComponent<SlimeSize>();
        SlimeSize self_size = GetComponent<SlimeSize>();
        if (!_canEat || target_size.GetColor() == self_size.GetColor()) return;

        SlimeMovement self_movement = GetComponent<SlimeMovement>();
        SlimeMovement target_movement = target.GetComponent<SlimeMovement>();
        SlimeCombat target_combat = target.GetComponent<SlimeCombat>();

        self_size.AddSize(target_movement._speed);
        self_movement.AddSpeed(target_movement._speed);
        this.AddHealth(target_combat._healthBase);
        this.AddAtk(target_combat._atkBase);

        // Eat animation then kill
        //GameObject.Destroy(target.gameObject);
    }

    public void AddHealth(float health)
    {
        float new_health = _health + health * _healthEatMultiplier;
        _health = new_health < _healthBase ? new_health : _healthBase;

        new_health = _healthBase + health * _healthEatMultiplier;
        _healthBase = new_health < _healthMax ? new_health : _healthMax;
    }

    public void AddAtk(float atk)
    {
        float new_atk = _atk + atk * _atkEatMultiplier;
        _atk = new_atk < _atkMax ? new_atk : _atkMax;
    }
}
