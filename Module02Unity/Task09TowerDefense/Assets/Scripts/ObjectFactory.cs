using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ObjectFactory : ScriptableObject
{
    private Scene _scene;

    protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
    {
        if (!_scene.isLoaded)
        {
            _scene = SceneManager.GetSceneByName(name);
        }

        if (!_scene.isLoaded)
        {
            _scene = SceneManager.CreateScene(name);
        }

        T instance = Instantiate(prefab);
        SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);

        return instance;
    }
}