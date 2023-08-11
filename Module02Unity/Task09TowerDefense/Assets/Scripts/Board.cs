using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private Tile _tilePrefab;

    private Vector2 _gridSize;
    private Tile[] _tiles;
    private Queue<Tile> _searchFrontier = new Queue<Tile>();
    private TileContentFactory _contentFactory;
    private List<Tile> _spawnPoint = new List<Tile>();
    private List<TileContent> _contentToUpdate = new List<TileContent>();

    public int SpawnPointCount => _spawnPoint.Count;

    public void Initialize(Vector2 gridSize, TileContentFactory contentFactory)
    {
        float scaleZ = 1f;
        float tileHeight = 0f;
        int tileIndexOffset = 1;
        int bitMask = 1;
        int centerTileIndex = 2;
        int firstTileIndex = 0;
        float half = 0.5f;

        _gridSize = gridSize;
        _ground.localScale = new Vector3(gridSize.x, gridSize.y, scaleZ);

        Vector2 gridCenterOffset = (gridSize - Vector2.one) * half;

        _tiles = new Tile[(int)(gridSize.x * gridSize.y)];
        _contentFactory = contentFactory;

        CreateTiles(gridSize, tileHeight, gridCenterOffset);
        SetTileNeighbors(gridSize, tileIndexOffset);
        SetTileAlternatives(bitMask);
        SetTileContent(TileContentType.Empty);
        ToggleDestinationAndSpawnPoint(centerTileIndex, firstTileIndex);
    }

    private void CreateTiles(Vector2 gridSize, float tileHeight, Vector2 gridCenterOffset)
    {
        for (int tileIndex = 0, y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++, tileIndex++)
            {
                Tile tile = Instantiate(_tilePrefab, transform);
                tile.transform.localPosition = new Vector3(x, tileHeight, y) - new Vector3(gridCenterOffset.x, 0, gridCenterOffset.y);
                _tiles[tileIndex] = tile;
            }
        }
    }

    private void SetTileNeighbors(Vector2 gridSize, int tileIndexOffset)
    {
        for (int tileIndex = 0, y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++, tileIndex++)
            {
                if (x > 0)
                {
                    Tile.MakeEastWestNeighbors(_tiles[tileIndex], _tiles[tileIndex - tileIndexOffset]);
                }

                if (y > 0)
                {
                    Tile.MakeNorthSouthNeighbors(_tiles[tileIndex], _tiles[tileIndex - (int)(gridSize.x)]);
                }
            }
        }
    }

    private void SetTileAlternatives(int bitMask)
    {
        for (int tileIndex = 0, y = 0; y < _gridSize.y; y++)
        {
            for (int x = 0; x < _gridSize.x; x++, tileIndex++)
            {
                Tile tile = _tiles[tileIndex];
                tile.IsAlternative = (x & bitMask) == 0;
                if ((y & bitMask) == 0)
                {
                    tile.IsAlternative = !tile.IsAlternative;
                }
            }
        }
    }

    private void SetTileContent(TileContentType contentType)
    {
        foreach (Tile tile in _tiles)
        {
            tile.Content = _contentFactory.Get(contentType);
        }
    }

    private void ToggleDestinationAndSpawnPoint(int centerTileIndex, int firstTileIndex)
    {
        ToggleDestination(_tiles[_tiles.Length / centerTileIndex]);
        ToggleSpawnPoint(_tiles[firstTileIndex]);
    }

    public void GameUpdate()
    {
        foreach (var content in _contentToUpdate)
        {
            content.GameUpdate();
        }
    }

    public bool FindPaths()
    {
        InitializeTiles();

        if (_searchFrontier.Count == 0)
        {
            return false;
        }

        ExpandPaths();

        if (AllTilesHavePath() == false)
        {
            return false;
        }

        ShowPaths();

        return true;
    }

    private void InitializeTiles()
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
    }

    private void ExpandPaths()
    {
        while (_searchFrontier.Count > 0)
        {
            Tile tile = _searchFrontier.Dequeue();
            if (tile != null)
            {
                var paths = tile.IsAlternative ?
                    new[] { tile.GrowPathNorth(), tile.GrowPathSouth(), tile.GrowPathEast(), tile.GrowPathWest() } :
                    new[] { tile.GrowPathWest(), tile.GrowPathEast(), tile.GrowPathSouth(), tile.GrowPathNorth() };

                foreach (var path in paths)
                {
                    _searchFrontier.Enqueue(path);
                }
            }
        }
    }

    private bool AllTilesHavePath() => _tiles.All(tile => tile.HasPath);

    private void ShowPaths()
    {
        foreach (var tile in _tiles)
        {
            tile.ShowPath();
        }
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
        int defaultLayerMask = 1;
        float coordinateOffset = .5f;

        if (Physics.Raycast(ray, out hit, float.MaxValue, defaultLayerMask))
        {
            int x = (int)(hit.point.x + _gridSize.x * coordinateOffset);
            int y = (int)(hit.point.z + _gridSize.y * coordinateOffset);

            if (x >= 0 && x < _gridSize.x && y >= 0 && y < _gridSize.y)
            {
                return _tiles[x + y * (int)_gridSize.x];
            }
        }

        return null;
    }

    public Tile GetSpawnPoint(int index) => _spawnPoint[index];
}