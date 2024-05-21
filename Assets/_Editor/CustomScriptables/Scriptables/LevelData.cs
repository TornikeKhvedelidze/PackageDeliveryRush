using System;
using UnityEngine;
using CustomInspector;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class LevelDataSaveSystem : Saveable<LevelDataSaveSystem>
{
    public bool Unlocked;
    public bool Purchased;
}

[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private LevelDataSaveSystem _data;

    [ValueDropdown(_sceneIds)]
    public string SceneId;
    public bool DefaultUnlocked;
    public bool DefaultPurchased;

    [Space]
    public ResourceScriptable OneTime_Resource;
    public float OneTime_Price = 5;
    public ResourceScriptable Buy_Resource;
    public float Buy_Price = 9999;
    public GameObject IsometricModel;
    [Header("Gameplay")]
    public int Level = 1;

    public Action OnChange;

    public bool AbleToPayOneTimePrice => OneTime_Resource.Amount > OneTime_Price;
    public bool AbleToBuy => Buy_Resource.Amount > Buy_Price;

    public bool Unlocked
    {
        get
        {
            return _data.Unlocked;
        }
        set
        {
            _data.Unlocked = value;
            OnChange?.Invoke();
        }
    }
    public bool Purchased
    {
        get
        {
            return _data.Purchased;
        }
        set
        {
            _data.Purchased = value;
            OnChange?.Invoke();
        }
    }

    private const string _sceneIds =
#if UNITY_EDITOR
        "LevelData.SceneList";
#else
        "";
#endif

    private void OnEnable()
    {
        _data.SaveId = SceneId;
        _data.Purchased = DefaultPurchased;
        _data.Unlocked = DefaultUnlocked;
        _data.Load(out var data);
        _data.Purchased = data.Purchased;
        _data.Unlocked = data.Unlocked;
        OnChange?.Invoke();
        OnChange += SaveSystemByTornike.InvokeSave;
    }

    public bool TryPayOneTimePrice()
    {
        if (!AbleToPayOneTimePrice) return false;

        OneTime_Resource.Amount -= OneTime_Price;

        return true;
    }

    public bool TryBuy()
    {
        if (!AbleToBuy) return false;

        Buy_Resource.Amount -= Buy_Price;

        Purchased = true;

        return true;
    }


#if UNITY_EDITOR
    public static string[] SceneList()
    {
        List<string> sceneNames = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
            sceneNames.Add(sceneName);
        }

        return sceneNames.ToArray();
    }
#endif
}
