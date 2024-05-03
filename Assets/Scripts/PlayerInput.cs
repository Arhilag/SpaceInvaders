using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    private float hor;
    private float ver;
    
    private Action<Vector2> _onChangePosition;
    private Action _onShoot;

    [Inject]
    private void Init(PlayerModel model)
    {
        _onChangePosition += model.AddMove;
        _onShoot += model.Shoot;
    }
    
    private void Update()
    {
        if (hor is > 0.1f or < -0.1f ||
            ver is > 0.1f or < -0.1f)
        {
            _onChangePosition.Invoke(new Vector2(hor, ver));
        }
    }

    public void Right()
    {
        hor = 1;
    }

    public void Left()
    {
        hor = -1;
    }

    public void Up()
    {
        ver = 1;
    }

    public void Down()
    {
        ver = -1;
    }

    public void UpHor()
    {
        hor = 0;
    }

    public void UpVer()
    {
        ver = 0;
    }

    public void Shoot()
    {
        _onShoot?.Invoke();
    }
}
