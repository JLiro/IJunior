using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private Tile _tilePrefab;

    private Vector2 _size;
    private Tile[] _tiles;
    private Queue<Tile> _searchFrontier = new Queue<Tile>();
    private TileContentFactory _contentFactory;
    private List<Tile> _spawnPoint = new List<Tile>();
    private List<TileContent> _contentToUpdate = new List<TileContent>();

    public int SpawnPointCount => _spawnPoint.Count;

    public void Initialize(Vector2 size, TileContentFactory contentFactory)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);
        Vector2 offset = new Vector2((size.x - 1) * 0.5f, ((size.y - 1) * 0.5f));
        _tiles = new Tile[(int)(size.x * size.y)];
        _contentFactory = contentFactory;

        for (int tilesCount = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, tilesCount++)
            {
                Tile tile = _tiles[tilesCount] = Instantiate(_tilePrefab);
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);

                if (x > 0)
                {
                    Tile.MakeEastWestNeighbors(tile, _tiles[tilesCount - 1]);
                }
                if (y > 0)
                {
                    Tile.MakeNorthSouthNeighbors(tile, _tiles[tilesCount - (int)(size.x)]);
                }

                tile.IsAlternative = (x & 1) == 0;
                if ((y & 1) == 0)
                {
                    tile.IsAlternative = tile.IsAlternative == false;
                }

                tile.Content = _contentFactory.Get(TileContentType.Empty);
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
            if (tile.Content.Type == TileContentType.Destination)
            {
                tile.BecomeDestination();
                _searchFrontier.Enqueue(tile);
            }
            else
            {
                tile.ClearPath();
            }
        }

        if (_searchFrontier.Count == 0)
        {
            return false;
        }

        while (_searchFrontier.Count > 0)
        {
            Tile tile = _searchFrontier.Dequeue();

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
    public void ToggleDestination(Tile tile)
    {
        if (tile.Content.Type == TileContentType.Destination)
        {
            tile.Content = _contentFactory.Get(TileContentType.Empty);

            if (FindPaths() == false)
            {
                tile.Content = _contentFactory.Get(TileContentType.Destination);
                FindPaths();
            }
        }
        else if (tile.Content.Type == TileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(TileContentType.Destination);
            FindPaths();
        }
    }

    public void ToggleWall(Tile tile)
    {
        if (tile.Content.Type == TileContentType.Wall)
        {
            tile.Content = _contentFactory.Get(TileContentType.Empty);
            FindPaths();
        }
        else if (tile.Content.Type == TileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(TileContentType.Wall);

            if (FindPaths() == false)
            {
                tile.Content = _contentFactory.Get(TileContentType.Empty);
                FindPaths();
            }
        }
    }

    public void ToggleTower(Tile tile)
    {
        if (tile.Content.Type == TileContentType.Tower)
        {
            _contentToUpdate.Remove(tile.Content);
            tile.Content = _contentFactory.Get(TileContentType.Empty);
            FindPaths();
        }
        else if (tile.Content.Type == TileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(TileContentType.Tower);

            if (FindPaths())
            {
                _contentToUpdate.Add(tile.Content);
            }
            else
            {
                tile.Content = _contentFactory.Get(TileContentType.Empty);
                FindPaths();
            }
        }
        else if (tile.Content.Type == TileContentType.Wall)
        {
            tile.Content = _contentFactory.Get(TileContentType.Tower);
            _contentToUpdate.Add(tile.Content);
        }
    }

    public void ToggleSpawnPoint(Tile tile)
    {
        if (tile.Content.Type == TileContentType.SpawnPoint)
        {
            if (_spawnPoint.Count > 1)
            {
                _spawnPoint.Remove(tile);
                tile.Content = _contentFactory.Get(TileContentType.Empty);
            }
        }
        else if (tile.Content.Type == TileContentType.Empty)
        {
            tile.Content = _contentFactory.Get(TileContentType.SpawnPoint);
            _spawnPoint.Add(tile);
        }
    }

    public Tile GetTile(Ray ray)
    {
        RaycastHit hit;
        int defouldLayerMask = 1;

        if (Physics.Raycast(ray, out hit, float.MaxValue, defouldLayerMask))
        {
            int x = (int)(hit.point.x + _size.x * .5f);
            int y = (int)(hit.point.z + _size.y * .5f);

            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return _tiles[x + y * (int)_size.x];
            }
        }

        return null;
    }

    public Tile GetSpawnPoint(int index)
    {
        return _spawnPoint[index];
    }
}