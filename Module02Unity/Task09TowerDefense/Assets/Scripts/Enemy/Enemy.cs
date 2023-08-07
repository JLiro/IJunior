using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyFactory _originFactory;

    private Tile _tileFrom;
    private Tile _tileTo;
    private Vector3 _positionFrom;
    private Vector3 _positionTo;
    private float _progress;

    private bool IsPathComplete => _tileTo == null;

    public float Health { get; private set; }

    public void Initialize()
    {
        Health = 100f;
    }

    public void SpawnOn(Tile tile)
    {
        transform.localPosition = tile.transform.localPosition;
        SetDestination(tile);
        _progress = 0f;
    }

    public bool UpdatePath()
    {
        if (Health <= 0f)
        {
            _originFactory.Reclaim(this);
            return false;
        }

        _progress += Time.deltaTime;

        while (_progress >= 1f)
        {
            _tileFrom = _tileTo;
            _tileTo = _tileTo.NextTileOnPath;

            if (IsPathComplete)
            {
                _originFactory.Reclaim(this);
                return false;
            }

            _positionFrom = _positionTo;
            _positionTo = _tileTo.transform.localPosition;

            _progress -= 1f;
        }

        UpdatePosition();

        return true;
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Max(0f, Health - damage);
    }

    private void SetDestination(Tile tile)
    {
        _tileFrom = tile;
        _tileTo = tile.NextTileOnPath;
        _positionFrom = _tileFrom.transform.localPosition;
        _positionTo = _tileTo.transform.localPosition;
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
    }
}