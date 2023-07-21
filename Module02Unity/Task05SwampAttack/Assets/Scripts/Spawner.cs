using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _timerAfterLastSpawn;
    private int _spawned;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int, int> EnemyCountChenged;

    private void Start()
    {
        SetWave(_currentWaveIndex);    
    }

    private void Update()
    {
        if(_currentWave == null)
        {
            return;
        }

        _timerAfterLastSpawn += Time.deltaTime;

        if(_timerAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timerAfterLastSpawn = 0;
            EnemyCountChenged?.Invoke(_spawned, _currentWave.Count);
        }

        if(_currentWave.Count <= _spawned)
        {
            if(_waves.Count > _currentWaveIndex + 1)
            {
                AllEnemySpawned?.Invoke();
            }

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    public void NextWave()
    {
        SetWave(++_currentWaveIndex);
        _spawned = 0;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        EnemyCountChenged?.Invoke(0, 1);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
