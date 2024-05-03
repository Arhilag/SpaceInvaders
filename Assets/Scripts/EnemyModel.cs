using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyModel
{
    public ReactiveProperty<bool> Active = new ReactiveProperty<bool>();
    private int _hp;
    private Vector3 _position;
    private Action _onDeactivate;
    private Action _onActivate;
    private Bonus[] _bonuses;

    public EnemyModel(int maxHp, Vector3 position, Action deactivateMethode, Action activateMethode)
    {
        _position = position;
        _onDeactivate += deactivateMethode;
        _onActivate += activateMethode;
        Activate(maxHp);
    }

    public void SetBonuses(Bonus[] bonuses)
    {
        _bonuses = bonuses;
    }
    
    public void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Active.Value = false;
            if (Random.Range(0, 10) == 0)
            {
                var bonus = Object.Instantiate(_bonuses[Random.Range(0, _bonuses.Length)]);
                bonus.transform.position = _position;
            }
            _onDeactivate?.Invoke();
        }
    }

    public void Activate(int maxHp)
    {
        _hp = maxHp;
        Active.Value = true;
        _onActivate?.Invoke();
    }
}
