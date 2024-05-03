using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    [SerializeField] private Transform _viewContainer;
    private Action _onDeactivate;

    public void Subscribe(Action deactivateMethode)
    {
        _onDeactivate += deactivateMethode;
    }
    
    public void ChangePosition(Vector2 newPosition)
    {
        _viewContainer.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(1);
        }
        _onDeactivate?.Invoke();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
