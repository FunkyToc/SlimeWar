using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeCombat : MonoBehaviour
{
    [Header("Skills")]
    [SerializeField] bool _canEat;
    
    [Header("Stats")]
    [SerializeField] Collider2D _atkRange;
    [SerializeField, Range(1f, 100f)] public float _healthBase;
    [SerializeField, Range(1f, 200f)] float _healthMax;
    [SerializeField, Range(0f, 1f)] float _healthEatMultiplier;

    [HideInInspector] public float _health;

    private void Reset()
    {
        _canEat = true;
        _health = _healthBase;
        _healthBase = 20f;
        _healthMax = 200f;
        _healthEatMultiplier = 0.5f;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    void EatSlime(GameObject target)
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

        // Eat animation then kill
        GameObject.Destroy(target.gameObject);
    }

    // SETTERS
    public void AddHealth(float health)
    {
        float new_health = _health + health * _healthEatMultiplier;
        _health = new_health < _healthBase ? new_health : _healthBase;

        new_health = _healthBase + health * _healthEatMultiplier;
        _healthBase = new_health < _healthMax ? new_health : _healthMax;
    }
}
