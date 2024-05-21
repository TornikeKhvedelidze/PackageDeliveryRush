using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioParameters audioParameters;
    [SerializeField] private AudioClipsData _audioDataScriptable;

    private AudioSource _audioSource;

    public const string AudioIdsQuery =
#if UNITY_EDITOR
        "AudioManager.ListAudioFiles";
#else
"";
#endif

    private void Start()
    {
        DontDestroyOnLoad(this);

        _audioSource = gameObject.AddComponent<AudioSource>();

        audioParameters.SFXOn.OnChanged += SetSFXParameters;
        audioParameters.SFXVolume.OnChanged += SetSFXParameters;

        audioParameters.MusicOn.OnChanged += SetMusicParameters;
        audioParameters.MusicVolume.OnChanged += SetMusicParameters;
    }

    private void OnDestroy()
    {
        audioParameters.SFXOn.OnChanged -= SetSFXParameters;
        audioParameters.SFXVolume.OnChanged -= SetSFXParameters;

        audioParameters.MusicOn.OnChanged -= SetMusicParameters;
        audioParameters.MusicVolume.OnChanged -= SetMusicParameters;
    }

    public static string[] ListAudioFiles()
    {
        return Instance._audioDataScriptable.ListAudioFileNames();
    }

    public static void PlayAudio(string name)
    {
        if(!Instance._audioDataScriptable.TryGetAudio(name, out var audioClip))
        {
            Debug.LogError($"Could't found audioclip id: {name}");
            return;
        }

        PlayAudio(audioClip);
    }

    private static void PlayAudio(AudioClip audioClip)
    {
        Instance._audioSource.PlayOneShot(audioClip);
    }

    private void SetSFXParameters<T>(T value)
    {
        if (typeof(T) == typeof(bool))
        {
            _audioSource.enabled = (bool)(object)value;
        }
        else if (typeof(T) == typeof(float))
        {
            _audioSource.volume = (float)(object)value;
        }
    }

    private void SetMusicParameters<T>(T value)
    {
        if (typeof(T) == typeof(bool))
        {
            _musicAudioSource.enabled = (bool)(object)value;
        }
        else if (typeof(T) == typeof(float))
        {
            _musicAudioSource.volume = (float)(object)value;
        }
    }
}