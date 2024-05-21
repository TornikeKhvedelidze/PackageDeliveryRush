using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection = Vector3.up;
    [SerializeField] private float _moveIntensity = 1;
    [SerializeField] private float _moveDuration = 2f;

    void Start()
    {
        Vector3 startPos = transform.position;

        var moveDirection = _moveDirection * _moveIntensity;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(startPos + moveDirection, _moveDuration).SetEase(Ease.InOutQuad));
        sequence.Append(transform.DOMove(startPos, _moveDuration).SetEase(Ease.InOutQuad));

        sequence.SetLoops(-1, LoopType.Restart);
    }
}
