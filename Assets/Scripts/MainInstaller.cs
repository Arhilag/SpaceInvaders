using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private Bonus[] _bonuses;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private BulletView _bulletPrefab;
    [SerializeField] private EnemyView _enemyViewPrefab;
    [SerializeField] private ScoreCounter _scoreCounter;

    private EnemyBuilder _enemyBuilder;
    private BulletPool _bulletPool;
    
    public override void InstallBindings()
    {
        _enemyBuilder = new EnemyBuilder(_enemyViewPrefab, _scoreCounter.PlusScore, _bonuses);
        _enemyBuilder.CreateEnemy(2,5);
        _bulletPool = new BulletPool(_bulletPrefab);
        _playerView.Subscribe(_bulletPool.ChangePrefab);
        var playerModel = new PlayerModel(new Vector2(0, -3), 5, _bulletPool, _playerView.ChangePosition);
        
        Container.Bind<PlayerModel>().FromInstance(playerModel);
    }
}