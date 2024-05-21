using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class IsometricLevel : MonoBehaviour
{
    [SerializeField] private Transform _levelPanel;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private float _levelPanelOpenDuration = 0.3f;
    

    public LevelData LevelData;

    public void Start()
    {
        SetLevelPanel(LevelData.Purchased);

        _levelText.text = $"LEVEL {LevelData.Level}";

        LevelData.OnChange += () => SetLevelPanel(LevelData.Purchased);
    }

    private void SetLevelPanel(bool value)
    {
        _levelPanel.DOScale(value ? 1 : 0, _levelPanelOpenDuration);

        _levelText.text = $"LEVEL {LevelData.Level}";
    }


#if UNITY_EDITOR
    public void Initialize(LevelData levelData)
    {
        LevelData = levelData;
    }
#endif
}
