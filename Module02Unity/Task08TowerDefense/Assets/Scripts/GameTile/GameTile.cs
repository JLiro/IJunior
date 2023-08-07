using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _arrow;

    private GameTile _north, _east, _south, _west, _nextOnPath;

    private int _distance;
    public bool HasPath => _distance != int.MaxValue;
    public bool IsAlternative;

    private Quaternion _northRotation = Quaternion.Euler(90f, 0f, 0f);
    private Quaternion _eastRotation  = Quaternion.Euler(90f, 90f, 0f);
    private Quaternion _southRotation = Quaternion.Euler(90f, 180f, 0f);
    private Quaternion _westRotation  = Quaternion.Euler(90f, 270f, 0f);

    private GameTileContent _content;

    public GameTile NextTileOnPath => _nextOnPath;

    public GameTileContent Content
    {
        get => _content;
        set
        {
            if(_content != null)
            {
                _content.Recycle();
            }

            _content = value;

            _content.transform.localPosition = transform.localPosition;
        }
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
        if (_distance == 0)
        {
            _arrow.gameObject.SetActive(false);
            return;
        }

        _arrow.gameObject.SetActive(true);

        if(_nextOnPath == _north)
        {
            _arrow.localRotation = _northRotation;
        }
        else if(_nextOnPath == _east)
        {
            _arrow.localRotation = _eastRotation;
        }
        else if (_nextOnPath == _south)
        {
            _arrow.localRotation = _southRotation;
        }
        else
        {
            _arrow.localRotation = _westRotation;
        }
    }

    public GameTile GrowPathNorth() => GrowPathTo(_north);

    public GameTile GrowPathEast() => GrowPathTo(_east);

    public GameTile GrowPathSouth() => GrowPathTo(_south);

    public GameTile GrowPathWest() => GrowPathTo(_west);

    private GameTile GrowPathTo(GameTile neighbor)
    {
        if(HasPath == false || neighbor == null || neighbor.HasPath)
        {
            return null;
        }

        neighbor._distance = _distance + 1;
        neighbor._nextOnPath = this;

        return neighbor.Content.IsBlockingPath ? null : neighbor;
    }
}