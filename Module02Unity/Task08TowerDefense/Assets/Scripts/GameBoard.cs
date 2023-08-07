using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] Transform _ground;
    [SerializeField] GameTile  _tilePrefab;

    private Vector2 _size;

    private GameTile[] _tiles;
    
    private bool _isTileAlternative; 

    private Queue<GameTile> _searchFrontier = new Queue<GameTile>();

    private GameTileContentFactory _contentFactory;

    private List<GameTile> _spawnPoint = new List<GameTile>();

    public int SpawnPointCount => _spawnPoint.Count;

    private List<GameTileContent> _contentToUpdate = new List<GameTileContent>();

    public void Initialize(Vector2 size, GameTileContentFactory contentFactory)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2((size.x - 1) * 0.5f, ((size.y - 1) * 0.5f));

        _tiles = new GameTile[(int)(size.x * size.y)];

        _contentFactory = contentFactory;

        for (int tilesCount = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, tilesCount++)
            {
                GameTile tile = _tiles[tilesCount] = Instantiate(_tilePrefab);

                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if (x > 0)
                {
                    GameTile.MakeEastWestNeighbors(tile, _tiles[tilesCount - 1]);
                }

                if (y > 0)
                {
                    GameTile.MakeNorthSouthNeighbors(tile, _tiles[tilesCount - (int)(size.x)]);
                }

                tile.IsAlternative = (x & 1) == 0;

                if ((y & 1) == 0)
                {
                    tile.IsAlternative = tile.IsAlternative == false;
                }

                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }

        ToggleDestination(_tiles[_tiles.Length / 2]);
        ToggleSpawnPoint(_tiles[0]);
    }

    public void GameUpdate()
    {
        for (int i = 0; i < _contentToUpdate.Count; i++)
        {
            _contentToUpdate[i].GameUpdate();
        }
    }

    public bool FindPaths()
    {
        foreach (var tile in _tiles)
        {
            if(tile.Content.Type == GameTileContentType.Destination)
            {
                tile.BecomeDestination();
                _searchFrontier.Enqueue(tile);
            }
            else
            {
                tile.ClearPath();
            }
        }

        if(_searchFrontier.Count == 0)
        {
            return false;
        }

        while(_searchFrontier.Count > 0)
        {
            GameTile tile = _searchFrontier.Dequeue();

            if (tile != null)
            {
                if (tile.IsAlternative)
                {
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                }
                else
                {
                    _searchFrontier.Enqueue(tile.GrowPathWest());
                    _searchFrontier.Enqueue(tile.GrowPathEast());
                    _searchFrontier.Enqueue(tile.GrowPathSouth());
                    _searchFrontier.Enqueue(tile.GrowPathNorth());
                }
            }
        }

        foreach (var tile in _tiles)
        {
            if (tile.HasPath == false)
            {
                return false;
            }
        }

        foreach (var tile in _tiles)
        {
            tile.ShowPath();
        }

        return true;
    }

    public void ToggleDestination(GameTile tile)
    {
        if(tile.Content.Type == GameTileContentType.Destination)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Empty);

            if(FindPaths() == false)
            {
                tile.Content = _contentFactory.Get(GameTileContentType.Destination);

                FindPaths();
            }
        }
        else if(tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Destination);

            FindPaths();
        }
    }

    public void ToggleWall(GameTile tile)
    {
        if(tile.Content.Type == GameTileContentType.Wall)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Empty);

            FindPaths();
        }
        else if(tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Wall);

            if(FindPaths() == false)
            {
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
                FindPaths();
            }
        }
    }

    public void ToggleTower(GameTile tile)
    {
        if (tile.Content.Type == GameTileContentType.Tower)
        {
            _contentToUpdate.Remove(tile.Content);

            tile.Content = _contentFactory.Get(GameTileContentType.Empty);

            FindPaths();
        }
        else if (tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Tower);

            if (FindPaths())
            {
                _contentToUpdate.Add(tile.Content);
            }
            else
            {
                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
                FindPaths();
            }
        }
        else if(tile.Content.Type == GameTileContentType.Wall)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.Tower);

            _contentToUpdate.Add(tile.Content);
        }
    }

    public void ToggleSpawnPoint(GameTile tile)
    {
        if(tile.Content.Type == GameTileContentType.SpawnPoint)
        {
            if(_spawnPoint.Count > 1)
            {
                _spawnPoint.Remove(tile);

                tile.Content = _contentFactory.Get(GameTileContentType.Empty);
            }
        }
        else if(tile.Content.Type == GameTileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(GameTileContentType.SpawnPoint);

            _spawnPoint.Add(tile);
        }
    }

    public GameTile GetTile(Ray ray)
    {
        RaycastHit hit;

        int defouldLayerMask = 1;

        if (Physics.Raycast(ray, out hit, float.MaxValue, defouldLayerMask))
        {
            int x = (int)(hit.point.x + _size.x * .5f);
            int y = (int)(hit.point.z + _size.y * .5f);

            if(x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return _tiles[x + y * (int)_size.x];
            }
        }

        return null;
    }

    public GameTile GetSpawnPoint(int index)
    {
        return _spawnPoint[index];
    }
}