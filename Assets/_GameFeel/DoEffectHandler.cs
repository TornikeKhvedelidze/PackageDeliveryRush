using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DoEffectHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _punchDirection = Vector3.up;
    [SerializeField] private float _duration;

    public void DoEffect()
    {
        transform.DOPunchScale(_punchDirection, _duration, 1, 1);
    }
}
