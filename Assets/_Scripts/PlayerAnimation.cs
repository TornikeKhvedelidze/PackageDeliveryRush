using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;
    [SerializeField] private InputDragProvider _inputDragProvider;

    private void Start()
    {
        _inputDragProvider.OnIsDragging += SetAnimation;
    }

    private void OnDestroy()
    {
        _inputDragProvider.OnIsDragging -= SetAnimation;
    }

    private void SetAnimation(bool value)
    {
        _animator.SetBool(_animationName, value);
    }
}
