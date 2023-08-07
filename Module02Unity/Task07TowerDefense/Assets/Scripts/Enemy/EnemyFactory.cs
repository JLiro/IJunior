using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : GameObjectFactory
{
    [SerializeField] private Enemy _prefab;

    public Enemy Get()
    {
        Enemy instance = CrateGameObjectInstance(_prefab);

        instance.Initialize();

        return instance;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
