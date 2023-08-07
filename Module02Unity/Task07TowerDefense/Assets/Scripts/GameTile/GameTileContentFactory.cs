using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : GameObjectFactory
{
    [SerializeField] private GameTileContent _destinationPrefab;
    [SerializeField] private GameTileContent _emptyPrefab;
    [SerializeField] private GameTileContent _wallPrefab;
    [SerializeField] private GameTileContent _spawnPrefab;
    [SerializeField] private GameTileContent _towerPrefab;

    public void Reclaim(GameTileContent content)
    {
        Destroy(content.gameObject);
    }

    public GameTileContent Get(GameTileContentType type)
    {
        switch (type)
        {
            case GameTileContentType.Destination:
                return Get(_destinationPrefab);

            case GameTileContentType.Empty:
                return Get(_emptyPrefab);

            case GameTileContentType.Wall:
                return Get(_wallPrefab);

            case GameTileContentType.SpawnPoint:
                return Get(_spawnPrefab);

            case GameTileContentType.Tower:
                return Get(_towerPrefab);
        }

        return null;
    }

    private GameTileContent Get(GameTileContent prefab)
    {
        GameTileContent instance = CrateGameObjectInstance(prefab);

        instance.OriginFactory = this;

        return instance;
    }
}
