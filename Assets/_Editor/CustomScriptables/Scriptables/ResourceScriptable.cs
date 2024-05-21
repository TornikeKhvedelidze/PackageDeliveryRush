using System;
using UnityEngine;

[Serializable]
public class ResourceSaveSystem : Saveable<ResourceSaveSystem>
{
    public float Amount;
}

[CreateAssetMenu(fileName = "NewWalkData", menuName = "CreateScriptable/Resource")]
public class ResourceScriptable : ScriptableObject
{
    [SerializeField] private ResourceSaveSystem _resourceSaveSystem;

    public string Name;
    public float Amount
    {
        set
        {
            _resourceSaveSystem.Amount = value; 
            OnValueChanged?.Invoke(Amount);
        }
        get
        {
            return _resourceSaveSystem.Amount;
        }
    }
    public float DefaultAmount = 0;
    public Sprite Image;
    public Action<float> OnValueChanged;

    private void OnEnable()
    {
        _resourceSaveSystem.SaveId = Name;
        Amount = DefaultAmount;
        _resourceSaveSystem.Load(out var data);
        Amount = data.Amount;
        OnValueChanged?.Invoke(Amount);
    }
}
