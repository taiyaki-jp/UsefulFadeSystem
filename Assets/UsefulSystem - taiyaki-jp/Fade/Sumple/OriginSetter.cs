using System;
using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;

public class OriginSetter : MonoBehaviour
{
    private Dropdown _thisDropdown;
    private int _usingMethod;
    public int UsingMethod {
        set
        {
            _usingMethod = value;
            OnValueChanged(0);
        }
    }
    private Enum _usingOrigin;
    public Enum UseOrigin { get => _usingOrigin;}

    private void Awake()
    {
        _thisDropdown = GetComponent<Dropdown>();
        _thisDropdown.onValueChanged.AddListener(OnValueChanged);
    }


    private void OnValueChanged(int value)
    {
        switch (_usingMethod)
        {
            case 1:
                _usingOrigin = (Horizontal)value;
                break;
            case 2:
                _usingOrigin = (Vertical)value;
                break;
            case 3:
                _usingOrigin = (Radial_90)value;
                break;
            case 4:
                _usingOrigin = (Radial_180)value;
                break;
            case 5:
                _usingOrigin = (Radial_360)value;
                break;
            default:
                _usingOrigin = default;
                break;
        }
        if(_usingOrigin!=default) Debug.Log(_usingOrigin.ToString());
        else Debug.Log("Origin not set");
    }
}