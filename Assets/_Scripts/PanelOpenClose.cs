using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class PanelOpenClose : MonoBehaviour
{
    [SerializeField] private BoolButtonScriptable _buttonScriptable;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _elementsParent;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private bool _openOnAwake;

    private void Start()
    {
        _buttonScriptable.OnPressed += SetPanel;
        _canvasGroup.alpha = 0;

        SetPanel(_openOnAwake);
    }

    private void OnDestroy()
    {
        _buttonScriptable.OnPressed -= SetPanel;
    }

    private void SetPanel(bool value)
    {

        _canvasGroup.DOFade(value ? 1f : 0f, _fadeDuration);

        StartCoroutine(SetPanelElements_Coroutine(value));

        if (!value) return;

        _elementsParent.SetActive(true);
    }

    IEnumerator SetPanelElements_Coroutine(bool value)
    {
        yield return new WaitForSeconds(_fadeDuration);

        SetPanelElements(value);
    }

    private void SetPanelElements(bool value)
    {
        _elementsParent.SetActive(value);
    }
}
