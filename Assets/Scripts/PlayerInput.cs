using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    private Action<Vector2> _onChangePosition;
    private Action _onShoot;

    [Inject]
    private void Init(PlayerModel model)
    {
        _onChangePosition += model.AddMove;
        _onShoot += model.Shoot;
    }
    
    // private void Update()
    // {
    //     var hor = Input.GetAxis("Horizontal");
    //     var ver = Input.GetAxis("Vertical");
    //     
    //     if (hor is > 0.1f or < -0.1f ||
    //         ver is > 0.1f or < -0.1f)
    //     {
    //         _onChangePosition.Invoke(new Vector2(hor, ver));
    //     }
    //
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Shoot();
    //     }
    // }

    public void Right()
    {
        _onChangePosition.Invoke(new Vector2(1, 0));
    }

    public void Left()
    {
        _onChangePosition.Invoke(new Vector2(-1, 0));
    }

    public void Up()
    {
        _onChangePosition.Invoke(new Vector2(0, 1));
    }

    public void Down()
    {
        _onChangePosition.Invoke(new Vector2(0, -1));
    }

    public void Shoot()
    {
        _onShoot?.Invoke();
    }
}
