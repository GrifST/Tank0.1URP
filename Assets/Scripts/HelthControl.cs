using System;
using UnityEngine;

public class HelthControl : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public Action<GameObject> OnDead;
    public Action<GameObject> OnEnemyDead;
    [SerializeField] private StatSetter _statSetter;
    [SerializeField] private GameObject Enemy;
    [Header("Здоровье и Щиты")]

    [SerializeField] private float _maxHelthPoint;
    [SerializeField] private float _maxShieldPoint;
    [SerializeField] private float _currentSP;
    [SerializeField] private float _currentHP;
    

    public StatSetter Setter
    {
        get => _statSetter;
        set
        {
            _statSetter = value;
            _statSetter.SetHP(_maxHelthPoint, _maxHelthPoint);
            _statSetter.SetSP(_maxShieldPoint, _maxShieldPoint);
        }
    }

    private void Start()
    {
        ResetHelthPoint();
        ResetShielPoint();

    }

    public void PublickSuicid()
    {
        Sucid();
    }
    public void ResetShielPoint()
    {
        
        _currentSP = _maxShieldPoint;
        Setter.SetSP(_currentSP, _maxShieldPoint);
    }
    public void ResetHelthPoint()
    {
        
        _currentHP = _maxHelthPoint;
        Setter.SetHP(_currentHP, _maxHelthPoint);
    }
    public void TakeDamage(float damage)
    {
        _currentSP -= damage;
        Setter.SetSP(_currentSP, _maxShieldPoint);
        if (_currentSP <= 0)
        {
            _currentHP -= damage;
            Setter.SetHP(_currentHP, _maxHelthPoint);

            if (_currentHP <= 0)
            {
                Sucid();
            }
        }
    }

    private void Sucid()
    {
        OnDead?.Invoke(player);
        OnEnemyDead?.Invoke(Enemy);
    }
   
}