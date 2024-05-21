using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField] private ButtonScriptable _buttonScriptable;

    private void OnTriggerEnter(Collider other)
    {
        _buttonScriptable.Invoke();
    }
}
