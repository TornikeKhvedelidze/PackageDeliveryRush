using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

public class ScriptableSubscriber : MonoBehaviour
{
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _audio;
    [SerializeField] private ButtonScriptable _button;
    [SerializeField] private BoolButtonScriptable _endSceneButton;

    private void Start()
    {
        _button.OnPressed += Enable;
    }

    private void OnDestroy()
    {
        _button.OnPressed -= Enable;
    }

    private void Enable()
    {
        _endSceneButton.Invoke(true);
        AudioManager.PlayAudio(_audio);
    }
}
