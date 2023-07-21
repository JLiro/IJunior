using System.Collections;
using UnityEngine;

public class FlyingEyeSpawner : MonoBehaviour
{
    [SerializeField] private FlyingEyesDaddy _allEnemyPrefab;
    [SerializeField] private Transform _pointsParent;
    [SerializeField] private FlyingEye _prefab;
    [SerializeField] private float     _coolDown;

    private Transform[] _points;

    private void Awake()
    {
        Instantiate(_allEnemyPrefab);
    }

    private void Start()
    {
        

        _points = new Transform[_pointsParent.childCount];

        for (int i = 0; i < _pointsParent.childCount; i++)
        {
            _points[i] = _pointsParent.GetChild(i);
        }

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds waitForSecond = new WaitForSeconds(_coolDown);

        for (int i = 0; i < _points.Length; i++)
        {
            //.transform.SetParent(_allEnemyPrefab.transform)

            Instantiate(_prefab, _points[i].position, Quaternion.identity).transform.SetParent(_allEnemyPrefab.transform);

            yield return waitForSecond;
        }
    }
}