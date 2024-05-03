using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _viewContainer;
    private Action<BulletView> _onTakeBonus;

    public void Subscribe(Action<BulletView> bonusMethode)
    {
        _onTakeBonus += bonusMethode;
    }
    
    public void ChangePosition(Vector2 newPosition)
    {
        _viewContainer.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bonus bonus))
        {
            _onTakeBonus?.Invoke(bonus.BulletView);
            bonus.DestroyBonus();
        }
    }
}
