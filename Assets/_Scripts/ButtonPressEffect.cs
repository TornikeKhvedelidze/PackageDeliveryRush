using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using CustomInspector;
using RDG;

public class ButtonPressEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _pointDownAudio;
    
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _pointUpAudio;

    [SerializeField, ValueDropdown(VibrationManager.VibrationLevelIdsQuery)]
    private string _vibrationType;

    public float hapticDuration = 0.2f;

    [SerializeField] private float _scaleIntensity = 0.1f;
    [SerializeField] private float _punchIntensity = 0.3f;
    [SerializeField] private float _effectDuration = 0.1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(1 - _scaleIntensity, _effectDuration);

        AudioManager.PlayAudio(_pointDownAudio);

        VibrationManager.PlayVibration(_vibrationType);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1, _effectDuration);

        AudioManager.PlayAudio(_pointUpAudio);
    }

    public void Punch(int direction)
    {
        var isTweening = DOTween.IsTweening(transform);

        if (isTweening) return;

        transform.DOPunchScale(direction * -_punchIntensity * Vector3.one, _effectDuration, 1, 1);
    }
}
