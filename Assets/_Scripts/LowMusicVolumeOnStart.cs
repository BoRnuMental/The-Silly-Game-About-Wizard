using DG.Tweening;
using UnityEngine;
using Zenject;

public class LowMusicVolumeOnStart : MonoBehaviour
{
    [SerializeField] float _duration;
    private AudioSource _audioSource;
    private float _endValue;

    [Inject]
    private void Construct(GameSettings settings)
    {
        _endValue = settings.MusicVolume;
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
        _audioSource.DOFade(_endValue, _duration);
    }
}