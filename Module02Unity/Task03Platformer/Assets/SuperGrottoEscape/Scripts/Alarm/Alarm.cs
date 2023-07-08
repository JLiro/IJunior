using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _speedVolume;

    private AudioSource _sound;
    private Coroutine   _changeVolume;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.volume = 0f;
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_sound.volume != targetVolume)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, targetVolume, _speedVolume * Time.deltaTime);

            yield return null;
        }
    }

    public void Play()
    {
        if(_changeVolume != null)
        {
            StopCoroutine(_changeVolume);
        }
        
        _sound.Play();
        _changeVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void Stop()
    {
        StopCoroutine(_changeVolume);
        _changeVolume = StartCoroutine(ChangeVolume(_minVolume));

        if (_sound.volume == _minVolume)
        {
            _sound.Stop();
        }
    }
}
