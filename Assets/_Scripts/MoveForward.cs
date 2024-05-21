using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private InputDragProvider _dragProvider;
    [SerializeField] private GameObject _disableObjectOnFirstMove;
    private bool _isDragged;

    private void Start()
    {
        _dragProvider.OnDragging += Move;

    }

    private void Move(float _)
    {
        transform.position += Vector3.forward * _moveSpeed * Time.deltaTime * 10;

        if (_isDragged) return;

        _isDragged = true;

        _disableObjectOnFirstMove.SetActive(false);
    }
}
