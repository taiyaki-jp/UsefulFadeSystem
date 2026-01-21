using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/JingleData",fileName = "Assets/Resources/JingleData")]
public class ScriptableJingleDatas : ScriptableObject
{
    [SerializeField] public List<JingleData> jingleDatas;
}

[Serializable]
public class JingleData
{
    [SerializeField] public string jingleName;
    [SerializeField] public AudioClip audioClip;
    [SerializeField, Range(0, 1)] public float volume;
}
