using System;
using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;
public class Sumple1 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField] private Button _button;

    [Header("Origin")]
    [SerializeField] private OriginSetter _startOrigin;
    [SerializeField] private OriginSetter _endOrigin;

    [Header("Color")]
    [SerializeField] private RGBColorPicker _startColor;
    [SerializeField] private RGBColorPicker _midColor;
    [SerializeField] private RGBColorPicker _mid2Color;
    [SerializeField] private RGBColorPicker _endColor;

    // Start is called before the first frame update
    void Start()
    {
        //これでフェードマネージャーを取れる
        _fadeManager = FadeManager.Instance;

        _button.onClick.AddListener(() =>
            //↓このように呼び出す
            _ = _fadeManager.FadeAndSceneChange<Enum>("SumpleScene2",
                startOrigin: _startOrigin.UseOrigin,//FillOriginEnum.cs参照
                endOrigin: _endOrigin.UseOrigin,//FillOriginEnum.cs参照
                startColor: _startColor.UseColor,
                midColor: _midColor.UseColor,
                midColor2: _mid2Color.UseColor,
                endColor: _endColor.UseColor
            )
        );
        _ = SoundManager.Instance.Init(15);
    }
}