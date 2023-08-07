using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyFactory OriginFactory { get; set; }

    private GameTile _tileFrom, _tileTo;
    private Vector3  _positionFrom, _positionTo;
    private float    _progress;

    public float Health { get; private set; }

    public void Initialize()
    {
        Health = 100f;
    }

    public void SpawnOn(GameTile tile)
    {
        transform.localPosition = tile.transform.localPosition;
        _tileFrom = tile;
        _tileTo = tile.NextTileOnPath;
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileTo.transform.localPosition;
        _progress = 0f;
    }

    public bool GameUpdate()
    {
        if(Health <= 0f)
        {
            OriginFactory.Reclaim(this);

            return false;
        }

        _progress += Time.deltaTime;

        while (_progress >= 1f)
        {
            _tileFrom = _tileTo;
            _tileTo = _tileTo.NextTileOnPath;

            if (_tileTo == null)
            {
                OriginFactory.Reclaim(this);

                return false;
            }

            _positionFrom = _positionTo;
            _positionTo = _tileTo.transform.localPosition;
            _progress -= 1f;
        }

        transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);

        return true;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
