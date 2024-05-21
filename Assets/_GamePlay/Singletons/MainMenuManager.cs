using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : Singleton<MainMenuManager>
{
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _changeFocusSound;

    [Header("Focus Elements")]
    [SerializeField] private IntButtonScriptable _switchFocus;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(-25, 60, -54);
    [SerializeField] private float _focusDuration = 0.2f;
    [SerializeField] private float _punchIntensity = 10f;
    [SerializeField] private Vector3 _punchDirection = Vector3.one;

    [Header("Level Elements")]
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private LevelsDataScriptable _levelsDataScriptable;
    [SerializeField] private IntButtonScriptable _sceneChangeScriptable;
    [SerializeField] private ButtonScriptable _purchaseButton;
    [SerializeField] private Transform _isometricModelsOrigin;
    [SerializeField] private Vector3 _distanceBetweenModels = new Vector3(50, 0, 0);

    [SerializeField] List<IsometricLevel> _isometricLevels;

    public static Action<LevelData> OnFocusChanged;
    public static Action OnSomethingChanged;
    public static IsometricLevel FocusedLevel { get; private set; }

    private Transform _camera;
    private int _focusIndex = 0;

    private void Start()
    {
        _switchFocus.OnPressed += SwitchFocus;
        _sceneChangeScriptable.OnPressed += OpenScene;
        _purchaseButton.OnPressed += BuyLevel;

        _isometricLevels.ForEach(x => x.LevelData.OnChange += () => OnSomethingChanged?.Invoke());

        _camera = Camera.main.transform;

        SetFocus();
    }

    private void OnDestroy()
    {
        _switchFocus.OnPressed -= SwitchFocus;
        _sceneChangeScriptable.OnPressed -= OpenScene;
        _purchaseButton.OnPressed -= BuyLevel;

        _isometricLevels.ForEach(x => x.LevelData.OnChange -= () => OnSomethingChanged?.Invoke());
    }

    private void SwitchFocus(int value)
    {
        _focusIndex += value;

        _focusIndex = _focusIndex > 0 ? _focusIndex : _isometricLevels.Count;

        SetFocus(value);

        AudioManager.PlayAudio(_changeFocusSound);
    }

    private void SetFocus(int value = 1)
    {
        var levelsCount = _isometricLevels.Count;

        var index = (_focusIndex + levelsCount) % levelsCount;

        FocusedLevel = _isometricLevels[index];

        var cameraPosition = FocusedLevel.transform.position + _cameraOffset;

        SetCameraPosition(cameraPosition, value);

        OnFocusChanged?.Invoke(FocusedLevel.LevelData);
    }

    private void SetCameraPosition(Vector3 position, int value)
    {
        var isTweening = DOTween.IsTweening(_camera);

        if (!isTweening)
        {
            _camera.DOPunchRotation(_punchDirection * _punchIntensity * value, _focusDuration * 2, 1, 1);
        }

        _camera.DOMove(position, _focusDuration);
    }

    private void OpenScene(int _)
    {
        var levelData = FocusedLevel.LevelData;

        if (!levelData.Unlocked)
        {
            NotUnlocked();
            return;
        }

        if (!levelData.Purchased && !levelData.TryPayOneTimePrice())
        {
            NotEnoughResourceForOneTime();
            return;
        }

        var levelId = FocusedLevel.LevelData.SceneId;

        _sceneController.OpenScene(levelId);
    }

    private void BuyLevel()
    {
        var levelData = FocusedLevel.LevelData;

        if (levelData.TryBuy()) return;

        NotEnoughResourceForBuy();
    }

    private void NotEnoughResourceForOneTime()
    {
        Debug.Log("Not Enough Resource For OneTime Pay");
    }
    private void NotEnoughResourceForBuy()
    {
        Debug.Log("Not Enough Resource For Buy");
    }
    private void NotUnlocked()
    {
        Debug.Log("Not Unlocked");
    }

#if UNITY_EDITOR
    public void Initialize_Editor()
    {
        var levelsData = _levelsDataScriptable.GetAllLevelsData();

        foreach (var level in _isometricLevels)
        {
            if (level == null) continue;
            DestroyImmediate(level.gameObject);
        }

        _isometricLevels.Clear();

        for (int i = 0; i < levelsData.Length; i++)
        {
            var levelData = levelsData[i];

            var isometricLevelGameobject = PrefabUtility.InstantiatePrefab(levelData.IsometricModel) as GameObject;
            Undo.RegisterCreatedObjectUndo(isometricLevelGameobject, "Place Prefab");

            var isometricLevelTransfomr = isometricLevelGameobject.transform;

            var position = _isometricModelsOrigin.position + _distanceBetweenModels * i;

            isometricLevelTransfomr.position = position;
            isometricLevelTransfomr.rotation = _isometricModelsOrigin.rotation;

            var isometciLevel = isometricLevelGameobject.GetComponent<IsometricLevel>();

            isometciLevel.Initialize(levelData);

            _isometricLevels.Add(isometciLevel);
        }
    }
#endif
}
