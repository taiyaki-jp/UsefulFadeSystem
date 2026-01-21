using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BGMData",fileName = "Assets/Resources/BGMData")]
public class ScriptableBGMDatas : ScriptableObject
{
    public List<BGMData> bgmDatas;
}
[Serializable]
public class BGMData
{
    [SerializeField] public string bgmName;
    [SerializeField] public AudioClip audioClip;
    [SerializeField, Range(0, 1)] public float volume;
}
