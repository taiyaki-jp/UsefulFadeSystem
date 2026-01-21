using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SEData",fileName = "Assets/Resources/SEData")]
public class ScriptableSEDatas : ScriptableObject
{
    [SerializeField] public List<SEData> seDatas;
}

[Serializable]
public class SEData
{
    [SerializeField] public string seName;
    [SerializeField] public AudioClip audioClip;
    [SerializeField, Range(0, 1)] public float volume ;
}