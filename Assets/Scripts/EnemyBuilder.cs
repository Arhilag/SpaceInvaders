using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class EnemyBuilder
{
    private EnemyView _viewPrefab;
    private List<EnemyModel> _enemies = new List<EnemyModel>();

    private CompositeDisposable _disposable = new CompositeDisposable();

    private int _countEnemy;
    private int _countDestroy;

    private Action _onPlusScore;
    private Bonus[] _bonuses;

    public EnemyBuilder(EnemyView viewPrefab, Action scoreMethode, Bonus[] bonuses)
    {
        _onPlusScore += scoreMethode;
        _viewPrefab = viewPrefab;
        _bonuses = bonuses;
    }
    
    public void CreateEnemy(int rows, int columns)
    {
        for (int j = 0; j < rows; j++)
        {
            for (int i = 0; i < columns; i++)
            {
                var view = Object.Instantiate(_viewPrefab);
                var pos = new Vector3(-columns + i * 2, 4-j*2);
                var model = new EnemyModel(1, pos, view.Deactivate, view.Activate);
                model.SetBonuses(_bonuses);
                view.Subscribe(model.TakeDamage);
                view.transform.position = pos;
                model.Active.Subscribe(x =>
                {
                    if (!x)
                    {
                        EnemyDestroy();
                        _onPlusScore?.Invoke();
                    }
                });
                _enemies.Add(model);
                _countEnemy++;
            }
        }
    }

    private async void Restart()
    {
        await Task.Delay(500);
        _countDestroy = 0;
        foreach (var enemy in _enemies)
        {
            enemy.Activate(1);
        }
    }

    private void EnemyDestroy()
    {
        _countDestroy++;
        if (_countDestroy == _countEnemy)
        {
            Restart();
        }
    }
}
