using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/Button")]
public class ButtonScriptable : ScriptableObject
{
    public Action OnPressed;

    public void Invoke()
    {
        OnPressed?.Invoke();
    }
}
