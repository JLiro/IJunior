using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2Int _boardSize;
    [SerializeField] private Board  _board;

    [SerializeField] private TileContentFactory _contentFactory;

    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField, Range(0.1f, 10f)] private float _spawnSpeed;

    private float _spawnProgress;
    private float _maxSpawnProgress = 1f;

    private EnemyCollection _enemies = new EnemyCollection();

    private void Start()
    {
        _board.Initialize(_boardSize, _contentFactory);
    }

    private void Update()
    {
        _spawnProgress += _spawnSpeed * Time.deltaTime;

        while (_spawnProgress >= _maxSpawnProgress)
        {
            _spawnProgress -= _maxSpawnProgress;
            SpawnEnemy();
        }

        _enemies.GameUpdate();

        Physics.SyncTransforms();

        _board.GameUpdate();
    }

    private void SpawnEnemy()
    {
        Tile spawnPoint = _board.GetSpawnPoint(Random.Range(0, _board.SpawnPointCount));
        Enemy enemy = _enemyFactory.Get();
        enemy.SpawnOn(spawnPoint);
        _enemies.Add(enemy);
    }
}