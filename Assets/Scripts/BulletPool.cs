using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private BulletView _bulletPrefab;
    private List<BulletModel> _bulletModels;

    public BulletPool(BulletView prefab)
    {
        ChangePrefab(prefab);
    }

    public void ChangePrefab(BulletView prefab)
    {
        _bulletPrefab = prefab;
        _bulletModels = new List<BulletModel>();
    }

    public BulletModel GetBullet()
    {
        foreach (var bullet in _bulletModels)
        {
            if (!bullet.Active)
            {
                return bullet;
            }
        }

        return AddBullet();
    }

    private BulletModel AddBullet()
    {
        var bulletView = Object.Instantiate(_bulletPrefab);
        var bulletModel = new BulletModel(bulletView.ChangePosition, bulletView.Activate, bulletView.Deactivate);
        bulletView.Subscribe(bulletModel.Deactivate);
        _bulletModels.Add(bulletModel);
        return bulletModel;
    }
}