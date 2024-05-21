using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

[Serializable]
public class ColorTypes
{
    public Color Active;
    public Color ActiveDark;
    public Color InActive;
}

public class MainMenuController_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text _MainText;
    [Header("ExtraPay")]
    [SerializeField] private GameObject _PanelPay;
    [SerializeField] private TMP_Text _PayText;
    [SerializeField] private RectTransform _PayTextTransfrom;
    [SerializeField] private Image _PayImage;
    [SerializeField] private Image _ButtonImage;
    [Header("Purchase")]
    [SerializeField] private TMP_Text _PurchaseText;
    [SerializeField] private Image _PurchaseResource;
    [SerializeField] private Transform _purchaseButton;
    [Header("Experience")]
    [SerializeField] private float _animateDuration = 0.3f;
    [SerializeField] private ColorTypes _colorTypes;

    private LevelData _currentLevelData;

    void Start()
    {
        var data = MainMenuManager.FocusedLevel;

        if(data != null)
        {
            InitialiseData(data.LevelData);
        }


        MainMenuManager.OnFocusChanged += InitialiseData;

        MainMenuManager.OnSomethingChanged += Refresh;
    }

    private void OnDestroy()
    {
        MainMenuManager.OnFocusChanged -= InitialiseData;

        MainMenuManager.OnSomethingChanged -= Refresh;
    }

    private void Refresh()
    {
        InitialiseData(_currentLevelData);
    }

    private void InitialiseData(LevelData levelData)
    {
        _currentLevelData = levelData;

        var unlokced = levelData.Unlocked;
        var purchased = levelData.Purchased;

        if (!unlokced)
        {
            _MainText.text = "LOCKED";

            SetExtraPrice(false);
            SetColor(_colorTypes.InActive);

            return;
        }

        if (!purchased)
        {
            _MainText.text = "PAY TO PLAY";

            SetExtraPrice(true, levelData);
            SetColor(_colorTypes.ActiveDark);
            return;
        }

        SetExtraPrice(false);
        SetColor(_colorTypes.Active);

        _MainText.text = "PLAY";
    }

    private void SetExtraPrice(bool value, LevelData levelData = null)
    {
        var scaleY = value ? 1 : 0;

        _PayTextTransfrom.DOScaleY(scaleY, _animateDuration);
        _purchaseButton.DOScale(value ? 1 : 0, _animateDuration);

        if (levelData == null || !value) return;

        _PurchaseText.text = levelData.Buy_Price.ToString();
        _PurchaseResource.sprite = levelData.Buy_Resource.Image;

        _PayText.text = levelData.OneTime_Price.ToString();
        _PayImage.sprite = levelData.OneTime_Resource.Image;
    }

    private void SetColor(Color color)
    {
        _ButtonImage.DOColor(color, _animateDuration);
    }
}
