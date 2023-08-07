using UnityEngine;

[CreateAssetMenu]
public class TileContentFactory : ObjectFactory
{
    [SerializeField] private TileContent[] _prefabs;

    public void Reclaim(TileContent content)
    {
        Destroy(content.gameObject);
    }

    public TileContent Get(TileContentType type)
    {
        foreach (var prefab in _prefabs)
        {
            if (prefab.Type == type)
            {
                TileContent instance = CreateGameObjectInstance(prefab);
                instance.OriginFactory = this;

                return instance;
            }
        }

        return null;
    }
}