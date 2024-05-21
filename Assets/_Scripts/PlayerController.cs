using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _borderPoint = 4f;
    [SerializeField] private float _rotationIntensity = 25;
    [SerializeField] private float _rotationSpeed = 0.3f;
    [SerializeField] private float _moveSpeed = 0.3f;
    [SerializeField] private InputDragProvider _inputDragProvider;

    private Vector3 _desiredPosition;

    void Start()
    {
        _inputDragProvider.OnDragging += Move;

        _desiredPosition = transform.localPosition;
    }

    private void Move(float value)
    {
        var intensity = 1 - Mathf.Abs(_desiredPosition.x + value) / _borderPoint;
        intensity = Mathf.Clamp(intensity, 0.1f, 1);

        _desiredPosition.x += value * _borderPoint * intensity;
        _desiredPosition.x = Mathf.Clamp(_desiredPosition.x, -_borderPoint, _borderPoint);

        transform.DOLocalMove(_desiredPosition, _moveSpeed);

        var currentRotation = transform.localEulerAngles;
        currentRotation.y = value * _rotationIntensity * intensity;

        transform.DORotate(currentRotation, _rotationSpeed);
    }
}
