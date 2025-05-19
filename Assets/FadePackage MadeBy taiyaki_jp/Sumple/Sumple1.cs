using System;
using UnityEngine;
using UnityEngine.UI;

public class Sumple1 : MonoBehaviour
{
    private FadeManager _fadeManager;
    [SerializeField]private Button _button;
    // Start is called before the first frame update
    void Start()
    {
        _fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();

        _button.onClick.AddListener(()=>_ = _fadeManager.Fade<Enum>("SumpleScene2", Radial_360.Right, Vertical.Top));
    }
}
