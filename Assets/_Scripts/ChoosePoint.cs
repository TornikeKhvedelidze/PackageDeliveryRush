using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePoint : MonoBehaviour
{
    [SerializeField] private IntButtonScriptable _buttonScriptable;

    public void ChangePoint(int value)
    {
        _buttonScriptable.Invoke(value);
    }
}
