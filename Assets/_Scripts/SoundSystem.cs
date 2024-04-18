using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] List<SoundNamePair> sounds;
    [SerializeField] AudioMixer _audioMixer;

    private Dictionary<string, AudioClip> _soundNames = new();
    private AudioSource _audioSource;
    private GameSettings _settings;

    [Inject]
    private void Construct(GameSettings settings)
    {
        _settings = settings;
    }
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        ChangeGlobalVolume(_settings.GlobalVolume);
        ChangeMusicVolume(_settings.MusicVolume);
        foreach (var sound in sounds)
            _soundNames.Add(sound.name.ToLower(), sound.sound);
    }

    public void PlaySound(string soundName)
    {
        var name = soundName.ToLower();
        if (!_soundNames.ContainsKey(name))
        {
            Debug.LogError($"SoundSystem doesn't contain a sound named: {name}");
            return;
        }
        _audioSource.PlayOneShot(_soundNames[name]);
    }

    public void ChangeGlobalVolume(float volume)
    {
        _audioMixer.SetFloat("Global", Mathf.Log10(volume)*30);
    }

    public void ChangeMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", Mathf.Log10(volume)*30);
    }

    [Serializable]
    private struct SoundNamePair
    {
        public string name;
        public AudioClip sound;
    }
}