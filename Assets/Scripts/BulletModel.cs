using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class BulletModel
{
    private float _speed;
    private Vector2 _transform;
    private Action<Vector2> _onChangePosition;
    private IDisposable _move;
    private bool _active;
    public bool Active => _active;
    private Action _onActivate;
    private Action _onDeactivate;
    
    public BulletModel(Action<Vector2> moveMethode, Action activateMethode, Action deactivateMethode)
    {
        _onChangePosition += moveMethode;
        _onActivate += activateMethode;
        _onDeactivate += deactivateMethode;
    }

    public void Activate(Vector2 startPosition, float speed)
    {
        _active = true;
        _transform = startPosition;
        _speed = speed;
        _onChangePosition?.Invoke(_transform);
        _move = Observable.EveryFixedUpdate().Subscribe(l => Move());
        _onActivate?.Invoke();
    }

    private void Move()
    {
        _transform += Vector2.up * (_speed * Time.deltaTime);
        _onChangePosition?.Invoke(_transform);
        if (_transform.y >= 7)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        _move.Dispose();
        _active = false;
        _onDeactivate?.Invoke();
    }
}
