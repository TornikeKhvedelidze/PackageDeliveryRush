using System;
using UnityEngine;
using System.Linq;
using CustomInspector;

[Serializable]
public class AudioClipData
{
    public string Name;
    public AudioClip AudioClip;
}


[CreateAssetMenu(fileName = "NewScriptable", menuName = "CreateScriptable/AudioClipsData")]
public class AudioClipsData : ScriptableObject
{

    [SerializeField] private AudioClipData[] _audioClipsData;

    public bool TryGetAudio(string name, out AudioClip audioClip)
    {
        audioClip = _audioClipsData
            .Where(x => x.Name == name)
            .Select(x => x.AudioClip)
            .FirstOrDefault();
        
        return audioClip != null;
    }

    public string[] ListAudioFileNames()
    {
        string[] namesArray = _audioClipsData
            .Select(x => x.Name)
            .ToArray();

        return namesArray;
    }

    public AudioClip[] GetAllAudioClips()
    {
        var audioClips = _audioClipsData.Select(x => x.AudioClip).ToArray();

        return audioClips;
    }
}
