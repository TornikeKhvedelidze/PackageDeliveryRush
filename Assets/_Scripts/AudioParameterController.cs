using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioParameterController : MonoBehaviour
{
    [SerializeField] private AudioParameters _parameters;
    [SerializeField] private Toggle _SFXToggle;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Toggle _MusicToggle;
    [SerializeField] private Slider _MusicSlider;

    private void Start()
    {
        _SFXToggle.onValueChanged.AddListener((value) =>
        {
            _parameters.SFXOn.Value = value;
        });

        _SFXSlider.onValueChanged.AddListener((value) =>
        {
            _parameters.SFXVolume.Value = value;
        });

        _MusicToggle.onValueChanged.AddListener((value) =>
        {
            _parameters.MusicOn.Value = value;
        });

        _MusicSlider.onValueChanged.AddListener((value) =>
        {
            _parameters.MusicVolume.Value = value;
        });
    }
}
