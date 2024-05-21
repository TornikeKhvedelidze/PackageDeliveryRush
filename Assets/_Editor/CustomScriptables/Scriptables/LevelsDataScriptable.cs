using System;
using UnityEngine;
using CustomInspector;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/LevelsData")]
public class LevelsDataScriptable : ScriptableObject
{
    [SerializeField] private LevelData[] _levelsData;

    public LevelData GetLevelDataByName(string name)
    {
        var levelData = _levelsData.Where(x => x.SceneId == name).FirstOrDefault();

        return levelData;
    }

    public LevelData[] GetAllLevelsData()
    {
        return _levelsData;
    }
}
