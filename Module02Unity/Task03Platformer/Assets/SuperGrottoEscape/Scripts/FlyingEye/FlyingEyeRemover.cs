using System.Collections;
using UnityEngine;

public class FlyingEyeRemover : MonoBehaviour
{
    [SerializeField] private FlyingEyeSpawner _enemyParent;
    [SerializeField] private float _coolDown;

    private void Start()
    {
        StartCoroutine(Destroing());
    }

    private IEnumerator Destroing()
    {
        WaitForSeconds waitForSecond = new WaitForSeconds(_coolDown);

        int childCount = _enemyParent.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(_enemyParent.transform.GetChild(i).gameObject);

            yield return waitForSecond;
        }
    }
}
