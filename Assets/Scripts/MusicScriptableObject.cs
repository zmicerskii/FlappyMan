using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AudioStore")]
public class MusicScriptableObject : ScriptableObject
{
    [SerializeField, ArrayElementTitle("audioType")] private AudioField[] audioField;
    public AudioClip GetAudioClipByType(AudioType audioType)
    {
        return audioField.First(x => x.audioType == audioType).audioClip;
    }
}
public enum AudioType
{
    Flap,
    BirdDied,
    Scored
}

[Serializable]
public class AudioField
{
    public AudioType audioType;
    public AudioClip audioClip;
}
