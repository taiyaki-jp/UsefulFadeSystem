using UnityEngine;
using UnityEngine.UI;

public class RGBColorPicker : MonoBehaviour
{
    [Header("スライダー")]
    [SerializeField]private Slider _rSlider;
    [SerializeField]private Slider _gSlider;
    [SerializeField]private Slider _bSlider;
    [Header("チェックボックス")]
    [SerializeField]private Toggle _isSkip;
    [Header("Image")]
    [SerializeField]private Image _targetImage;

    private Color _color;
    public Color UseColor { get => _color; }

    void Start()
    {
        // スライダーの範囲を0〜255に設定
        _rSlider.minValue = 0; _rSlider.maxValue = 255;
        _gSlider.minValue = 0; _gSlider.maxValue = 255;
        _bSlider.minValue = 0; _bSlider.maxValue = 255;

        // 値変更イベントを登録
        _rSlider.onValueChanged.AddListener(_ => UpdateColor());
        _gSlider.onValueChanged.AddListener(_ => UpdateColor());
        _bSlider.onValueChanged.AddListener(_ => UpdateColor());

        // 初期色を反映
        UpdateColor();

        if (_isSkip == null) return;
        _isSkip.isOn = true;
        _isSkip.onValueChanged.AddListener(Boolchenged);
        Boolchenged(true);
    }

    private void Boolchenged(bool condition)
    {
        if (condition)
        {
            _rSlider.interactable = false;
            _gSlider.interactable = false;
            _bSlider.interactable = false;
            _color = default;
        }
        else
        {
            _rSlider.interactable= true;
            _gSlider.interactable= true;
            _bSlider.interactable= true;
        }
    }

    void UpdateColor()
    {
        float r = _rSlider.value / 255f;
        float g = _gSlider.value / 255f;
        float b = _bSlider.value / 255f;

        _color = new Color(r, g, b, 1f);

        if (_targetImage != null) _targetImage.color = _color;
        
    }
}