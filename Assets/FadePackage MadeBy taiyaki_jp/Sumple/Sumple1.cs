using System;
using UnityEngine;
using UnityEngine.UI;

public class Sumple1 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField]private Button _button;
    [SerializeField]private Color _startColor;
    [SerializeField]private Color _midColor;
    [SerializeField]private Color _mid2Color;
    [SerializeField]private Color _endColor;
    // Start is called before the first frame update
    void Start()
    {
        _fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();

        _button.onClick.AddListener(()=>_fadeManager.Fade<Enum>("SumpleScene2",startOrigin:Horizontal.Left,endOrigin:Vertical.Top,startColor:_startColor,midColor:_midColor,midColor2:_mid2Color,endColor:_endColor));
    }
}
