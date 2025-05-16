using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeAndLoad 
{
    private float _fadeSpeed=1;
    public float Speed
    {
        set { _fadeSpeed = value; }
    }
    private Image _fadeImage;
    public Image Image
    {
        set { _fadeImage = value; }
    }

    /// <summary>
    /// FillAmount式フェードイン
    /// </summary>
    /// <typeparam name="TOriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    public async UniTask FadeIn<TOriginEnum>(TOriginEnum origin) where TOriginEnum : Enum
    {
        float fillAmount = 0;
        _fadeImage.fillAmount = fillAmount;

        _fadeImage.fillMethod=AutoMethodSet(origin);
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount < 1)
        {
            fillAmount += Time.deltaTime;
            _fadeImage.fillAmount= fillAmount;
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// FillAmount式フェードアウト
    /// </summary>
    /// <typeparam name="TOriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="origin">methodで指定したものに対応するFillOriginEnumを使う</param>
    public async UniTask FadeOut<TOriginEnum>(TOriginEnum origin)where TOriginEnum : Enum
    {
        

        float fillAmount = 1;
        _fadeImage.fillAmount = fillAmount;

        _fadeImage.fillMethod=AutoMethodSet(origin);
        _fadeImage.fillOrigin = Convert.ToInt32(origin);//Enumをintに変換
        while (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime;
            _fadeImage.fillAmount = fillAmount;
            await UniTask.Yield();
        }
    }

    /// <summary>
    /// 渡されたoriginに基づいてmethodを変える関数
    /// </summary>
    /// <typeparam name="TOriginEnum">FillOriginEnum.csの中のEnum</typeparam>
    /// <param name="origin">渡されたOriginEnum</param>
    private Image.FillMethod AutoMethodSet<TOriginEnum>(TOriginEnum origin)where TOriginEnum : Enum
    {
        switch (origin)
        {
            case HorizontalOrigin:
                return Image.FillMethod.Horizontal;
            case VerticalOrigin:
                return Image.FillMethod.Vertical;
            case Radial_90_Origin:
                return Image.FillMethod.Radial90;
            case Radial_180_Origin:
                return Image.FillMethod.Radial180;
            case Radial_360_Origin:
                return Image.FillMethod.Radial360;
        }
        return Image.FillMethod.Horizontal;
    }
    
    
    /// <summary>
    /// 透明度いじる方式のフェードイン
    /// </summary>
    public async UniTask FadeIn()
    {
        _fadeImage.fillAmount = 1;

        float a = 0f;
  
        while (a<1)
        {
            a += Time.deltaTime*_fadeSpeed;
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// 透明度いじる方式のフェードアウト
    /// </summary>
    public async UniTask FadeOut()
    {
        _fadeImage.fillAmount = 1;

        float a = 1f;

        while (a > 0)
        {
            a -= Time.deltaTime * _fadeSpeed;
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, a);
            await UniTask.Yield();
        }
    }
    /// <summary>
    /// どの色でフェードするか
    /// </summary>
    /// <param name="color">色</param>
    public void SetColor(Color color)
    {
        _fadeImage.color = color;
    }
}
