using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class BibrationLevelData
{
    public string Name;
    public long Duration;
}

[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/VibrationLevelsData")]
public class VibrationLevelsScriptable : ScriptableObject
{
    [SerializeField] private BibrationLevelData[] _vibrationLevelsData;

    public long GetVibrationDuration(string name)
    {
        var duration = _vibrationLevelsData
            .Where(x => x.Name == name)
            .Select(x => x.Duration)
            .FirstOrDefault();

        return duration;
    }

    public string[] ListVibrationLevels()
    {
        string[] namesArray = _vibrationLevelsData
            .Select(x => x.Name)
            .ToArray();

        return namesArray;
    }
}
