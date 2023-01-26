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
    public bool canShieldRegen = false;
    [SerializeField] private float shieldRegen;
    [SerializeField] private float shieldRegenColdown;
    private float shieldRegenTimer;
    private StatSetter _statSetter;
    private BoxCollider2D characterCollider;
    [SerializeField] private GameObject _deadEffect;
    public StatSetter statSetter
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
        characterCollider = GetComponent<BoxCollider2D>();
        statSetter = GameManager.main.CreateStatSetter();
        ResetHelthPoint();
        ResetShielPoint();
    }


    public void ResetShielPoint()
    {

        _currentSP = _maxShieldPoint;
        statSetter.SetSP(_currentSP, _maxShieldPoint);
    }
    public void ResetHelthPoint()
    {

        _currentHP = _maxHelthPoint;
        statSetter.SetHP(_currentHP, _maxHelthPoint);
    }
    public void TakeDamage(float damage)
    {
        shieldRegenTimer = 0;
        if (_currentSP > 0) _currentSP -= damage;
        
        
       
        if (_currentSP <= 0)
        {
            if (canShieldRegen) canShieldRegen = false;
            _currentHP -= damage;
           

            if (_currentHP <= 0)
            {
                Kill();
            }
        }
        statSetter.SetSP(_currentSP, _maxShieldPoint);
        statSetter.SetHP(_currentHP, _maxHelthPoint);
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
        Instantiate(_deadEffect, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        statSetter.transform.position =  Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, characterCollider.bounds.min.y));
        if (!canShieldRegen) return;
        if(shieldRegenTimer > shieldRegenColdown && _currentSP < _maxShieldPoint)
        {
            _currentSP += shieldRegen * Time.deltaTime;
            if (_currentSP > _maxShieldPoint) _currentSP = _maxShieldPoint;
            statSetter.SetSP(_currentSP, _maxShieldPoint);
        }
        else
        {
            shieldRegenTimer += Time.deltaTime;
        }
    }
    protected virtual void OnDestroy()
    {
       if(statSetter) Destroy(statSetter.gameObject);

    }
}
