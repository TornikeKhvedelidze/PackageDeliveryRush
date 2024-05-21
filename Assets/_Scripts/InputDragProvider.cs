using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputDragProvider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ButtonScriptable _stopScriptable; 
    [SerializeField] private float _sensitivity = 4f;

    public Action<float> OnDragging;
    public Action<bool> OnIsDragging;

    private float savedX;
    private bool _pressed;

    private void Update()
    {
        if (!_pressed) return;

        var currcentX = Input.mousePosition.x;

        var difference = currcentX - savedX;

        var normaisedDifference = difference / Screen.width;

        OnDragging?.Invoke(normaisedDifference * _sensitivity);

        savedX = currcentX;

        if (_stopScriptable == null) return;

        _stopScriptable.OnPressed += StopMoving;
    }

    private void OnDestroy()
    {
        if (_stopScriptable == null) return;

        _stopScriptable.OnPressed -= StopMoving;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        savedX = eventData.position.x;
        _pressed = true;
        OnIsDragging?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
        OnIsDragging?.Invoke(false);
    }

    private void StopMoving()
    {
        _pressed = false;
        OnIsDragging?.Invoke(false);
    }
}
