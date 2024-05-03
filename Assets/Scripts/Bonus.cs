using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private BulletView _bulletView;
    public BulletView BulletView => _bulletView;
    
    private float _speed = 7;
    private IDisposable _move;

    private void Start()
    {
        _move = Observable.EveryFixedUpdate().Subscribe(l => Move());
    }
    
    private void Move()
    {
        transform.position += Vector3.down * (_speed * Time.deltaTime);
        
        if (transform.position.y <= -7)
        {
            _move.Dispose();
            Destroy(gameObject);
        }
    }

    public void DestroyBonus()
    {
        _move.Dispose();
        Destroy(gameObject);
    }
}
