using System;
using UnityEngine;

public class Sumple : MonoBehaviour
{
    private FadeManager _fadeManager;
    // Start is called before the first frame update
    void Start()
    {
        _fadeManager =GameObject.Find("FadeManager").GetComponent<FadeManager>();


        _ = _fadeManager.Fade<Enum>("A-Test2", Radial_360_Origin.Right, VerticalOrigin.Top);
    }
}
