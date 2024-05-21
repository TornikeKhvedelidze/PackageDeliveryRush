using RDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : Singleton<VibrationManager>
{
    [SerializeField] private VibrationLevelsScriptable _vibrationLevels;


    public const string VibrationLevelIdsQuery =
#if UNITY_EDITOR
        "VibrationManager.ListVibrationLevels";
#else
"";
#endif

    void Start()
    {
        DontDestroyOnLoad(this);
    }


    public static string[] ListVibrationLevels()
    {
        return Instance._vibrationLevels.ListVibrationLevels();
    }
    public static void PlayVibration(string name)
    {
        var vibrationDuration = Instance._vibrationLevels.GetVibrationDuration(name);
        PlayVibration(vibrationDuration);
    }


    private static void PlayVibration(long duration)
    {
        Vibration.Vibrate(duration, -1, false);
    }
}
