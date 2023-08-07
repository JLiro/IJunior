using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;

    private GameTile _north, _east, _south, _west, _nextOnPath;

    private int _distance;
    public bool HasPath => _distance != int.MaxValue;
    public bool IsAlternative;

    private readonly Quaternion[] _rotations =
    {
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(90f, 90f, 0f),
        Quaternion.Euler(90f, 180f, 0f),
        Quaternion.Euler(90f, 270f, 0f)
    };

    private GameTileContent _content;

    public GameTile NextTileOnPath => _nextOnPath;

    public GameTileContent Content
    {
        get => _content;
        set
        {
            if (_content != null)
            {
                _content.Recycle();
            }

            _content = value;

            _content.transform.localPosition = transform.localPosition;
        }
    }

    public static void MakeNeighbors(GameTile a, GameTile b)
    {
        a._nextOnPath = b;
        b._nextOnPath = a;
    }

    public static void MakeEastWestNeighbors(GameTile east, GameTile west)
    {
        east._west = west;
        west._east = east;
    }

    public static void MakeNorthSouthNeighbors(GameTile north, GameTile south)
    {
        north._south = south;
        south._north = north;
    }

    public void ClearPath()
    {
        _distance = int.MaxValue;
        _nextOnPath = null;
    }

    public void BecomeDestination()
    {
        _distance = 0;
        _nextOnPath = null;
    }

    public void ShowPath()
    {
        _arrow.gameObject.SetActive(_distance > 0);

        if (_distance > 0)
        {
            int index = (_nextOnPath == _north) ? 0 : (_nextOnPath == _east) ? 1 : (_nextOnPath == _south) ? 2 : 3;
            _arrow.localRotation = _rotations[index];
        }
    }

    public GameTile GrowPathNorth() => GrowPathTo(_north);

    public GameTile GrowPathEast() => GrowPathTo(_east);

    public GameTile GrowPathSouth() => GrowPathTo(_south);

    public GameTile GrowPathWest() => GrowPathTo(_west);

    private GameTile GrowPathTo(GameTile neighbor)
    {
        if (!HasPath || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;

        return neighbor.Content.IsBlockingPath ? null : neighbor;
    }

    public GameTile GrowPath()
    {
        if (!HasPath || _nextOnPath == null || _nextOnPath.HasPath)
        {
            return null;
        }

        _nextOnPath._distance = _distance + 1;
        return _nextOnPath.Content.IsBlockingPath ? null : _nextOnPath;
    }
}

public enum Direction
{
    North,
    East,
    South,
    West
}