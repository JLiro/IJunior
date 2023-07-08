using System.Collections;
using UnityEngine;

public class FlyingEyeRemover : MonoBehaviour
{
    [SerializeField] private float _timeToRemoveAllEnemy;

    private Coroutine _corutine;

    private void Start()
    {
        _corutine = StartCoroutine(Instantiated());
    }

    private IEnumerator Instantiated()
    {
        yield return new WaitForSeconds(_timeToRemoveAllEnemy);

        foreach (GameObject flyingEye in FindObjectsOfType<GameObject>())
        {
            if (flyingEye.GetComponent<FlyingEye>() != null)
            {
                Destroy(flyingEye);
            }
        }

        StopCoroutine(_corutine);
    }
}
