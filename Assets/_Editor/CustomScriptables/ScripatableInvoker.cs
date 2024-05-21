using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

public class ScripatableInvoker : MonoBehaviour
{
    [SerializeField] private bool _Empty;
    [SerializeField, HideIf("_Empty", false)] private ButtonScriptable _ButtonScriptable;
    [SerializeField] private bool _int;
    [SerializeField, HideIf("_int", false)] private IntButtonScriptable _intButtonScriptable;
    [SerializeField] private bool _float;
    [SerializeField, HideIf("_float", false)] private FloatButtonScriptable _floatButtonScriptable;
    [SerializeField] private bool _bool;
    [SerializeField, HideIf("_bool", false)] private BoolButtonScriptable _boolButtonScriptable;


    public void Press()
    {
        if (!_Empty)
        {
            LogError();
            return;
        }

        _ButtonScriptable.Invoke();
    }
    public void Press(int value)
    {
        if (!_int)
        {
            LogError();
            return;
        }

        _intButtonScriptable.Invoke(value);
    }
    public void Press(float value)
    {
        if (!_float)
        {
            LogError();
            return;
        }

        _floatButtonScriptable.Invoke(value);
    }
    public void Press(bool value)
    {
        if (!_bool)
        {
            LogError();
            return;
        }

        _boolButtonScriptable.Invoke(value);
    }

    private void LogError()
    {
        Debug.LogError($"YOU CALLED THE WRONG SCRIPTABLE IN {name}", this);
    }

}
