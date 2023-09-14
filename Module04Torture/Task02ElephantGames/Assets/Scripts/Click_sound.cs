using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Click_sound : MonoBehaviour
{
    [SerializeField] private AudioClip _saund;
    [SerializeField] private float _volume;

    private AudioSource _audioSource => GetComponent<AudioSource>();

    public void OnMouseDown()
    {
        _audioSource.PlayOneShot(_saund, _volume);
    }
}