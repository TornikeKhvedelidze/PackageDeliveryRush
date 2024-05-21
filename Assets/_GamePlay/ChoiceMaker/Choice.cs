using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Choice : MonoBehaviour
{
    [SerializeField] private string _choiceId;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _image;
    [SerializeField] private Renderer _renderer;

    public void SetChoice(ChoiceScriptable choiceScriptable)
    {
        var Color = choiceScriptable.Material.color;
        Color.a = 1;

        _name.text = choiceScriptable.Name;
        _name.color = Color;
        _renderer.material = choiceScriptable.Material;
        _image.sprite = choiceScriptable.Image;
        _image.color = Color;

        _choiceId = choiceScriptable.Name;
    }

    private void OnTriggerEnter(Collider other)
    {
        DecisionManager.MakeChoice(_choiceId);
    }
}
