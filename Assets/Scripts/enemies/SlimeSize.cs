using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSize : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] SlimeColor _slimeColor;
    [SerializeField, Range(1f, 10f)] public float _sizeBase;
    [SerializeField, Range(1f, 10f)] public float _sizeMax;
    [SerializeField, Range(0f, 1f)] public float _sizeEatMultiplier;

    [HideInInspector] public float _size;

    void Reset()
    {
        _sizeBase = 1f;
        _sizeMax = 10f;
        _sizeEatMultiplier = 0.5f;
    }

    private void Awake()
    {
        _size = _sizeBase;
    }

    private void OnValidate()
    {
        float s = _sizeBase < _sizeMax ? _sizeBase : _sizeMax;
        transform.localScale = new Vector3(s, s, 1);
    }

    void UpdateSizeRender()
    {
        transform.localScale = new Vector3(_size, _size, 1);
    }

    // GETTERS
    public float GetColor() 
    {
        return (float) _slimeColor;
    }
    
    // SETTERS
    public void AddSize(float size, bool updateRender = true) 
    {
        float new_size = _size + size * _sizeEatMultiplier;
        _size = new_size < _sizeMax ? new_size : _sizeMax;

        if (updateRender) UpdateSizeRender();
    }

}