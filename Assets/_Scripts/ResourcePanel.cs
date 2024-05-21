using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CustomInspector;
using System;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _receiveAudio;

    [SerializeField] private ResourceScriptable _resource;
    [SerializeField] private Image _resourceIcon;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private ButtonPressEffect _doPunch;
    [SerializeField] private float _countDelay = 0.03f;

    private float _newAmount;
    private float _currentAmount;
    private float _countTimer;

    private float _currentAcceleration = 1;
    private void Start()
    {
        Initialise();
        _resource.OnValueChanged += SetAmount;
    }

    private void Update()
    {
        if (_currentAmount == _newAmount)
        {
            _currentAcceleration = 0;
            return;
        }

        EncreaseValue();

        if (_countTimer > 0)
        {
            _countTimer -= Time.deltaTime;
            return;
        }

        _countTimer = _countDelay;

        UpdateAmount();
    }

    public void SetResource(ResourceScriptable resource = null)
    {
        _resourceIcon.sprite = resource.Image;
        SetAmount(resource.Amount);

        resource.OnValueChanged += SetAmount;
    }

    private void Initialise()
    {
        _resourceIcon.sprite = _resource.Image;

        _currentAmount = _resource.Amount;
        _amountText.text = _currentAmount.ToString();

        SetAmount(_resource.Amount);
    }

    private void SetAmount(float value)
    {
        _newAmount = value;
    }

    private void UpdateAmount()
    {
        var difference = _newAmount - _currentAmount;

        var direction = difference > 0 ? 1 : -1;

        _currentAmount += direction * _currentAcceleration;

        if (Mathf.Abs(difference) <= 2)
        {
            _currentAmount = _newAmount;
            _currentAcceleration = 0;
        }

        AudioManager.PlayAudio(_receiveAudio);

        _amountText.text = _currentAmount.ToString();

        _doPunch.Punch(-direction);
    }

    private void EncreaseValue()
    {
        var difference = _newAmount - _currentAmount;

        _currentAcceleration++;

        if (_currentAcceleration <= Mathf.Abs(difference)) return;

        _currentAcceleration = Mathf.Abs(difference);
    }
}
