using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS : MonoBehaviour
{
    [SerializeField] TMP_Text _fpsText;
    float deltaTime = 0.0f;
    private float fps = 0.0f;

    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
    }

    private void LateUpdate()
    {
        _fpsText.text = string.Format("FPS: {0:0.}", fps);
    }
}
