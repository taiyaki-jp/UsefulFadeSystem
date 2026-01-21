using System;
using System.Collections.Generic;
using System.Linq;
using FadeOrigins;
using UnityEngine;
using UnityEngine.UI;

public class MethodSetter : MonoBehaviour
{
    [SerializeField] private Dropdown _originDropdown;
    [SerializeField] private bool _canSkip;
    private Dropdown _thisDropdown;
    private OriginSetter _originsetter;

    private void Awake()
    {
        _thisDropdown = GetComponent<Dropdown>();
        _originsetter = _originDropdown.gameObject.GetComponent<OriginSetter>();

        _thisDropdown.ClearOptions();
        List<string> methodList = new List<string>();

        if (_canSkip) methodList.Add("Skip");
        else methodList.Add("None");

        var methods = Enum.GetValues(typeof(Image.FillMethod)).Cast<Image.FillMethod>().ToList();
        foreach (var t in methods)
        {
            methodList.Add(t.ToString());
        }

        _thisDropdown.AddOptions(methodList);
    }

    private void Start()
    {
        OnValueChange(0);
        _thisDropdown.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(int value)
    {
        _originDropdown.ClearOptions();
        _originsetter.UsingMethod = value;

        if (value == 0)
        {
            _originDropdown.interactable = false;
            return;
        }

        _originDropdown.interactable = true;
        var originList = new List<string>();
        switch (value)
        {
            case 1:
            {
                var origin = Enum.GetValues(typeof(Horizontal)).Cast<Horizontal>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 2:
            {
                var origin = Enum.GetValues(typeof(Vertical)).Cast<Vertical>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 3:
            {
                var origin = Enum.GetValues(typeof(Radial_90)).Cast<Radial_90>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 4:
            {
                var origin = Enum.GetValues(typeof(Radial_180)).Cast<Radial_180>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
            case 5:
            {
                var origin = Enum.GetValues(typeof(Radial_360)).Cast<Radial_360>().ToList();
                foreach (var t in origin)
                {
                    originList.Add(t.ToString());
                }

                break;
            }
        }

        _originDropdown.AddOptions(originList);
    }
}