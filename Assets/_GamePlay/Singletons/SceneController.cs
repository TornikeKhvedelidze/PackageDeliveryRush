using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using CustomInspector;

public class SceneController : Singleton<SceneController>
{
    [SerializeField, ValueDropdown(AudioManager.AudioIdsQuery)]
    private string _openSound;
    [SerializeField] private bool _openSceneWithScriptable = true;
    [SerializeField, HideIf("_openSceneWithScriptable", false)] private IntButtonScriptable _buttonScriptable;
    [SerializeField] private CanvasGroupFader _canvasGroupFader;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _sceneEndDuration = 1f;

    private Camera _camera;

    protected override void Awake()
    {
        base.Awake();

        float deviceRefreshRate = Screen.currentResolution.refreshRate;

        StartCoroutine(SetFPS_Coroutine(Mathf.RoundToInt(deviceRefreshRate)));

        Debug.Log("Refresh Rate Ratio: " + deviceRefreshRate);

        _camera = Camera.main;

        if (!_openSceneWithScriptable) return;

        _buttonScriptable.OnPressed += OpenScene; 
    }

    private void OnDestroy()
    {
        _buttonScriptable.OnPressed -= OpenScene;
    }

    private void Update()
    {
        Debug.Log(Application.targetFrameRate);
    }

    public static void Restart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OpenScene(string name)
    {
        AudioManager.PlayAudio(_openSound);

        CloseScene();

        StartCoroutine(OpenScene_Coroutine(-1, name));
    }

    public void OpenScene(int id)
    {
        Debug.Log("called");

        CloseScene();

        StartCoroutine(OpenScene_Coroutine(id));
    }

    private void CloseScene()
    {
        _camera.DOOrthoSize(1, _sceneEndDuration);
        _canvasGroupFader.SetFade(true, _sceneEndDuration);
        _audioSource.Play();
    }

    IEnumerator OpenScene_Coroutine(int id, string name = "")
    {
        Debug.Log("Star Coroutine");
        if (_sceneEndDuration > 0)
        {
            yield return new WaitForSeconds(_sceneEndDuration);
        }

        Debug.Log("End Coroutine");
        SaveSystemByTornike.InvokeSave();

        if(id < 0)
        {
            SceneManager.LoadScene(name);
            yield break;
        }

        SceneManager.LoadScene(id);
    }

    IEnumerator SetFPS_Coroutine(int value)
    {

            yield return new WaitForEndOfFrame();

            Application.targetFrameRate = value;
            Debug.Log("Refresh Rate Ratio: " + value);
    }
}
