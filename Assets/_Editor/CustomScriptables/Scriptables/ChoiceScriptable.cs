using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/Choice")]
public class ChoiceScriptable : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public Material Material;
    [Space, Header("Reward")]
    public ResourceScriptable resource;
    public float RewardAmount;

    public void Reward()
    {
        resource.Amount += RewardAmount;
    }
}
