using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private  float _speed;

    private Coroutine _coroutine;

    public void OnValueChanged(int value, int maxValue)
    {
        float newValue = (float)value / maxValue;

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothValue(newValue));
    }

    private IEnumerator SmoothValue(float newValue)
    {
        while (newValue != _slider.value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, newValue, _speed * Time.deltaTime);

            yield return null;
        }
    }
}