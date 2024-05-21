using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateScriptable/AudioParameters")]
public class AudioParameters : ScriptableObject
{
    [Serializable]
    public class AudioParameter<T>
    {
        public T _value;
        public Action<T> OnChanged;

        public T Value
        {
            set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
            get
            {
                return _value;
            }
        }

        public static implicit operator T(AudioParameter<T> parameter)
        {
            return parameter.Value;
        }
    }

    public AudioParameter<float> MusicVolume;
    public AudioParameter<bool> MusicOn;
    public AudioParameter<float> SFXVolume;
    public AudioParameter<bool> SFXOn;
}
