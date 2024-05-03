using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerModel
{
    private float _speed;
    private Vector2 _transform;
    private BulletPool _bulletPool;
    private Action<Vector2> _onChangePosition;

    public PlayerModel(Vector2 startPosition, float speed, BulletPool bulletPool, Action<Vector2> methode)
    {
        _transform = startPosition;
        _speed = speed;
        _bulletPool = bulletPool;
        _onChangePosition += methode;
        _onChangePosition?.Invoke(_transform);
    }

    public void AddMove(Vector2 direction)
    {
        _transform += direction * (_speed * Time.deltaTime);
        _onChangePosition?.Invoke(_transform);
    }

    public void Shoot()
    {
        var bullet = _bulletPool.GetBullet();
        bullet.Activate(_transform, 10);
    }
}
