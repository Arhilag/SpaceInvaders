using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageable
{
    private Action<int> _onTakeDamage;

    public void Subscribe(Action<int> takeDamage)
    {
        _onTakeDamage += takeDamage;
    }
    
    public void TakeDamage(int damage)
    {
        _onTakeDamage?.Invoke(damage);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }
}
