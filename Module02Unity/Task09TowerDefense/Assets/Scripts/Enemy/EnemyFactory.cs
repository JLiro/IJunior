using UnityEngine;

[CreateAssetMenu]
public class EnemyFactory : ObjectFactory
{
    [SerializeField] private Enemy _prefab;

    public Enemy Get()
    {
        Enemy instance = CreateGameObjectInstance(_prefab);

        instance.Initialize();

        return instance;
    }

    public void Reclaim(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
