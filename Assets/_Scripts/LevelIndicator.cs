using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIndicator : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private TMP_Text _text;

    void Start()
    {
        _text.text = $"LEVEL {_levelData.Level}";
    }

}
