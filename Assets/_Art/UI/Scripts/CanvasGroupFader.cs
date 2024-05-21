using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private bool _activeOnStart;
    [SerializeField] private bool _fadeInOnStart;

    private void Start()
    {
        _canvasGroup.gameObject.SetActive(_activeOnStart);

        SetFade(_fadeInOnStart);
    }

    public void SetFade(bool value, float duration = 0)
    {
        var alpha = value ? 1.0f : 0.0f;

        if(duration <= 0)
        {
            duration = _fadeDuration;
        }

        _canvasGroup.DOFade(alpha, duration);

        if (value)
        {
            _canvasGroup.gameObject.SetActive(true);
            return;
        }

        if (!_canvasGroup.gameObject.activeSelf) return;

        StartCoroutine(Disable_Coroutine(duration));
    }

    IEnumerator Disable_Coroutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        _canvasGroup.gameObject.SetActive(false);
    }
}
