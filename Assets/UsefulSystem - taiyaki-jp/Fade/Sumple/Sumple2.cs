using System;
using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;
public class Sumple2 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField] private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        //これでフェードマネージャーを取れる
        _fadeManager = FadeManager.Instance;

        _button.onClick.AddListener(() =>
            //↓このように呼び出す
            _ = _fadeManager.FadeAndSceneChange<Enum>("SumpleScene1")
        );
    }
}