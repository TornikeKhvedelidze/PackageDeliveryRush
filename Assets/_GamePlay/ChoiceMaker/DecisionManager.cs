using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DecisionManager : Singleton<DecisionManager>
{
    [SerializeField] ChoiceScriptable[] _choices;

    public static ChoiceScriptable[] GetChoices()
    {
        return Instance._choices;
    }

    public static void MakeChoice(string choiceId)
    {
        var choice = FindChoiveScriptable(choiceId);

        choice.Reward();
    }

    private static ChoiceScriptable FindChoiveScriptable(string Id)
    {
        var choice = Instance._choices.Where(x => x.Name == Id).FirstOrDefault();

        return choice;
    }

    public const string PlayableIdsQuery =
#if UNITY_EDITOR
        "DecisionManager.GetPlayablePooledObjectIds";
#else
"";
#endif

    public static string[] GetPlayablePooledObjectIds()
    {
        var choices = Instance._choices.Select(x => x.name).ToArray();

        return choices;
    }
}
