using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;

[Serializable]
public class UI_DoMoveClass
{
    public RectTransform Target;
    public Vector3 StartRectPosition;
    public Vector3 EndRectPosition;
    public float Duration = 0.3f;

    public void DoEffect(bool value)
    {
        var desiredPosition = value ? StartRectPosition : EndRectPosition;

        Target.DOAnchorPos(desiredPosition, Duration);
    }
}

[Serializable]
public class UI_DoScaleClass
{
    public RectTransform Target;
    public Vector3 StartScale;
    public Vector3 EndScale;
    public float Duration = 0.3f;

    public void DoEffect(bool value)
    {
        var desiredPosition = value ? StartScale : EndScale;

        Target.DOScale(desiredPosition, Duration);
    }
}

[Serializable]
public class UI_DoPunchMoveClass
{
    public RectTransform Target;
    public Vector3 PunchDirection = Vector3.up;
    public float Inetnsity = 0.3f;
    public float Duration = 0.3f;

    public void DoEffect()
    {
        if (DOTween.IsTweening(Target))
        {
            Debug.LogError($"That wat too fast for {Target.name}", Target);
            return;
        }

        Target.DOPunchAnchorPos(PunchDirection * Inetnsity, Duration, 1, 1);
    }
}

[Serializable]
public class UI_DoPunchScaleClass
{
    public RectTransform Target;
    public Vector3 PunchDirection = Vector3.one;
    public float Intensity = 1;
    public float Duration = 0.3f;
    public float Delay = 0.1f;

    public void DoEffect()
    {
        if (DOTween.IsTweening(Target))
        {
            return;
        }

        Target.DOPunchScale(PunchDirection * Intensity, Duration, 1, 1);
    }
}

public class EndScenePanel : MonoBehaviour
{
    [SerializeField] private BoolButtonScriptable _buttonScriptable;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private List<UI_DoMoveClass> _doMover;
    [SerializeField] private List<UI_DoPunchMoveClass> _doPunchMover;
    [SerializeField] private List<UI_DoPunchScaleClass> _doPunchScaler;

    [SerializeField] private float _fadeduration = 0.3f;

    private void Start()
    {
        _buttonScriptable.OnPressed += DoEffect;

        DoEffect(false);
    }

    private void OnDestroy()
    {
        _buttonScriptable.OnPressed -= DoEffect;
    }

    public void DoEffect(bool value)
    {
        _doMover.ForEach(x => x.DoEffect(value));
        _doPunchMover.ForEach(x => x.DoEffect());
        _doPunchScaler.ForEach(x => StartCoroutine(DoEffect_Coroutine(x)));

        _canvasGroup.DOFade(value ? 1 : 0, _fadeduration);
        StartCoroutine(Enable_Coroutine(value));
    }

    IEnumerator DoEffect_Coroutine(UI_DoPunchScaleClass scaler)
    {
        if(scaler.Delay <= 0)
        {
            scaler.DoEffect();

            yield break;
        }

        yield return new WaitForSeconds(scaler.Delay);

        scaler.DoEffect();
    }

    IEnumerator Enable_Coroutine(bool value)
    {
        if (value)
        {
            _canvasGroup.gameObject.SetActive(value);
            yield break;
        }

        yield return new WaitForSeconds(_fadeduration);

        _canvasGroup.gameObject.SetActive(value);
    }
}
