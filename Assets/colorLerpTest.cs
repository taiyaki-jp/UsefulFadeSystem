using UnityEngine;
using UnityEngine.UI;

public class colorLerpTest : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    private float _t=0;
    Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _t = 0;
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _t += Time.deltaTime;
        _image.color = Color.Lerp(_startColor, _endColor, _t);

    }
}
