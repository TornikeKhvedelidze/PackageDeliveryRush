using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMaker : MonoBehaviour
{
    [SerializeField] private Choice[] _choices = new Choice[2];

    private ChoiceScriptable[] _choceScriptables;

    private void Start()
    {
        _choceScriptables = DecisionManager.GetChoices();

        var flip = 50 > Random.Range(0, 100);

        if (flip)
        {
            System.Array.Reverse(_choceScriptables);
        }

        for (int i = 0; i < _choices.Length; i++)
        {
            _choices[i].SetChoice(_choceScriptables[i]);
        }
    }



}
