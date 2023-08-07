using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;

    private Tile _north, _east, _south, _west, _nextOnPath;

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

    private TileContent _content;

    public Tile NextTileOnPath => _nextOnPath;

    public TileContent Content
    {
        get
        {
            return _content;
        }
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

    public static void MakeEastWestNeighbors(Tile east, Tile west)
    {
        east._west = west;
        west._east = east;
    }

    public static void MakeNorthSouthNeighbors(Tile north, Tile south)
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

    public Tile GrowPathNorth() => GrowPathTo(_north);

    public Tile GrowPathEast() => GrowPathTo(_east);

    public Tile GrowPathSouth() => GrowPathTo(_south);

    public Tile GrowPathWest() => GrowPathTo(_west);

    private Tile GrowPathTo(Tile neighbor)
    {
        if (!HasPath || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;

        return neighbor.Content.IsBlockingPath ? null : neighbor;
    }
}