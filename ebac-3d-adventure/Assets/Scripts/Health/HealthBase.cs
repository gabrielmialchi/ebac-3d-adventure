using Animation;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int _currentLife;
    public int startLife = 10;
    public int maxLife = 100;

    public bool destroyOnKill = false;

    [SerializeField] private float _delayToDestroy = 3f;

    public Action<HealthBase> OnDamageAction;
    public Action<HealthBase> OnKillAction;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    protected virtual void ResetLife()
    {
        _currentLife = startLife;
    }

    public virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, _delayToDestroy);

        OnKillAction?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(int damage)
    {
        _currentLife -= damage;

        if (_currentLife <= 0)
        {
            Kill();
        }

        OnDamageAction?.Invoke(this);
    }

}
