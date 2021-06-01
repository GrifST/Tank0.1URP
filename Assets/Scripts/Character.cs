using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isPlayer = false;
    public Tank tank;
    public Turret tower;

    [Header("Здоровье и Щиты")]

    [SerializeField] private float _maxHelthPoint;
    [SerializeField] private float _maxShieldPoint;
    [SerializeField] private float _currentSP;
    [SerializeField] private float _currentHP;
    [SerializeField] private float shieldRegen;
    [SerializeField] private float shieldRegenColdown;
    private float shieldRegenTimer;
    [SerializeField] private StatSetter _statSetter;
    public StatSetter setter
    {
        get => _statSetter;
        set
        {
            _statSetter = value;
            _statSetter.SetHP(_maxHelthPoint, _maxHelthPoint);
            _statSetter.SetSP(_maxShieldPoint, _maxShieldPoint);
        }
    }

    protected virtual void Start()
    {
        ResetHelthPoint();
        ResetShielPoint();
    }


    public void ResetShielPoint()
    {

        _currentSP = _maxShieldPoint;
        setter.SetSP(_currentSP, _maxShieldPoint);
    }
    public void ResetHelthPoint()
    {

        _currentHP = _maxHelthPoint;
        setter.SetHP(_currentHP, _maxHelthPoint);
    }
    public void TakeDamage(float damage)
    {
        shieldRegenTimer = 0;
        _currentSP -= damage;
       
        if (_currentSP <= 0)
        {
            _currentHP -= damage;
           

            if (_currentHP <= 0)
            {
                Kill();
            }
        }
        setter.SetSP(_currentSP, _maxShieldPoint);
        setter.SetHP(_currentHP, _maxHelthPoint);
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (shieldRegen == 0) return;
        if(shieldRegenTimer > shieldRegenColdown && _currentSP < _maxShieldPoint)
        {
            _currentSP += shieldRegen * Time.deltaTime;
            if (_currentSP > _maxShieldPoint) _currentSP = _maxShieldPoint;
            setter.SetSP(_currentSP, _maxShieldPoint);
        }
        else
        {
            shieldRegenTimer += Time.deltaTime;
        }
    }

}
