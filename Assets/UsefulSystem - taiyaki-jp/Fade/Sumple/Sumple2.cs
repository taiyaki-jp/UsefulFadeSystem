using System;
using UnityEngine;
using UnityEngine.UI;
public class Sumple2 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField] private Button _button;
    [SerializeField] private Button _endButton;

    // Start is called before the first frame update
    private void Start()
    {
        //これでフェードマネージャーを取れる
        _fadeManager = FadeManager.Instance;

        _button.onClick.AddListener(() =>
            //↓このように呼び出す
            _ = _fadeManager.FadeAndSceneChange<Enum>("SumpleScene1")
        );
        _endButton.onClick.AddListener(()=> _ = _fadeManager.GameEndFade());
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        _endButton.onClick.RemoveAllListeners();
    }
}