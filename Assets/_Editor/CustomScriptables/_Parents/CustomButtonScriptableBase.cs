using System;
using UnityEngine;

public class CustomButtonScriptableBase<T> : ScriptableObject
{
    public Action<T> OnPressed;

    public void Invoke(T value)
    {
        OnPressed?.Invoke(value);
    }
}
